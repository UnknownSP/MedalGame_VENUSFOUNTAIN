#include "app.h"
#include "DD_Gene.h"
#include "DD_RCDefinition.h"
#include "SystemTaskManager.h"
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>
#include "MW_GPIO.h"
#include "MW_IWDG.h"
#include "message.h"
#include "MW_flash.h"
#include "constManager.h"

static
int getRotateDirectionByMech(int recentPocket, int nowPocket, int nowDirection);
static
double PositionControl(double targetPocket, double nowPocket);

static int win_medal_coef = 2;
volatile uint8_t Pocket_Number;
volatile double Pocket_Number_Detailed;
volatile uint8_t Pocket_In_Number;

int appInit(void){

	ad_init();

	/*GPIO の設定などでMW,GPIOではHALを叩く*/
	return EXIT_SUCCESS;
}

/*application tasks*/
int appTask(void){
	
	int i;
	static unsigned int win_medal = 0;

	static bool enable_flag = false;
	static bool button_flag = true;
	//static unsigned int test_duty = 0;
	//static unsigned int rift_target = 0;
	//static unsigned int JPC_in_num = 0;
	//static bool JPC_flag = true;
	//static int test_JPC_duty = 0;
	static int button_count = 0;
	static bool _is_manual = false;
	static bool _launch_R_Ball = false;
	static bool _launch_R_S1 = false;
	static bool _launch_R_S2 = false;
	static bool _launch_L_Ball = false;
	static bool _launch_L_S1 = false;
	static bool _launch_L_S2 = false;
	static bool _sens_pos_1 = false;
	static bool _sens_pos_2 = false;
	static bool _sens_pos_3 = false;
	static bool _sens_pos_4 = false;
	static bool _sens_pos_5 = false;
	static bool _sens_pos_1_enable = true;
	int pocket = 0;
	static bool _ball_In_UpdateStop = false;
	static bool _ball_In_UpdateStopped = false;
	static bool _ball_InStop = false;
	static bool _ball_InStopped = false;
	static int rotate_direction = 1;
	static int rotate_direction_byMech = 1;
	static bool _Pos_control = false;
	static double PosCtrl_target = 0.0;
	static double PosCtrl_diff = 0.0;

	//ユーザーボタンの状態管理
	if(MW_GPIORead(GPIOCID,GPIO_PIN_13) == 0 & button_flag){
		button_flag = false;
		button_count++;
		if(!enable_flag){
			enable_flag = true;
		}else{
			//enable_flag = false;
		}
	}
	if(MW_GPIORead(GPIOCID,GPIO_PIN_13) == 1){
		button_flag = true;
	}

	//PCからの受信データの処理
	if(((PC_control_rcv[0] & 0b10000000) >> 7) == 1){
		enable_flag = true;
	}else{
		enable_flag = false;
	}
	if(((PC_control_rcv[0] & 0b00100000) >> 5) == 1){
		_is_manual = true;
	}else{
		_is_manual = false;
	}
	if(((PC_control_rcv[1] & 0b10000000) >> 7) == 1){
		_ball_InStop = true;
	}else{
		_ball_InStop = false;
	}
	if(((PC_control_rcv[1] & 0b01000000) >> 6) == 1){
		_ball_In_UpdateStop = true;
	}else{
		_ball_In_UpdateStop = false;
	}
	if(((PC_control_rcv[3] & 0b10000000) >> 7) == 1){
		_Pos_control = true;
	}else{
		_Pos_control = false;
	}
	PosCtrl_target = (double)((PC_control_rcv[2] & 0b11111000) >> 3) / 2.0;

	//スレーブから得られたセンサの値から状態(フラグ)をセット
	if(((g_md_h[PIC_TYPE2].rcv_data[1] >> 3) & 0b00000001) == 1){
		_launch_R_S1 = true;
	}else{
		_launch_R_S1 = false;
	}
	if(((g_md_h[PIC_TYPE2].rcv_data[1] >> 2) & 0b00000001) == 1){
		_launch_R_S2 = true;
	}
	if(((g_md_h[PIC_TYPE2].rcv_data[1] >> 5) & 0b00000001) == 1){
		_launch_L_S1 = true;
	}else{
		_launch_L_S1 = false;
	}
	if(((g_md_h[PIC_TYPE2].rcv_data[1] >> 4) & 0b00000001) == 1){
		_launch_L_S2 = true;
	}
	//入賞検知センサ
	if(((g_md_h[PIC_TYPE1].rcv_data[1] >> 4) & 0b00000001) == 1){
		if(!_ball_In_UpdateStopped){
			Pocket_In_Number = 1;
		}
	}else if(((g_md_h[PIC_TYPE1].rcv_data[1] >> 5) & 0b00000001) == 1){
		if(!_ball_In_UpdateStopped){
			Pocket_In_Number = 2;
		}
	}else if(((g_md_h[PIC_TYPE1].rcv_data[1] >> 6) & 0b00000001) == 1){
		if(!_ball_In_UpdateStopped){
			Pocket_In_Number = 3;
		}
	}else if(((g_md_h[PIC_TYPE1].rcv_data[1] >> 7) & 0b00000001) == 1){
		if(!_ball_In_UpdateStopped){
			Pocket_In_Number = 4;
		}
	}else{
		if(!_ball_In_UpdateStopped){
			Pocket_In_Number = 0;
		}
	}
	if(((g_md_h[PIC_TYPE1].rcv_data[0] >> 0) & 0b00000001) == 1){
		_sens_pos_1 = true;
	}else{
		_sens_pos_1 = false;
	}
	if(((g_md_h[PIC_TYPE1].rcv_data[0] >> 1) & 0b00000001) == 1){
		_sens_pos_2 = true;
	}else{
		_sens_pos_2 = false;
	}
	if(((g_md_h[PIC_TYPE1].rcv_data[0] >> 2) & 0b00000001) == 1){
		_sens_pos_3 = true;
	}else{
		_sens_pos_3 = false;
	}
	if(((g_md_h[PIC_TYPE1].rcv_data[0] >> 3) & 0b00000001) == 1){
		_sens_pos_4 = true;
	}else{
		_sens_pos_4 = false;
	}
	if(((g_md_h[PIC_TYPE1].rcv_data[0] >> 4) & 0b00000001) == 1){
		_sens_pos_5 = true;
	}else{
		_sens_pos_5 = false;
	}
	if(((g_md_h[PIC_TYPE1].rcv_data[1] >> 2) & 0b00000001) == 1){
		rotate_direction = 1;
	}else if(((g_md_h[PIC_TYPE1].rcv_data[1] >> 3) & 0b00000001) == 1){
		rotate_direction = -1;
	}else{
		rotate_direction = 0;
	}

	//抽選機エンコーダの値をデコード
	if(_sens_pos_1){
		if(_sens_pos_1_enable){
			pocket = 0;
			if(_sens_pos_2 || _sens_pos_3 ||_sens_pos_4 ||_sens_pos_5){
				if(_sens_pos_2){
					pocket += 1;
				}
				if(_sens_pos_3){
					pocket += 2;
				}
				if(_sens_pos_4){
					pocket += 4;
				}
				if(_sens_pos_5){
					pocket += 8;
				}
				rotate_direction_byMech = getRotateDirectionByMech(Pocket_Number,pocket,rotate_direction_byMech);
				Pocket_Number = pocket;
				Pocket_Number_Detailed = pocket;
			}else{
				if(rotate_direction != 0){
					Pocket_Number_Detailed = Pocket_Number + (double)rotate_direction * 0.5;
				}else{
					Pocket_Number_Detailed = Pocket_Number + (double)rotate_direction_byMech * 0.5;
				}
				if(Pocket_Number_Detailed < 0.0){
					Pocket_Number_Detailed = 11.5;
				}else if(Pocket_Number_Detailed > 12.0){
					Pocket_Number_Detailed = 0.5;
				}
			}
		}
		_sens_pos_1_enable = false;
	}else{
		_sens_pos_1_enable = true;
	}


	if(enable_flag){
		//全てのスレーブをゲーム中にセット
		for(i=0;i<DD_NUM_OF_MD;i++){
			g_md_h[i].mode = D_MMOD_IN_GAME;
		}

		if(_ball_In_UpdateStop){
			if(Pocket_In_Number != 0){
				_ball_In_UpdateStopped = true;
			}
		}else{
			_ball_In_UpdateStopped = false;
		}
		//ボールが入賞してストップする場合
		if(_ball_InStop){
			if(Pocket_In_Number != 0){
				_ball_InStopped = true;
			}
			if(!_ball_InStopped){
				if(_Pos_control){
					PosCtrl_diff = PositionControl(PosCtrl_target,Pocket_Number_Detailed);
				}else{
					g_md_h[PIC_TYPE1].snd_data[0] = PC_control_rcv[3] & 0b01001111;
					g_md_h[PIC_TYPE1].snd_data[1] = PC_control_rcv[4];
				}
			}else{
				g_md_h[PIC_TYPE1].snd_data[0] &= 0b01000000;
				g_md_h[PIC_TYPE1].snd_data[1] = 0b00000000;
			}
		}else{
			_ball_InStopped = false;
			if(_Pos_control){
				PosCtrl_diff = PositionControl(PosCtrl_target,Pocket_Number_Detailed);
				PC_control_temp = PosCtrl_target;
			}else{
				g_md_h[PIC_TYPE1].snd_data[0] = PC_control_rcv[3] & 0b01001111;
				g_md_h[PIC_TYPE1].snd_data[1] = PC_control_rcv[4];
			}
		}


		//手動操作による状態セット
		if(_is_manual){
			g_md_h[PIC_TYPE2].snd_data[0] = 0b00000000;
			g_md_h[PIC_TYPE2].snd_data[1] = PC_control_rcv[2] & 0b00000011;
		}
		if((PC_control_rcv[1] & 0b00000001) == 1){
			_launch_R_Ball = true;
		}
		if(((PC_control_rcv[1] >> 1) & 0b00000001) == 1){
			_launch_L_Ball = true;
		}
		if(_launch_R_Ball){
			if(_launch_R_S2 && _launch_R_S1){
				_launch_R_Ball = false;
				_launch_R_S2 = false;
				g_md_h[PIC_TYPE2].snd_data[1] &= 0b11111110;
			}else{
				g_md_h[PIC_TYPE2].snd_data[1] |= 0b00000001;
			}
		}
		if(_launch_L_Ball){
			if(_launch_L_S2 && _launch_L_S1){
				_launch_L_Ball = false;
				_launch_L_S2 = false;
				g_md_h[PIC_TYPE2].snd_data[1] &= 0b11111101;
			}else{
				g_md_h[PIC_TYPE2].snd_data[1] |= 0b00000010;
			}
		}
	}else{
		//全てのスレーブの動作を停止
		for(i=0;i<DD_NUM_OF_MD;i++){
			g_md_h[i].mode = D_MMOD_STANDBY;
		}
		g_md_h[PIC_TYPE1].snd_data[0] = 0b00000000;
		g_md_h[PIC_TYPE1].snd_data[1] = 0b00000000;
		g_md_h[PIC_TYPE2].snd_data[0] = 0b00000000;
		g_md_h[PIC_TYPE2].snd_data[1] = 0b00000000;
		_launch_R_Ball = false;
		_launch_L_Ball = false;
		_ball_InStop = false;
		_ball_InStopped = false;
		_Pos_control = false;
	}

	if( g_SY_system_counter % _MESSAGE_INTERVAL_MS < _INTERVAL_MS ){
		//MW_printf("[Mess] %f %f",PosCtrl_target,PosCtrl_diff);
	}
	return EXIT_SUCCESS;
}

static
int getRotateDirectionByMech(int recentPocket, int nowPocket, int nowDirection){
	if(recentPocket == nowPocket) return nowDirection;
	int diff = nowPocket - recentPocket;
	if(diff == 11){
		return -1;
	}else if(diff == -11){
		return 1;
	}else{
		if(diff > 0){
			return 1;
		}else if(diff < 0){
			return -1;
		}
	}
}

//抽選機の回転位置をセットする関数
static
double PositionControl(double targetPocket, double nowPocket){
	double diffToTarget = targetPocket - nowPocket;
	double diffAbs = fabs(diffToTarget);
	double calcTarget = 0.0;
	int16_t rotate_speed = 0;

	if(diffAbs > 6.0){
		if(diffToTarget > 0.0){
			calcTarget = diffToTarget - 12.0;
		}else if(diffToTarget < 0.0){
			calcTarget = diffToTarget + 12.0;
		}
	}else{
		calcTarget = diffToTarget;
	}

	if(fabs(calcTarget) <= ROTATE_STOP_RANGE){
		g_md_h[PIC_TYPE1].snd_data[0] &= 0b11110000;
		g_md_h[PIC_TYPE1].snd_data[1] = 0b00000000;
	}else{
		if(calcTarget > 0.0){
			g_md_h[PIC_TYPE1].snd_data[0] &= 0b11110111;
			g_md_h[PIC_TYPE1].snd_data[0] |= 0b00000100;
		}else if(calcTarget < 0.0){
			g_md_h[PIC_TYPE1].snd_data[0] &= 0b11111011;
			g_md_h[PIC_TYPE1].snd_data[0] |= 0b00001000;
		}
		rotate_speed = ROTATE_MIN_SPEED + (int)(fabs(calcTarget)*ROTATE_SPEED_COEFF);
		g_md_h[PIC_TYPE1].snd_data[0] = (g_md_h[PIC_TYPE1].snd_data[0] & 0b11111100) + ((rotate_speed >> 8) & 0b00000011);
		g_md_h[PIC_TYPE1].snd_data[1] = rotate_speed & 0b0000000011111111;
	}
	return calcTarget;
}