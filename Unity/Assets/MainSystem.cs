using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MainSystem : MonoBehaviour
{
    public SerialHandler serialHandler;
    GameObject UICanvas;
    GameObject ROTATE;
    double time = 0.0;
    double case_time = 0.0;
    bool skip_enable = false;
    bool skiped = false;

    /*receive data*/
    int user_button_data = 0;
    bool user_button_enable = true;
    bool USER_BUTTON_PUSH = true;
    bool USER_BUTTON_PUSH_ONETIME = false;
    int test_data = 0;
    bool status_recet_flag = false;
    bool status_game_start_flag = false;
    bool status_rounch_ball_flag = false;
    bool status_out_ball_flag = false;
    bool status_red_jpc_flag = false;
    bool status_blue_jpc_flag = false;
    /*receive data*/

    GameObject READY;
    GameObject START;
    GameObject bar_left;
    GameObject bar_right;
    GameObject bar_middle;
    GameObject WIN;
    GameObject OVER_100;
    GameObject OVER_100_RULE;
    //GameObject RESULT;
    //GameObject OUT;
    SpriteRenderer title_render;
    SpriteRenderer setumei_button_render;
    SpriteRenderer setumei1_render;
    SpriteRenderer setumei2_render;
    SpriteRenderer setumei3_render;
    SpriteRenderer READY_render;
    SpriteRenderer START_render;
    SpriteRenderer bar_left_render;
    SpriteRenderer bar_right_render;
    SpriteRenderer bar_middle_render;
    SpriteRenderer WIN_render;
    SpriteRenderer OVER_100_render;
    SpriteRenderer OVER_100_RULE_render;
    SpriteRenderer UNDER_100_RESULT_IMAGE_render;
    //SpriteRenderer RESULT_render;
    //SpriteRenderer OUT_render;
    VideoPlayer gamestart1_vp;
    VideoPlayer gamestart2_vp;
    VideoPlayer game_in1_vp;
    VideoPlayer game_in2_vp;
    VideoPlayer game_over_100_1_vp;
    VideoPlayer game_over_100_2_vp;
    VideoPlayer over_100_result_vp;
    Vector3 bar_left_posi;
    Vector3 bar_right_posi;
    Vector3 bar_middle_scale;
    Vector3 READY_scale;
    Vector3 START_scale;
    Vector3 START_rotation;
    Vector3 OVER_100_scale;
    Vector3 OVER_100_RULE_scale;
    float READY_alpha = 0.0f;
    float START_alpha = 0.0f;

    TextMesh MUSIC_CREDIT_mesh;

    public float sound_volume = 0.1f;
    public AudioClip Bumper_Sound;
    public AudioClip GAME_START_1_Sound;
    public AudioClip GAME_START_2_Sound;
    public AudioClip ROUNCH_BALL_Sound;
    public AudioClip OVER_100_Sound;
    public AudioClip UNDER_100_Sound;
    public AudioClip OVER_100_RESULT_Sound;
    public AudioClip UNDER_100_RESULT_Sound;
    public AudioClip DISPLAY_MOVE;
    public AudioClip VENUSJPC_START_BGM;
    public AudioClip VENUSJPC_STARTEND_BGM;
    public AudioClip VENUSJPC_MEGA_BGM;
    public AudioClip VENUSJPC_GIGA_BGM;
    public AudioClip VENUSJPC_VENUS_BGM;
    public AudioClip VENUSJPC_ALL_BGM;
    public AudioClip VENUSJPC_MEGA_VOICE;
    public AudioClip VENUSJPC_GIGA_VOICE;
    public AudioClip VENUSJPC_VENUS_VOICE;
    public AudioClip VENUSJPC_123_START_BGM;
    public AudioClip VENUSJPC_NEXT_VOICE_BGM;
    public AudioClip VENUSJPC_NEXT_SE_1_BGM;
    public AudioClip VENUSJPC_NEXT_SE_2_BGM;
    public AudioClip VENUSJPC_NEXT_SE_3_BGM;
    public AudioClip VENUSJPC_POWERUP_BGM;
    public AudioClip VENUSJPC_THISISGOOD_BGM;
    public AudioClip VENUSJPC_JPPOCKET_BGM;
    public AudioClip FOUNTAINJPC_START_ST_BGM;
    public AudioClip FOUNTAINJPC_START_BGM;
    public AudioClip FOUNTAINJPC_STARTEND_BGM;
    public AudioClip FOUNTAINJPC_1TO2_BGM;
    public AudioClip FOUNTAINJPC_3_BGM;
    public AudioClip FOUNTAINJPC_ROTATEWAITING;
    public AudioClip FOUNTAINJPC_LAUNCH_2and3;
    public AudioClip FOUNTAINJPC_LAUNCHBALL;
    public AudioClip FOUNTAINJPC_LAUNCHBALL_FIRST;
    public AudioClip FOUNTAINJPC_LAUNCHBALL_SECOND;
    public AudioClip FOUNTAINJPC_LAUNCHBALL_THIRD;
    public AudioClip FOUNTAINJPC_JPGET;
    public AudioClip FOUNTAINJPC_JPGET_BTF;
    public AudioClip FOUNTAINJPC_NOTJPGET;
    public AudioClip FOUNTAINJPC_POCKETIN_1;
    public AudioClip FOUNTAINJPC_POCKETIN_3;
    public AudioClip FOUNTAINJPC_2TO3_BGM;
    public AudioClip FOUNTAINJPC_IN_OCEAN_1_BGM;
    public AudioClip FOUNTAINJPC_IN_WIND_1_BGM;
    public AudioClip FOUNTAINJPC_IN_SUNRISE_1_BGM;
    public AudioClip FOUNTAINJPC_IN_WILD_1_BGM;
    public AudioClip FOUNTAINJPC_OCEAN_2TO3_BGM;
    public AudioClip FOUNTAINJPC_WIND_2TO3_BGM;
    public AudioClip FOUNTAINJPC_SUNRISE_2TO3_BGM;
    public AudioClip FOUNTAINJPC_WILD_2TO3_BGM;
    public AudioClip FOUNTAINJPC_REACH_BGM;
    public AudioClip BLUE_IN_BGM;
    public AudioClip GREEN_IN_BGM;
    public AudioClip RED_IN_BGM;
    public AudioClip WILD_IN_BGM;
    public AudioClip JPC_END_50TO150WIN;
    public AudioClip JPC_END_200TO350WIN;
    public AudioClip JPC_END_400TO750WIN;
    public AudioClip JPC_END_800WIN;
    public AudioClip WELCOME_VF_BGM;
    public AudioClip GC_ROYALJPC_BGM;
    public AudioClip KONAMI_ERROR_BGM;
    AudioSource SE_audio_source;
    [SerializeField] public AudioSource BGM_audio_source;
    [SerializeField] public AudioSource JP_audio_source;
    [SerializeField] public AudioSource Error_audio_source;
    bool Sound_first_flag = true;

    VideoPlayer F_START_VIDEO;
    VideoPlayer V_START_VIDEO;

    GameObject[,] Display_number = new GameObject[5, 10];
    SpriteRenderer[,] Display_number_render = new SpriteRenderer[5, 10];

    public Text OpMode_text;
    public Image OpMode_back;
    bool OperationMode = false;
    bool OpSwitch_Select = false;
    bool OpSwitch_Enter = false;

    int win_medal = 0;
    int win_meadl_test_coef = 3;

    int game_phase = 0;

    Button RotateSpeedReset_Button;
    Slider RotateSpeed_Slider;
    Text Debug_text;
    Text DebugSystem_text;
    Text TargetRotateSpeed_text;
    Text RealRotateSpeed_text;
    Image S_Pos_1;
    Image S_Pos_2;
    Image S_Pos_3;
    Image S_Pos_4;
    Image S_Pos_5;
    Image S_In_1;
    Image S_In_2;
    Image S_In_3;
    Image S_In_4;
    Image S_Ball_R;
    Image S_Ball_L;
    Image S_Launch_R_1;
    Image S_Launch_R_2;
    Image S_Launch_L_1;
    Image S_Launch_L_2;
    static int rotateSpeed = 0;
    static int realRotateSpeed = 0;

    string debug_flush = "";
    string debug_MD1 = "";
    string debug_MD2 = "";
    string debug_PC = "";

    string[] MD1_data = new string[] { };
    string[] MD2_data = new string[] { };
    byte[,] MD1_rcv = new byte[3, 8] { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
    byte[,] MD2_rcv = new byte[3, 8] { { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
    bool _Sensor_In_1 = false;
    bool _Sensor_In_2 = false;
    bool _Sensor_In_3 = false;
    bool _Sensor_In_4 = false;
    bool _Sensor_Pos_1 = false;
    bool _Sensor_Pos_2 = false;
    bool _Sensor_Pos_3 = false;
    bool _Sensor_Pos_4 = false;
    bool _Sensor_Pos_5 = false;
    bool _Sensor_Ball_R = false;
    bool _Sensor_Ball_L = false;
    bool _Sensor_Launch_R_1 = false;
    bool _Sensor_Launch_R_2 = false;
    bool _Sensor_Launch_L_1 = false;
    bool _Sensor_Launch_L_2 = false;

    EnableToggle enableToggle;
    ManualToggle manualToggle;
    BGMToggle BGMToggle;
    Launch_R_Motor launch_R_Motor;
    Launch_L_Motor launch_L_Motor;
    Launch_R_Ball launch_R_Ball;
    Launch_L_Ball launch_L_Ball;
    GameObject Lottery;
    SlowSlider slow_speed_1;
    SlowSlider slow_speed_2;
    SlowSlider slow_speed_3;
    SlowSlider slow_continue_1;
    SlowSlider slow_continue_2;
    SlowSlider slow_continue_3;
    SlowSlider slow_transition_1;
    SlowSlider slow_transition_2;
    SlowSlider slow_transition_3;
    SlowSlider slider_userInputMin;
    SlowSlider slider_userInputMax;
    Image SliderBackGround;
    Image ControllerBackGround;
    ControlButton startOneTime;
    FountainButton startFountain;
    VenusButton startVenus;
    ControlDropButton launchSide;
    Image BallInPocket1_Image;
    Image BallInPocket2_Image;
    Image BallInPocket3_Image;
    SpriteRenderer RotateLottery_Render;
    SpriteRenderer Rotate_BG_Render;
    SpriteRenderer Rotate_GO_Render;
    SpriteRenderer Rotate_START_Render;
    GameObject RotateLottery;
    LotteryAction LotteryAction;
    touchRotate touchRotate;

    const float Lotter_offset = 180f;
    static float lottery_deg = 0f;

    private static readonly Color DISABLE_COLOR = new Color(0.7f, 0.7f, 0.7f);
    private static readonly Color ENABLE_COLOR = new Color(1.0f, 1.0f, 1.0f);

    byte[] send_data = new byte[8] {
        0,0,0,0,0,0,0,0
    };

    static int GamePhase = 0;
    static bool _startOneTime = false;
    static bool _startFountain = false;
    static bool _startVenus = false;
    static bool _autoMoving = false;
    static bool _autoMovingFountain = false;
    static bool _autoMovingVenus = false;

    static int[] Fountain_In_Pocket = new int[3] { 0, 0, 0 };
    static string[] Fountain_In_Pocket_Color = new string[3] { "", "", "" };
    static int[] Venus_In_Pocket = new int[4] { 0, 0, 0, 0 };
    static string[] Venus_In_Pocket_Color = new string[4] { "", "", "", "" };
    static int Fountain_count = 0;
    static int Venus_count = 0;


    private const int GP_startFountain = 17;
    private const int GP_startVenus = 18;
    private const int GP_startWait = 19;
    private const int GP_startLottery = 20;
    private const int GP_setStartSpeed = 21;
    private const int GP_setStartSide = 22;
    private const int GP_waitLotteryStop = 23;
    private const int GP_ditectRBall = 24;
    private const int GP_ditectLBall = 25;
    private const int GP_LoadRBall = 26;
    private const int GP_LoadLBall = 27;
    private const int GP_waitLoadStop = 28;
    private const int GP_waitRotationInit = 29;
    private const int GP_waitUserRotation = 30;
    private const int GP_startRotation = 31;
    private const int GP_waitRotation = 32;
    private const int GP_LaunchBall = 33;
    private const int GP_SlowSpeedWait = 34;
    private const int GP_SlowSpeed1 = 35;
    private const int GP_SlowSpeed2 = 36;
    private const int GP_SlowSpeed3 = 37;
    private const int GP_BallInStop = 38;
    private const int GP_BallInPocketDitect = 39;
    private const int GP_BallCollect = 40;
    private const int GP_WelcomeVF = 41;
    private const int GP_waitSound1 = 50;
    private const int GP_waitSound2 = 51;
    private const int GP_waitSound3 = 52;
    private const int GP_waitAccel = 53;


    private const int RotateInitSpeed = 50;
    private const int LoadSpeed = 8;
    private const int RotateDirection = 1;
    private const double StartAccelTime = 4.0f;
    private const double StartWaitTime = 2.0f;
    private const double waitSound1_time = 4.5;
    private const double waitSound2_time = 3.0;
    private const double waitSound3_time = 2.0;

    static int AM_RotateInit_FirstPocket = 0;
    static int AM_BallInPocket = 0;
    static int AM_BallInSensor = 0;
    static bool AM_BallInFlag = false;
    static int AM_startSpeed = 0;
    static int AM_recentSpeed = 0;
    static int AM_recentSpeedDiff = 0;
    static int AM_startSide = 0;
    static double AM_startTime = 0.0;
    static double AM_caseTime = 0.0;
    static bool AM_Error = false;

    static int Pocket_Num = 0;
    static float Pocket_Num_Detailed = 0.0f;
    static int Pocket_In_Num = 0;
    static bool JP_Audio_reset = true;

    const float lotterySize_outside = 835.0f; //1920x1080
    const float lotteryOrigin = 552.5f; //1920x1080
    const float lotteryOrigin_diff = lotteryOrigin - (1080.0f/2.0f); //1920x1080
    const float lotterySize_inside = 545.0f; //1920x1080
    static bool _rotate_firstTouchedLottery = false;
    static bool _rotate_TouchedOut = false;
    static float _rotate_firstTouchAngle = 0.0f;
    static float _rotate_TouchedOutAngle = 0.0f;
    static float _rotate_TouchedOutAngleDiff = 0.0f;
    static float _rotate_TouchedOutAngleSum = 0.0f;
    static bool _rotate_TouchRelease = false;
    static float _rotate_LotteryDeg = 0.0f;
    static float _rotate_LotterySpeed = 0.0f;
    static float _rotate_recentPos_x = 0.0f;
    static float _rotate_recentPos_y = 0.0f;
    static bool _rotate_recentPush = false;
    static float _rotate_deltaTime = 0.0f;
    static bool _rotate_is_rotated = false;

    static float launch_delay = 0.0f;
    static float slowspeed_delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
        UICanvas = GameObject.Find("Canvas");
        ROTATE = GameObject.Find("ROTATE");
        serialHandler.OnDataReceived += OnDataReceived;
        RotateSpeed_Slider = GameObject.Find("RotateSpeedSlider").GetComponent<Slider>();
        RotateSpeed_Slider.onValueChanged.AddListener(Slider_action);
        RotateSpeedReset_Button = GameObject.Find("RotateSpeedResetButton").GetComponent<Button>();
        RotateSpeedReset_Button.onClick.AddListener(ResetButton_action);
        Debug_text = GameObject.Find("DebugText").GetComponent<Text>();
        DebugSystem_text = GameObject.Find("DebugSystemText").GetComponent<Text>();
        TargetRotateSpeed_text = GameObject.Find("TargetRotateSpeedText").GetComponent<Text>();
        RealRotateSpeed_text = GameObject.Find("RealRotateSpeedText").GetComponent<Text>();
        S_Pos_1 = GameObject.Find("S_Pos_1").GetComponent<Image>();
        S_Pos_2 = GameObject.Find("S_Pos_2").GetComponent<Image>();
        S_Pos_3 = GameObject.Find("S_Pos_3").GetComponent<Image>();
        S_Pos_4 = GameObject.Find("S_Pos_4").GetComponent<Image>();
        S_Pos_5 = GameObject.Find("S_Pos_5").GetComponent<Image>();
        S_In_1 = GameObject.Find("S_In_1").GetComponent<Image>();
        S_In_2 = GameObject.Find("S_In_2").GetComponent<Image>();
        S_In_3 = GameObject.Find("S_In_3").GetComponent<Image>();
        S_In_4 = GameObject.Find("S_In_4").GetComponent<Image>();
        S_Ball_R = GameObject.Find("S_Ball_R").GetComponent<Image>();
        S_Ball_L = GameObject.Find("S_Ball_L").GetComponent<Image>();
        S_Launch_R_1 = GameObject.Find("S_Launch_R_1").GetComponent<Image>();
        S_Launch_R_2 = GameObject.Find("S_Launch_R_2").GetComponent<Image>();
        S_Launch_L_1 = GameObject.Find("S_Launch_L_1").GetComponent<Image>();
        S_Launch_L_2 = GameObject.Find("S_Launch_L_2").GetComponent<Image>();
        enableToggle = GameObject.Find("EnableToggle").GetComponent<EnableToggle>();
        manualToggle = GameObject.Find("ManualToggle").GetComponent<ManualToggle>();
        BGMToggle = GameObject.Find("BGMToggle").GetComponent<BGMToggle>();
        launch_R_Motor = GameObject.Find("Launch_R_Motor").GetComponent<Launch_R_Motor>();
        launch_L_Motor = GameObject.Find("Launch_L_Motor").GetComponent<Launch_L_Motor>();
        launch_R_Ball = GameObject.Find("Launch_R_Ball").GetComponent<Launch_R_Ball>();
        launch_L_Ball = GameObject.Find("Launch_L_Ball").GetComponent<Launch_L_Ball>();
        Lottery = GameObject.Find("Lottery");
        slow_speed_1 = GameObject.Find("SlowSpeed_1_Slider").GetComponent<SlowSlider>();
        slow_speed_2 = GameObject.Find("SlowSpeed_2_Slider").GetComponent<SlowSlider>();
        slow_speed_3 = GameObject.Find("SlowSpeed_3_Slider").GetComponent<SlowSlider>();
        slow_continue_1 = GameObject.Find("SlowSpeed_1_continue_Slider").GetComponent<SlowSlider>();
        slow_continue_2 = GameObject.Find("SlowSpeed_2_continue_Slider").GetComponent<SlowSlider>();
        slow_continue_3 = GameObject.Find("SlowSpeed_3_continue_Slider").GetComponent<SlowSlider>();
        slow_transition_1 = GameObject.Find("SlowSpeed_1_transition_Slider").GetComponent<SlowSlider>();
        slow_transition_2 = GameObject.Find("SlowSpeed_2_transition_Slider").GetComponent<SlowSlider>();
        slow_transition_3 = GameObject.Find("SlowSpeed_3_transition_Slider").GetComponent<SlowSlider>();
        slider_userInputMin = GameObject.Find("UserInputMinSpeed_Slider").GetComponent<SlowSlider>();
        slider_userInputMax = GameObject.Find("UserInputMaxSpeed_Slider").GetComponent<SlowSlider>();
        SliderBackGround = GameObject.Find("BackGround_Slider").GetComponent<Image>();
        ControllerBackGround = GameObject.Find("BackGround_Controller").GetComponent<Image>();
        startOneTime = GameObject.Find("StartOneTime_Button").GetComponent<ControlButton>();
        launchSide = GameObject.Find("LaunchSide_DropDown").GetComponent<ControlDropButton>();
        BallInPocket1_Image = GameObject.Find("InPocket_1").GetComponent<Image>();
        BallInPocket2_Image = GameObject.Find("InPocket_2").GetComponent<Image>();
        BallInPocket3_Image = GameObject.Find("InPocket_3").GetComponent<Image>();
        startFountain = GameObject.Find("StartFountain_Button").GetComponent<FountainButton>();
        startVenus = GameObject.Find("StartVenus_Button").GetComponent<VenusButton>();

        SE_audio_source = GetComponent<AudioSource>();
        SE_audio_source.volume = sound_volume;
        BGM_audio_source.volume = sound_volume;
        JP_audio_source.volume = sound_volume;
        Error_audio_source.volume = sound_volume;

        F_START_VIDEO = GameObject.Find("F_START_VIDEO").GetComponent<VideoPlayer>();
        F_START_VIDEO.loopPointReached += video_end;
        V_START_VIDEO = GameObject.Find("V_START_VIDEO").GetComponent<VideoPlayer>();
        V_START_VIDEO.loopPointReached += video_end;
        //F_ROTATE_VIDEO = GameObject.Find("F_ROTATE_VIDEO").GetComponent<VideoPlayer>();
        //F_ROTATE_VIDEO.loopPointReached += video_end;

        RotateLottery = GameObject.Find("ROTATE_LOTTERY_GEN");
        LotteryAction = GameObject.Find("ROTATE_LOTTERY_GEN").GetComponent<LotteryAction>();
        touchRotate = GameObject.Find("ROTATE_START").GetComponent<touchRotate>();
        RotateLottery_Render = GameObject.Find("ROTATE_LOTTERY").GetComponent<SpriteRenderer>();
        Rotate_BG_Render = GameObject.Find("ROTATE_BG").GetComponent<SpriteRenderer>();
        Rotate_GO_Render = GameObject.Find("ROTATE_GO").GetComponent<SpriteRenderer>();
        Rotate_START_Render = GameObject.Find("ROTATE_START").GetComponent<SpriteRenderer>();

        userRotateInit();

    }

    // Update is called once per frame
    void Update()
    {
        KeybordInput();

        if (status_recet_flag)
        {
            //game_phase = 0;
        }
        if (status_out_ball_flag)
        {
            //game_phase = 20;
        }

        if (!user_button_enable && USER_BUTTON_PUSH_ONETIME)
        {
            USER_BUTTON_PUSH_ONETIME = false;
        }
        if (user_button_data == 1 && user_button_enable)
        {
            USER_BUTTON_PUSH_ONETIME = true;
            USER_BUTTON_PUSH = true;
            user_button_enable = false;
        }
        if (user_button_data == 0)
        {
            USER_BUTTON_PUSH = false;
            user_button_enable = true;
        }
        if (USER_BUTTON_PUSH_ONETIME)
        {
            
        }

        AM_caseTime += Time.deltaTime;
        //GamePhaseによって動作を分岐
        switch (GamePhase)
        {
            case GP_startFountain:
                ROTATE.SetActive(false);
                V_START_VIDEO.Stop();
                F_START_VIDEO.Stop();
                F_START_VIDEO.Play();
                Invoke("F_START_Sound", 1.5f);
                Invoke("F_STARTEND_Sound", 4.5f);
                GamePhase = GP_startWait;
                AM_caseTime = 0.0;
                break;
            case GP_startVenus:
                ROTATE.SetActive(false);
                V_START_VIDEO.Stop();
                F_START_VIDEO.Stop();
                V_START_VIDEO.Play();
                Invoke("V_START_Sound", 1.5f);
                Invoke("V_STARTEND_Sound", 4.5f);
                GamePhase = GP_startWait;
                AM_caseTime = 0.0;
                break;
            case GP_startWait:
                if (AM_caseTime < 4.5)
                {
                }
                else
                {
                    GamePhase = GP_startLottery;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_startLottery:
                if (_autoMovingFountain)
                {
                    if (Fountain_count == 2)
                    {
                        Invoke("F_3_BGM_Sound",5.0f);
                    }
                    else if (Fountain_count == 0)
                    {
                        SE_audio_source.clip = FOUNTAINJPC_1TO2_BGM;
                        SE_audio_source.loop = true;
                        SE_audio_source.Play();
                    }
                }
                if (_autoMovingVenus)
                {
                    if (Venus_count == 3)
                    {
                        //Invoke("V_ALL_BGM_Sound", 5.0f);
                    }
                    else if (Venus_count == 2)
                    {
                        //Invoke("V_VENUS_BGM_Sound", 5.0f);
                    }
                    else if (Venus_count == 1)
                    {
                        //Invoke("V_GIGA_BGM_Sound", 5.0f);
                    }
                    else if (Venus_count == 0)
                    {
                        Invoke("V_MEGA_BGM_Sound", 0.0f);
                    }
                }
                GamePhase = GP_setStartSpeed;
                AM_caseTime = 0.0;
                break;
            case GP_setStartSpeed:
                //AM_startSpeed = (int)RotateSpeed_Slider.value;
                GamePhase = GP_setStartSide;
                AM_caseTime = 0.0;
                break;
            case GP_setStartSide:
                AM_startSide = launchSide.selected;
                if (AM_startSide == 1)
                {
                    AM_startSide = 0;
                }
                else
                {
                    AM_startSide = 1;
                }
                if (_autoMovingFountain)
                {
                    if(Fountain_count != 0)
                    {
                        if (AM_startSide == 0)
                        {
                            GamePhase = GP_ditectRBall;
                        }
                        else if (AM_startSide == 1)
                        {
                            GamePhase = GP_ditectLBall;
                        }
                        AM_caseTime = 0.0;
                        break;
                    }
                }
                GamePhase = GP_waitLotteryStop;
                AM_caseTime = 0.0;
                break;
            case GP_waitLotteryStop:
                if (realRotateSpeed == 0)
                {
                    if (AM_startSide == 0)
                    {
                        GamePhase = GP_ditectRBall;
                    }else if (AM_startSide == 1)
                    {
                        GamePhase = GP_ditectLBall;
                    }
                    AM_caseTime = 0.0;
                }
                else
                {
                    rotateSpeed = 0;
                }
                break;
            case GP_ditectRBall:
                if (_Sensor_Ball_R)
                {
                    GamePhase = GP_waitRotationInit;
                    AM_RotateInit_FirstPocket = Pocket_Num;
                    AM_caseTime = 0.0;
                }
                else
                {
                    GamePhase = GP_LoadRBall;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_ditectLBall:
                if (_Sensor_Ball_L)
                {
                    GamePhase = GP_waitRotationInit;
                    AM_RotateInit_FirstPocket = Pocket_Num;
                    AM_caseTime = 0.0;
                }
                else
                {
                    GamePhase = GP_LoadLBall;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_LoadRBall:
                if (!_Sensor_Ball_R)
                {
                    rotateSpeed = LoadSpeed * RotateDirection;
                }
                else
                {
                    if((_autoMovingVenus && (Venus_count != 0)) || (_autoMovingFountain && (Fountain_count != 0)))
                    {
                        GamePhase = GP_waitSound2;
                        AM_caseTime = 0.0;
                    }
                    else
                    {
                        GamePhase = GP_waitLoadStop;
                        rotateSpeed = 0;
                        AM_caseTime = 0.0;
                    }
                }
                break;
            case GP_LoadLBall:
                if (!_Sensor_Ball_L)
                {
                    rotateSpeed = -LoadSpeed * RotateDirection;
                }
                else
                {
                    if ((_autoMovingVenus && (Venus_count != 0)) || (_autoMovingFountain && (Fountain_count != 0)))
                    {
                        GamePhase = GP_waitSound2;
                        AM_caseTime = 0.0;
                    }
                    else
                    {
                        GamePhase = GP_waitLoadStop;
                        rotateSpeed = 0;
                        AM_caseTime = 0.0;
                    }
                }
                break;
            case GP_waitLoadStop:
                if (realRotateSpeed == 0)
                {
                    GamePhase = GP_waitRotationInit;
                    AM_RotateInit_FirstPocket = Pocket_Num;
                    AM_caseTime = 0.0;
                }
                else
                {
                    rotateSpeed = 0;
                }
                break;
            case GP_waitRotationInit:
                if ((_autoMovingFountain && Fountain_count == 0) || (_autoMovingVenus && Venus_count == 0))
                {
                    /*if (AM_RotateInit_FirstPocket == 8)
                    {
                        rotateSpeed = -RotateInitSpeed;
                        AM_RotateInit_FirstPocket = Pocket_Num;
                    }
                    else
                    {
                        if (Pocket_Num != 8)
                        {
                            rotateSpeed = -RotateInitSpeed;
                        }
                        else
                        {
                            rotateSpeed = 0;
                            AM_caseTime = 0.0;
                            GamePhase = GP_waitUserRotation;
                        }
                    }*/
                    if (AM_RotateInit_FirstPocket <= 7)
                    {
                        rotateSpeed = -RotateInitSpeed;
                        if (Pocket_Num > 7 && Pocket_Num <= 10)
                        {
                            AM_RotateInit_FirstPocket = Pocket_Num;
                        }
                    }
                    else
                    {
                        send_data[6] |= 0b10000000;
                        send_data[5] &= 0b00000111;
                        send_data[5] |= (byte)((0b00011111 & 13) << 3);
                        if (Pocket_Num_Detailed >= 6.0 && Pocket_Num_Detailed <= 7.0)
                        {
                            rotateSpeed = 0;
                            AM_caseTime = 0.0;
                            GamePhase = GP_waitUserRotation;
                            send_data[6] &= 0b01111111;
                            send_data[5] &= 0b00000111;
                        }
                    }
                }
                else
                {
                    AM_caseTime = 0.0;
                    GamePhase = GP_startRotation;
                }
                Sound_first_flag = true;
                break;
            case GP_waitUserRotation:
                if ((_autoMovingFountain && Fountain_count != 0) || (_autoMovingVenus && Venus_count != 0))
                {
                    AM_caseTime = 0.0;
                    Sound_first_flag = true;
                    GamePhase = GP_startRotation;
                    break;
                }
                if (Sound_first_flag && AM_caseTime > 1.0f)
                {
                    if (_autoMovingFountain)
                    {
                        SE_audio_source.PlayOneShot(FOUNTAINJPC_ROTATEWAITING);
                        Sound_first_flag = false;
                        userRotateInit();
                        color_alpha_to_1(RotateLottery_Render);
                        color_alpha_to_1(Rotate_GO_Render);
                        color_alpha_to_1(Rotate_START_Render);
                        color_alpha_to_1(Rotate_BG_Render);
                        ROTATE.SetActive(true);
                        touchRotate.startAnimation();
                    }
                    else if (_autoMovingVenus)
                    {
                        SE_audio_source.PlayOneShot(FOUNTAINJPC_ROTATEWAITING);
                        Sound_first_flag = false;
                        userRotateInit();
                        color_alpha_to_1(RotateLottery_Render);
                        color_alpha_to_1(Rotate_GO_Render);
                        color_alpha_to_1(Rotate_START_Render);
                        color_alpha_to_1(Rotate_BG_Render);
                        ROTATE.SetActive(true);
                        touchRotate.startAnimation();
                    }
                }
                var inputData = getInputData(false);
                //Debug.Log("x:" + inputData.pos_x + " y:" + inputData.pos_y + "pushed:"+inputData._pushed);
                _rotate_deltaTime += Time.deltaTime;
                if (!_rotate_is_rotated && (inputData.pos_x != _rotate_recentPos_x || inputData.pos_y != _rotate_recentPos_y || _rotate_recentPush != inputData._pushed))
                {
                    var inputAngle = getInputAngle(inputData.pos_x, inputData.pos_y);
                    //Debug.Log("inLottery:" + inputAngle._pos_in_lottery + " Angle:" + inputAngle.angle);
                    var rotateLottery = getRotateLottery(_rotate_LotteryDeg, _rotate_LotterySpeed, _rotate_deltaTime, inputAngle.angle, inputAngle._pos_in_lottery, inputData._pushed);
                    _rotate_LotteryDeg = rotateLottery.rotateDegree;
                    _rotate_LotterySpeed = rotateLottery.rotateSpeed;
                    //Debug.Log("Deg:" + _rotate_LotteryDeg + " Speed:" + _rotate_LotterySpeed + " isRotate:" + rotateLottery._is_rotate);
                    if (rotateLottery._is_rotate)
                    {
                        Debug.Log("Deg:" + _rotate_LotteryDeg + " Speed:" + _rotate_LotterySpeed + " isRotate:" + rotateLottery._is_rotate);
                        _rotate_is_rotated = true;
                    }
                    _rotate_deltaTime = 0.0f;
                }
                else
                {
                    _rotate_LotterySpeed = 0.0f;
                }
                RotateLottery.transform.eulerAngles = new Vector3(0f, 0f, _rotate_LotteryDeg);
                _rotate_recentPos_x = inputData.pos_x;
                _rotate_recentPos_y = inputData.pos_y;
                _rotate_recentPush = inputData._pushed;

                int targetPocket_twice = (int)(getLotterPocketFromDeg(_rotate_LotteryDeg) * 2.0);
                //Debug.Log("Target Pocket:" + targetPocket_twice);
                send_data[6] |= 0b10000000;
                send_data[5] &= 0b00000111;
                send_data[5] |= (byte)((0b00011111 & targetPocket_twice) << 3);
                if (_rotate_is_rotated)
                {
                    SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCH_2and3, 1.4f);
                    color_alpha_to_0(Rotate_GO_Render);
                    color_alpha_to_0(Rotate_START_Render);
                    color_alpha_to_0(Rotate_BG_Render);
                    touchRotate.stopAnimation();
                    Sound_first_flag = true;
                    AM_caseTime = 0.0;
                    GamePhase = GP_startRotation;
                    AM_startSpeed = (int)(_rotate_LotterySpeed * 0.8f);
                    if (Math.Abs(AM_startSpeed) <= slider_userInputMin.Value)
                    {
                        if (AM_startSpeed >= 0.0)
                        {
                            AM_startSpeed = slider_userInputMin.Value;
                        }
                        else
                        {
                            AM_startSpeed = -slider_userInputMin.Value;
                        }
                    }
                    else if (Math.Abs(AM_startSpeed) >= slider_userInputMax.Value)
                    {
                        if (AM_startSpeed >= 0.0)
                        {
                            AM_startSpeed = slider_userInputMax.Value;
                        }
                        else
                        {
                            AM_startSpeed = -slider_userInputMax.Value;
                        }
                    }
                    rotateSpeed = (int)(AM_startSpeed); 
                    send_data[6] &= 0b01111111;
                    send_data[5] &= 0b00000111;
                    Debug.Log("Start Speed:" + AM_startSpeed);
                    LotteryAction.FadeOut(AM_startSpeed, _rotate_LotteryDeg);
                    userRotateInit();
                }
                break;
            case GP_startRotation:
                if (Sound_first_flag)
                {
                    if (_autoMovingFountain)
                    {
                        if (Fountain_count == 2)
                        {
                            launch_delay = UnityEngine.Random.Range(0.0f,1.0f);
                        }
                        else
                        {
                            launch_delay = 0.0f;
                        }
                        Invoke("F_Launch_Sound", 1.8f + launch_delay);
                        if (Fountain_count != 0)
                        {
                            Invoke("F_LAUNCH_2and3_Sound", launch_delay);
                            //SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCH_2and3, 1.0f);
                        }
                        Sound_first_flag = false;
                    }
                    else if (_autoMovingVenus)
                    {
                        launch_delay = UnityEngine.Random.Range(0.0f, 1.0f);
                        if (Venus_count == 0)
                        {
                            Invoke("V_MEGA_JPCVoice_Sound", 4.2f + launch_delay);
                        }
                        else if (Venus_count == 1)
                        {
                            Invoke("V_GIGA_JPCVoice_Sound", 4.2f + launch_delay);
                        }
                        else if (Venus_count == 2 || Venus_count == 3)
                        {
                            Invoke("V_VENUS_JPCVoice_Sound", 4.2f + launch_delay);
                        }
                        Invoke("F_Launch_Sound", 1.8f + launch_delay);
                        if (Venus_count != 0)
                        {
                            Invoke("F_LAUNCH_2and3_Sound", launch_delay);
                            //SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCH_2and3, 1.0f);
                        }
                        Sound_first_flag = false;
                    }
                }
                if (AM_caseTime < StartAccelTime)
                {
                    //rotateSpeed = (int)((double)AM_startSpeed * (AM_caseTime / StartAccelTime));
                    if (_autoMovingFountain)
                    {
                        if(Fountain_count != 0)
                        {
                            //rotateSpeed = (int)((double)AM_startSpeed * (AM_caseTime / StartAccelTime));
                            if (AM_startSide == 0)
                            {
                                if (AM_startSpeed > 0.0)
                                {
                                    rotateSpeed = LoadSpeed * RotateDirection + (int)((AM_startSpeed * (AM_caseTime / StartAccelTime)) * (Math.Abs(AM_startSpeed) - LoadSpeed) / Math.Abs(AM_startSpeed));
                                }
                                else
                                {
                                    rotateSpeed = (int)((double)AM_startSpeed * (AM_caseTime / StartAccelTime));
                                }
                            }else if (AM_startSide == 1)
                            {
                                if (AM_startSpeed > 0.0)
                                {
                                    rotateSpeed = (int)((double)AM_startSpeed * (AM_caseTime / StartAccelTime));
                                }
                                else
                                {
                                    rotateSpeed = -LoadSpeed * RotateDirection + (int)((AM_startSpeed * (AM_caseTime / StartAccelTime)) * (Math.Abs(AM_startSpeed) - LoadSpeed) / Math.Abs(AM_startSpeed));
                                }
                            }
                        }
                        else
                        {
                            rotateSpeed = AM_startSpeed;
                        }
                    }
                    else if(_autoMovingVenus)
                    {
                        if (Venus_count != 0)
                        {
                            if (AM_startSide == 0)
                            {
                                if (AM_startSpeed > 0.0)
                                {
                                    rotateSpeed = LoadSpeed * RotateDirection + (int)((AM_startSpeed * (AM_caseTime / StartAccelTime)) * (Math.Abs(AM_startSpeed) - LoadSpeed) / Math.Abs(AM_startSpeed));
                                }
                                else
                                {
                                    rotateSpeed = (int)((double)AM_startSpeed * (AM_caseTime / StartAccelTime));
                                }
                            }
                            else if (AM_startSide == 1)
                            {
                                if (AM_startSpeed > 0.0)
                                {
                                    rotateSpeed = (int)((double)AM_startSpeed * (AM_caseTime / StartAccelTime));
                                }
                                else
                                {
                                    rotateSpeed = -LoadSpeed * RotateDirection + (int)((AM_startSpeed * (AM_caseTime / StartAccelTime)) * (Math.Abs(AM_startSpeed) - LoadSpeed) / Math.Abs(AM_startSpeed));
                                }
                            }
                        }
                        else
                        {
                            rotateSpeed = AM_startSpeed;
                        }
                    }
                    else
                    {
                        rotateSpeed = AM_startSpeed;
                    }
                }
                else
                {
                    rotateSpeed = (int)(AM_startSpeed);
                    GamePhase = GP_waitRotation;
                    Sound_first_flag = true;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_waitRotation:
                if (_autoMovingFountain || _autoMovingVenus)
                {
                    if (AM_caseTime < StartWaitTime + launch_delay)
                    {
                        rotateSpeed = (int)(AM_startSpeed);
                    }
                    else
                    {
                        rotateSpeed = (int)(AM_startSpeed);
                        GamePhase = GP_LaunchBall;
                        Sound_first_flag = true;
                        AM_caseTime = 0.0;
                    }
                }
                else
                {
                    if (AM_caseTime < StartWaitTime)
                    {
                        rotateSpeed = (int)(AM_startSpeed);
                    }
                    else
                    {
                        rotateSpeed = (int)(AM_startSpeed);
                        GamePhase = GP_LaunchBall;
                        Sound_first_flag = true;
                        AM_caseTime = 0.0;
                    }
                }
                break;
            case GP_LaunchBall:
                if (_autoMovingFountain)
                {
                    if (Fountain_count == 0)
                    {
                        Invoke("F_Launch1_Sound", 1.7f);
                    }
                    else if (Fountain_count == 1)
                    {
                        Invoke("F_Launch2_Sound", 1.7f);
                    }
                    else if (Fountain_count == 2)
                    {
                        Invoke("F_Launch3_Sound", 1.7f);
                    }
                }
                if (_autoMovingVenus)
                {
                    if (Venus_count <= 3)
                    {
                        Invoke("V_Launch123_Sound", 1.7f);
                    }
                    if (Venus_count == 3)
                    {
                        Invoke("F_Launch3_Sound", 3.7f);
                    }
                }
                if (AM_startSide == 0)
                {
                    send_data[3] |= 0b00000001;
                    Invoke("Reset_R_Ball_Launch", 1.5f);
                }
                else if (AM_startSide == 1)
                {
                    send_data[3] |= 0b00000010;
                    Invoke("Reset_L_Ball_Launch", 1.5f);
                }
                AM_recentSpeed = rotateSpeed;
                if (AM_recentSpeed >= 0)
                {
                    AM_recentSpeedDiff = slow_speed_1.Value - AM_recentSpeed;
                }
                else
                {
                    AM_recentSpeedDiff = -slow_speed_1.Value - AM_recentSpeed;
                }
                AM_BallInFlag = false;
                if (_autoMovingFountain)
                {
                    if (Fountain_count == 0 || Fountain_count == 1)
                    {
                        if ((AM_startSide == 1 && AM_startSpeed > 0.0) || (AM_startSide == 0 && AM_startSpeed < 0.0))
                        {
                            send_data[3] |= 0b11000000;
                        }
                        else
                        {
                            send_data[3] |= 0b01000000;
                        }
                    }
                    else
                    {
                        send_data[3] |= 0b11000000;
                    }
                }
                else
                {
                    send_data[3] |= 0b11000000;
                }
                slowspeed_delay = 2.5f + UnityEngine.Random.Range(0.0f, 2.0f);
                Sound_first_flag = true;
                AM_caseTime = 0.0;
                GamePhase = GP_waitAccel;
                break;
            case GP_waitAccel:
                if (AM_caseTime > 2.0f)
                {
                    AM_caseTime = 0.0;
                    GamePhase = GP_SlowSpeedWait;
                }
                break;
            case GP_SlowSpeedWait:
                if (AM_caseTime < slowspeed_delay)
                {
                    if (Sound_first_flag)
                    {
                        if (UnityEngine.Random.Range(0.0f,1.0f) >= 0.5)
                        {
                            rotateSpeed = (int)((AM_startSpeed) * UnityEngine.Random.Range(1.2f, 1.5f));
                        }
                        else
                        {
                            rotateSpeed = (int)(AM_startSpeed);
                        }
                        Sound_first_flag = false;
                    }
                }
                else
                {
                    Sound_first_flag = true;
                    GamePhase = GP_SlowSpeed1;
                    AM_caseTime = 0.0;
                }
                if (Pocket_In_Num != 0 || AM_BallInFlag)//_Sensor_In_1 || _Sensor_In_2 || _Sensor_In_3 || _Sensor_In_4)
                {
                    rotateSpeed = 0;
                    RotateSpeed_Slider.value = 0.0f;
                    AM_BallInSensor = Pocket_In_Num;
                    /*if (AM_startSpeed > 0)
                    {
                        AM_BallInPocket = BallInPocketDitect(1);
                    }
                    else
                    {
                        AM_BallInPocket = BallInPocketDitect(-1);
                    }*/
                    GamePhase = GP_BallInStop;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_SlowSpeed1:
                if (AM_caseTime < (double)slow_transition_1.Value)
                {
                    rotateSpeed = (int)((double)AM_recentSpeed + (double)AM_recentSpeedDiff * (AM_caseTime / (double)slow_transition_1.Value));
                }
                else
                {
                    if (AM_startSpeed >= 0)
                    {
                        rotateSpeed = slow_speed_1.Value;
                    }
                    else
                    {
                        rotateSpeed = -slow_speed_1.Value;
                    }
                }
                if (AM_caseTime > (double)slow_continue_1.Value)
                {
                    AM_recentSpeed = rotateSpeed;
                    if (AM_recentSpeed >= 0)
                    {
                        AM_recentSpeedDiff = slow_speed_2.Value - AM_recentSpeed;
                    }
                    else
                    {
                        AM_recentSpeedDiff = -slow_speed_2.Value - AM_recentSpeed;
                    }
                    GamePhase = GP_SlowSpeed2;
                    AM_caseTime = 0.0;
                }
                if (Pocket_In_Num != 0 || AM_BallInFlag)//_Sensor_In_1 || _Sensor_In_2 || _Sensor_In_3 || _Sensor_In_4)
                {
                    rotateSpeed = 0;
                    RotateSpeed_Slider.value = 0.0f;
                    AM_BallInSensor = Pocket_In_Num;
                    /*if (AM_startSpeed > 0)
                    {
                        AM_BallInPocket = BallInPocketDitect(1);
                    }
                    else
                    {
                        AM_BallInPocket = BallInPocketDitect(-1);
                    }*/
                    GamePhase = GP_BallInStop;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_SlowSpeed2:
                if (AM_caseTime < (double)slow_transition_2.Value)
                {
                    rotateSpeed = (int)((double)AM_recentSpeed + (double)AM_recentSpeedDiff * (AM_caseTime / (double)slow_transition_2.Value));
                }
                else
                {
                    if (AM_startSpeed >= 0)
                    {
                        rotateSpeed = slow_speed_2.Value;
                    }
                    else
                    {
                        rotateSpeed = -slow_speed_2.Value;
                    }
                }
                if (AM_caseTime > (double)slow_continue_2.Value)
                {
                    AM_recentSpeed = rotateSpeed;
                    if (AM_recentSpeed >= 0)
                    {
                        AM_recentSpeedDiff = slow_speed_3.Value - AM_recentSpeed;
                    }
                    else
                    {
                        AM_recentSpeedDiff = -slow_speed_3.Value - AM_recentSpeed;
                    }
                    GamePhase = GP_SlowSpeed3;
                    AM_caseTime = 0.0;
                }
                if (Pocket_In_Num != 0 || AM_BallInFlag) //_Sensor_In_1 || _Sensor_In_2 || _Sensor_In_3 || _Sensor_In_4)
                {
                    rotateSpeed = 0;
                    RotateSpeed_Slider.value = 0.0f;
                    AM_BallInSensor = Pocket_In_Num;
                    /*if (AM_startSpeed > 0)
                    {
                        AM_BallInPocket = BallInPocketDitect(1);
                    }
                    else
                    {
                        AM_BallInPocket = BallInPocketDitect(-1);
                    }*/
                    GamePhase = GP_BallInStop;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_SlowSpeed3:
                if (AM_caseTime < (double)slow_transition_3.Value)
                {
                    rotateSpeed = (int)((double)AM_recentSpeed + (double)AM_recentSpeedDiff * (AM_caseTime / (double)slow_transition_3.Value));
                }
                else
                {
                    if (AM_startSpeed >= 0)
                    {
                        rotateSpeed = slow_speed_3.Value;
                    }
                    else
                    {
                        rotateSpeed = -slow_speed_3.Value;
                    }
                }
                if (AM_caseTime > (double)slow_continue_3.Value)
                {
                    AM_Error = true;
                    AM_caseTime = 0.0;
                }
                if (Pocket_In_Num != 0 || AM_BallInFlag)//_Sensor_In_1 || _Sensor_In_2 || _Sensor_In_3 || _Sensor_In_4)
                {
                    rotateSpeed = 0;
                    RotateSpeed_Slider.value = 0.0f;
                    AM_BallInSensor = Pocket_In_Num;
                    /*if (AM_startSpeed > 0)
                    {
                        AM_BallInPocket = BallInPocketDitect(1);
                    }
                    else
                    {
                        AM_BallInPocket = BallInPocketDitect(-1);
                    }*/
                    GamePhase = GP_BallInStop;
                    AM_caseTime = 0.0;
                }
                break;
            case GP_BallInStop:
                if (_autoMovingFountain)
                {
                    if (Fountain_count == 0)
                    {
                        if (AM_startSide == 0)
                        {
                            rotateSpeed = LoadSpeed * RotateDirection;
                        }
                        else if (AM_startSide == 1)
                        {
                            rotateSpeed = -LoadSpeed * RotateDirection;
                        }
                        RotateSpeed_Slider.value = rotateSpeed;
                    }
                    else if (Fountain_count == 1)
                    {
                        
                    }
                    else
                    {
                        rotateSpeed = 0;
                        RotateSpeed_Slider.value = 0.0f;
                    }
                }
                else
                {
                    rotateSpeed = 0;
                    RotateSpeed_Slider.value = 0.0f;
                }
                GamePhase = GP_BallInPocketDitect;
                send_data[3] &= 0b00111111;
                break;
            case GP_BallInPocketDitect:
                if (AM_startSpeed >= 0)
                {
                    AM_BallInPocket = BallInPocketDitect(1,AM_BallInSensor);
                }
                else
                {
                    AM_BallInPocket = BallInPocketDitect(-1, AM_BallInSensor);
                }
                Debug.Log(AM_BallInPocket);
                if (AM_BallInPocket == 0)
                {

                }else {
                    string pocket_color;
                    Color32 changeColor = new Color32(0, 0, 0, 255);
                    if (_autoMovingFountain)
                    {
                        Fountain_count++;
                        DebugSystem_text.text = getBallInPocketString(AM_BallInPocket);
                        pocket_color = getBallInPocketColor(AM_BallInPocket);
                        if (pocket_color == "JackPot")
                        {
                            changeColor = new Color32(255, 255, 0, 255);
                            BGM_audio_source.PlayOneShot(WILD_IN_BGM,2.0f);
                        }
                        else if (pocket_color == "Red")
                        {
                            changeColor = new Color32(255, 0, 0, 255);
                            BGM_audio_source.PlayOneShot(RED_IN_BGM, 2.0f);
                        }
                        else if (pocket_color == "Green")
                        {
                            changeColor = new Color32(0, 255, 0, 255);
                            BGM_audio_source.PlayOneShot(GREEN_IN_BGM, 2.0f);
                        }
                        else if (pocket_color == "Blue")
                        {
                            changeColor = new Color32(0, 0, 255, 255);
                            BGM_audio_source.PlayOneShot(BLUE_IN_BGM, 2.0f);
                        }

                        if (Fountain_count == 1) {
                            BallInPocket1_Image.color = changeColor;
                            Fountain_In_Pocket[0] = AM_BallInPocket;
                            Fountain_In_Pocket_Color[0] = pocket_color;
                            SE_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_1);
                            if (pocket_color == "JackPot")
                            {
                                Invoke("F_IN_WILD_1_Sound",2.0f);
                            }
                            else if (pocket_color == "Red")
                            {
                                Invoke("F_IN_SUNRISE_1_Sound", 2.0f);
                            }
                            else if (pocket_color == "Green")
                            {
                                Invoke("F_IN_WIND_1_Sound", 2.0f);
                            }
                            else if (pocket_color == "Blue")
                            {
                                Invoke("F_IN_OCEAN_1_Sound", 2.0f);
                            }
                        }
                        else if (Fountain_count == 2)
                        {
                            BallInPocket2_Image.color = changeColor;
                            Fountain_In_Pocket[1] = AM_BallInPocket;
                            Fountain_In_Pocket_Color[1] = pocket_color;
                            if (!checkFountainContinue(Fountain_In_Pocket_Color[0], Fountain_In_Pocket_Color[1]))
                            {
                                rotateSpeed = 0;
                                RotateSpeed_Slider.value = 0.0f;
                                Invoke("Audio_AllStop",0.2f);
                                BGM_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_3, 1.3f);
                                //_autoMovingFountain = false;
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                //_autoMoving = false;
                                //Invoke("F_NotJPGet_Sound",1.5f);
                                Invoke("JPC_END_50to150_Sound", 3.0f);
                                break;
                            }
                            if (AM_startSide == 0)
                            {
                                rotateSpeed = LoadSpeed * RotateDirection;
                            }
                            else if (AM_startSide == 1)
                            {
                                rotateSpeed = -LoadSpeed * RotateDirection;
                            }
                            RotateSpeed_Slider.value = rotateSpeed;
                            SE_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_1);
                            Invoke("F_Reach_Sound", 2.7f);
                            if (pocket_color == "JackPot")
                            {
                                Invoke("F_2to3_WILD_Sound", 2.8f);
                            }
                            else if (pocket_color == "Red")
                            {
                                Invoke("F_2to3_SUNRISE_Sound", 2.8f);
                            }
                            else if (pocket_color == "Green")
                            {
                                Invoke("F_2to3_WIND_Sound", 2.8f);
                            }
                            else if (pocket_color == "Blue")
                            {
                                Invoke("F_2to3_OCEAN_Sound", 2.8f);
                            }
                        }
                        else if (Fountain_count == 3)
                        {
                            BallInPocket3_Image.color = changeColor;
                            Fountain_In_Pocket[2] = AM_BallInPocket;
                            Fountain_In_Pocket_Color[2] = pocket_color;
                            Invoke("Audio_AllStop", 0.2f);
                            BGM_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_3, 1.5f);
                        }

                        if (Fountain_count >= 3)
                        {
                            if (checkFountain_JP(Fountain_In_Pocket_Color[0], Fountain_In_Pocket_Color[1], Fountain_In_Pocket_Color[2]) == "none")
                            {
                                if (Fountain_In_Pocket_Color[0] == "Red" && Fountain_In_Pocket_Color[1] == "Red")
                                {
                                    Invoke("JPC_END_200to350_Sound", 3.0f);
                                }
                                else
                                {
                                    Invoke("JPC_END_50to150_Sound", 3.0f);
                                }
                            }
                            else
                            {
                                Invoke("F_JPGet_Sound", 5.3f);
                                Invoke("F_JPGetBTF_Sound", 2.8f);
                            }
                            //_autoMovingFountain = false;
                            GamePhase = GP_BallCollect;
                            AM_caseTime = 0.0;
                            //_autoMoving = false;
                        }
                        else {
                            GamePhase = GP_startLottery;
                        }

                    }
                    else if (_autoMovingVenus)
                    {
                        Venus_count++;
                        DebugSystem_text.text = getBallInPocketString(AM_BallInPocket);
                        pocket_color = getBallInPocketColor(AM_BallInPocket);
                        if (pocket_color == "JackPot")
                        {
                            changeColor = new Color32(255, 255, 0, 255);
                        }
                        else if (pocket_color == "Red")
                        {
                            changeColor = new Color32(255, 0, 0, 255);
                        }
                        else if (pocket_color == "Green")
                        {
                            changeColor = new Color32(0, 255, 0, 255);
                        }
                        else if (pocket_color == "Blue")
                        {
                            changeColor = new Color32(0, 0, 255, 255);
                        }

                        if (Venus_count == 1)
                        {
                            Invoke("Audio_AllStop", 0.4f);
                            BGM_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_3, 1.3f);
                            BallInPocket1_Image.color = changeColor;
                            Venus_In_Pocket[0] = AM_BallInPocket;
                            Venus_In_Pocket_Color[0] = pocket_color;
                            if (pocket_color == "JackPot")
                            {
                                Invoke("F_JPGet_Sound", 5.3f);
                                Invoke("F_JPGetBTF_Sound", 2.8f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Red")
                            {
                                Invoke("V_NEXT_VOICE_Sound", 3.0f);
                                Invoke("V_NEXT_SE_1_Sound", 3.0f);
                                Invoke("V_GIGA_BGM_Sound", 5.0f);
                                Invoke("V_NEXT_SE_3_Sound", 6.5f);
                                Invoke("V_POWERUP_Sound", 9.5f);
                                GamePhase = GP_waitSound1;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Green")
                            {
                                Invoke("JPC_END_50to150_Sound", 3.0f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Blue")
                            {
                                Invoke("JPC_END_200to350_Sound", 3.0f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                        }
                        else if (Venus_count == 2)
                        {
                            Invoke("Audio_AllStop", 0.4f);
                            BGM_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_3, 1.3f);
                            BallInPocket2_Image.color = changeColor;
                            Venus_In_Pocket[1] = AM_BallInPocket;
                            Venus_In_Pocket_Color[1] = pocket_color;
                            if (pocket_color == "JackPot")
                            {
                                Invoke("F_JPGet_Sound", 5.3f);
                                Invoke("F_JPGetBTF_Sound", 2.8f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Red")
                            {
                                Invoke("V_NEXT_VOICE_Sound", 3.0f);
                                Invoke("V_NEXT_SE_1_Sound", 3.0f);
                                Invoke("V_VENUS_BGM_Sound", 5.0f);
                                Invoke("V_NEXT_SE_3_Sound", 6.5f);
                                Invoke("V_POWERUP_Sound", 9.5f);
                                GamePhase = GP_waitSound1;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Green")
                            {
                                Invoke("JPC_END_50to150_Sound", 3.0f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Blue")
                            {
                                Invoke("JPC_END_200to350_Sound", 3.0f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                        }
                        else if (Venus_count == 3)
                        {
                            Invoke("Audio_AllStop", 0.4f);
                            BGM_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_3, 1.3f);
                            BallInPocket3_Image.color = changeColor;
                            Venus_In_Pocket[2] = AM_BallInPocket;
                            Venus_In_Pocket_Color[2] = pocket_color;
                            if (pocket_color == "JackPot")
                            {
                                Invoke("F_JPGet_Sound", 5.3f);
                                Invoke("F_JPGetBTF_Sound", 2.8f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Red")
                            {
                                Invoke("V_NEXT_VOICE_Sound", 3.0f);
                                Invoke("V_NEXT_SE_1_Sound", 3.0f);
                                if (BGMToggle.Value)
                                {
                                    Invoke("GC_ROYALJPC_BGM_Sound", 5.0f);
                                }
                                else
                                {
                                    Invoke("V_ALL_BGM_Sound", 5.0f);
                                }
                                Invoke("V_NEXT_SE_3_Sound", 6.5f);
                                Invoke("V_ThisIsGood_Sound", 9.5f);
                                GamePhase = GP_waitSound1;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Green")
                            {
                                Invoke("JPC_END_200to350_Sound", 3.0f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                            else if (pocket_color == "Blue")
                            {
                                Invoke("JPC_END_400to750_Sound", 3.0f);
                                GamePhase = GP_BallCollect;
                                AM_caseTime = 0.0;
                                break;
                            }
                        }
                        else if (Venus_count == 4)
                        {
                            Invoke("Audio_AllStop", 0.4f);
                            BGM_audio_source.PlayOneShot(FOUNTAINJPC_POCKETIN_3, 1.3f);
                            //BallInPocket3_Image.color = changeColor;
                            Venus_In_Pocket[3] = AM_BallInPocket;
                            Venus_In_Pocket_Color[3] = pocket_color;
                            Invoke("F_JPGet_Sound", 5.3f);
                            Invoke("F_JPGetBTF_Sound", 2.8f);
                            GamePhase = GP_BallCollect;
                            AM_caseTime = 0.0;
                            break;
                        }

                        if (Fountain_count >= 3)
                        {
                            if (checkFountain_JP(Fountain_In_Pocket_Color[0], Fountain_In_Pocket_Color[1], Fountain_In_Pocket_Color[2]) == "none")
                            {
                                if (Fountain_In_Pocket_Color[0] == "Red" && Fountain_In_Pocket_Color[1] == "Red")
                                {
                                    Invoke("JPC_END_200to350_Sound", 3.0f);
                                }
                                else
                                {
                                    Invoke("JPC_END_50to150_Sound", 3.0f);
                                }
                            }
                            else
                            {
                                Invoke("F_JPGet_Sound", 5.3f);
                                Invoke("F_JPGetBTF_Sound", 2.8f);
                            }
                            //_autoMovingFountain = false;
                            GamePhase = GP_BallCollect;
                            AM_caseTime = 0.0;
                            //_autoMoving = false;
                        }
                        else
                        {
                            GamePhase = GP_startLottery;
                        }

                    }
                    else {
                        DebugSystem_text.text = getBallInPocketString(AM_BallInPocket);
                        pocket_color = getBallInPocketColor(AM_BallInPocket);
                        if (pocket_color == "JackPot")
                        {
                            BallInPocket1_Image.color = new Color32(255, 255, 0, 255);
                        }
                        else if (pocket_color == "Red")
                        {
                            BallInPocket1_Image.color = new Color32(255, 0, 0, 255);
                        }
                        else if (pocket_color == "Green")
                        {
                            BallInPocket1_Image.color = new Color32(0, 255, 0, 255);
                        }
                        else if (pocket_color == "Blue")
                        {
                            BallInPocket1_Image.color = new Color32(0, 0, 255, 255);
                        }

                        GamePhase = 0;
                        _autoMoving = false;
                    }
                }
                break;
            case GP_waitSound1:
                if (AM_caseTime > waitSound1_time)
                {
                    GamePhase = GP_startLottery;
                    AM_caseTime = 0.0f;
                }
                break;
            case GP_waitSound2:
                if (AM_caseTime > waitSound2_time)
                {
                    GamePhase = GP_startRotation;
                    AM_caseTime = 0.0f;
                }
                break;
            case GP_BallCollect:
                if (AM_caseTime > 7.0f)
                {
                    if (AM_startSide == 0)
                    {
                        if (!_Sensor_Ball_R)
                        {
                            rotateSpeed = LoadSpeed * RotateDirection;
                        }
                        else
                        {
                            GamePhase = GP_WelcomeVF;
                            AM_caseTime = 0.0;
                            rotateSpeed = 0;
                        }
                    }
                    else
                    {
                        if (!_Sensor_Ball_L)
                        {
                            rotateSpeed = -LoadSpeed * RotateDirection;
                        }
                        else
                        {
                            GamePhase = GP_WelcomeVF;
                            AM_caseTime = 0.0;
                            rotateSpeed = 0;
                        }
                    }
                }
                Sound_first_flag = true;
                break;

            case GP_WelcomeVF:
                if (AM_caseTime > 0.5 && Sound_first_flag)
                {
                    SE_audio_source.PlayOneShot(WELCOME_VF_BGM, 2.5f);
                    Sound_first_flag = false;
                }
                else if (AM_caseTime > 5.0)
                {
                    UICanvas.SetActive(true);
                    GamePhase = 0;
                    _autoMoving = false;
                    _autoMovingFountain = false;
                    _autoMovingVenus = false;
                    AM_startTime = 0.0;
                    Sound_first_flag = true;
                }
                break;
            default:
                break;
        }

        if (_autoMoving && _autoMovingVenus)
        {
            if (GamePhase == GP_SlowSpeedWait || GamePhase == GP_SlowSpeed1 || GamePhase == GP_SlowSpeed2 || GamePhase == GP_SlowSpeed3)
            {
                float Detailed_modify = 0.0f;
                if (AM_startSpeed > 0.0)
                {
                    Detailed_modify = Pocket_Num_Detailed + 0.5f;
                }
                else
                {
                    Detailed_modify = Pocket_Num_Detailed - 0.5f;
                }
                if (Detailed_modify > 12.0)
                {
                    Detailed_modify -= 12.0f;
                }
                else if (Detailed_modify < 0.0)
                {
                    Detailed_modify += 12.0f;
                }
                if (Detailed_modify > 3.0f && Detailed_modify < 10.0f)
                {
                    JP_audio_source.clip = VENUSJPC_JPPOCKET_BGM;
                    JP_audio_source.loop = true;
                    JP_audio_source.volume = 0.0f;
                    if (JP_Audio_reset && !(Detailed_modify >= 5.0 && Detailed_modify <= 7.0))
                    {
                        JP_audio_source.Play();
                        JP_Audio_reset = false;
                    }
                    if ((Detailed_modify >= 5.0 && Detailed_modify <= 7.0) && !JP_Audio_reset)
                    {
                        JP_audio_source.Stop();
                        JP_Audio_reset = true;
                    }
                }
                else
                {
                    float diff = 0.0f;
                    if (AM_startSpeed > 0.0)
                    {
                        if (Detailed_modify <= 3.0)
                        {
                            diff = Detailed_modify - 0.5f;
                        }
                        else if (Detailed_modify >= 10.0)
                        {
                            diff = 2.0f - (Detailed_modify - 10.0f) + 0.5f;
                        }
                        else
                        {
                            diff = 3.125f;
                        }
                    }
                    else
                    {
                        if (Detailed_modify <= 3.0)
                        {
                            diff = Detailed_modify - 0.5f;
                        }
                        else if (Detailed_modify >= 10.0)
                        {
                            diff = 2.0f - (Detailed_modify - 10.0f) + 0.5f;
                        }
                        else
                        {
                            diff = 3.125f;
                        }
                    }
                    JP_audio_source.volume = 0.05f + 0.20f - (diff / 12.5f);
                }
            }
            else
            {
                JP_audio_source.volume = 0.0f;
                JP_audio_source.Stop();
            }
        }

        //DebugSystem_text.text = GamePhase.ToString();
        if (AM_Error)
        {
            SE_audio_source.Stop();
            BGM_audio_source.Stop();
            JP_audio_source.Stop();
            //Error_audio_source.PlayOneShot(KONAMI_ERROR_BGM,1.0f);
            enableToggle.DisableToggle();
            DebugSystem_text.text = "Error";
            AM_Error = false;
        }
        //DebugSystem_text.text = AM_startSpeed.ToString();

        //Lottery.transform.eulerAngles = new Vector3(0f, 0f, RotateSpeed_Slider.value + Lotter_offset);

        lottery_deg = getLotterDegree(lottery_deg);
        Lottery.transform.eulerAngles = new Vector3(0f, 0f, lottery_deg + Lotter_offset);

        if (enableToggle.Value)
        {
            Error_audio_source.Stop();
            send_data[0] |= 0b10000000;
            if (!_autoMoving)
            {
                manualToggle.EnableToggle();
                BGMToggle.EnableToggle();
                RotateSpeed_Slider.enabled = true;
                RotateSpeed_Slider.interactable = true;
                SliderBackGround.color = ENABLE_COLOR;
                ControllerBackGround.color = ENABLE_COLOR;
                slow_speed_1.EnableSlider();
                slow_speed_2.EnableSlider();
                slow_speed_3.EnableSlider();
                slow_continue_1.EnableSlider();
                slow_continue_2.EnableSlider();
                slow_continue_3.EnableSlider();
                slow_transition_1.EnableSlider();
                slow_transition_2.EnableSlider();
                slow_transition_3.EnableSlider();
                slider_userInputMin.EnableSlider();
                slider_userInputMax.EnableSlider();
                startOneTime.EnableButton();
                startFountain.EnableButton();
                startVenus.EnableButton();
                launchSide.EnableDropDown();
            }
            if (startOneTime.pushed())
            {
                Debug.Log("pushed One Time");
                _startOneTime = true;
            }
            if (startFountain.pushed())
            {
                Debug.Log("pushed Fountain");
                _startFountain = true;
            }
            if (startVenus.pushed())
            {
                Debug.Log("pushed Venus");
                _startVenus = true;
            }
            if (_startFountain)
            {
                Debug.Log("Fountain");
                BallInPocket1_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BallInPocket2_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BallInPocket3_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                RotateSpeed_Slider.enabled = false;
                RotateSpeed_Slider.interactable = false;
                ControllerBackGround.color = DISABLE_COLOR;
                if (manualToggle.Value)
                {
                    manualToggle.SwitchToggle();
                }
                manualToggle.DisableToggle();
                BGMToggle.DisableToggle();
                slow_speed_1.DisableSlider();
                slow_speed_2.DisableSlider();
                slow_speed_3.DisableSlider();
                slow_continue_1.DisableSlider();
                slow_continue_2.DisableSlider();
                slow_continue_3.DisableSlider();
                slow_transition_1.DisableSlider();
                slow_transition_2.DisableSlider();
                slow_transition_3.DisableSlider();
                slider_userInputMin.DisableSlider();
                slider_userInputMax.DisableSlider();
                startOneTime.DisableButton();
                startFountain.DisableButton();
                startVenus.DisableButton();
                launchSide.DisableDropDown();
                _startFountain = false;
                _autoMovingFountain = true;
                GamePhase = GP_startFountain;
                _autoMoving = true;
                AM_startSpeed = (int)RotateSpeed_Slider.value;
                Fountain_count = 0;
                Venus_count = 0;
                Fountain_In_Pocket[0] = 0;
                Fountain_In_Pocket[1] = 0;
                Fountain_In_Pocket[2] = 0;
                Fountain_In_Pocket_Color[0] = "";
                Fountain_In_Pocket_Color[1] = "";
                Fountain_In_Pocket_Color[2] = "";
                Venus_In_Pocket[0] = 0;
                Venus_In_Pocket[1] = 0;
                Venus_In_Pocket[2] = 0;
                Venus_In_Pocket[3] = 0;
                Venus_In_Pocket_Color[0] = "";
                Venus_In_Pocket_Color[1] = "";
                Venus_In_Pocket_Color[2] = "";
                Venus_In_Pocket_Color[3] = "";
                UICanvas.SetActive(false);
            }
            if (_startVenus)
            {
                Debug.Log("Venus");
                BallInPocket1_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BallInPocket2_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BallInPocket3_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                RotateSpeed_Slider.enabled = false;
                RotateSpeed_Slider.interactable = false;
                ControllerBackGround.color = DISABLE_COLOR;
                if (manualToggle.Value)
                {
                    manualToggle.SwitchToggle();
                }
                manualToggle.DisableToggle();
                BGMToggle.DisableToggle();
                slow_speed_1.DisableSlider();
                slow_speed_2.DisableSlider();
                slow_speed_3.DisableSlider();
                slow_continue_1.DisableSlider();
                slow_continue_2.DisableSlider();
                slow_continue_3.DisableSlider();
                slow_transition_1.DisableSlider();
                slow_transition_2.DisableSlider();
                slow_transition_3.DisableSlider();
                slider_userInputMin.DisableSlider();
                slider_userInputMax.DisableSlider();
                startOneTime.DisableButton();
                startFountain.DisableButton();
                startVenus.DisableButton();
                launchSide.DisableDropDown();
                _startVenus = false;
                _autoMovingVenus = true;
                GamePhase = GP_startVenus;
                _autoMoving = true;
                AM_startSpeed = (int)RotateSpeed_Slider.value;
                Fountain_count = 0;
                Venus_count = 0;
                Fountain_In_Pocket[0] = 0;
                Fountain_In_Pocket[1] = 0;
                Fountain_In_Pocket[2] = 0;
                Fountain_In_Pocket_Color[0] = "";
                Fountain_In_Pocket_Color[1] = "";
                Fountain_In_Pocket_Color[2] = "";
                Venus_In_Pocket[0] = 0;
                Venus_In_Pocket[1] = 0;
                Venus_In_Pocket[2] = 0;
                Venus_In_Pocket[3] = 0;
                Venus_In_Pocket_Color[0] = "";
                Venus_In_Pocket_Color[1] = "";
                Venus_In_Pocket_Color[2] = "";
                Venus_In_Pocket_Color[3] = "";
                UICanvas.SetActive(false);
            }
            if (_startOneTime)
            {
                Debug.Log("OneTime");
                BallInPocket1_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BallInPocket2_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                BallInPocket3_Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                RotateSpeed_Slider.enabled = false;
                RotateSpeed_Slider.interactable = false;
                ControllerBackGround.color = DISABLE_COLOR;
                if (manualToggle.Value)
                {
                    manualToggle.SwitchToggle();
                }
                manualToggle.DisableToggle();
                BGMToggle.DisableToggle();
                slow_speed_1.DisableSlider();
                slow_speed_2.DisableSlider();
                slow_speed_3.DisableSlider();
                slow_continue_1.DisableSlider();
                slow_continue_2.DisableSlider();
                slow_continue_3.DisableSlider();
                slow_transition_1.DisableSlider();
                slow_transition_2.DisableSlider();
                slow_transition_3.DisableSlider();
                slider_userInputMin.DisableSlider();
                slider_userInputMax.DisableSlider();
                startOneTime.DisableButton();
                startFountain.DisableButton();
                startVenus.DisableButton();
                launchSide.DisableDropDown();
                _startOneTime = false;
                GamePhase = GP_startLottery;
                _autoMoving = true;
                _autoMovingFountain = false;
                _autoMovingVenus = false;
                AM_startSpeed = (int)RotateSpeed_Slider.value;
            }
        }
        else
        {
            send_data[0] &= 0b01111111;
            send_data[5] &= 0b11111100;
            send_data[3] &= 0b11111100;
            manualToggle.DisableToggle();
            BGMToggle.DisableToggle();
            RotateSpeed_Slider.enabled = false;
            RotateSpeed_Slider.interactable = false;
            RotateSpeed_Slider.value = 0;
            rotateSpeed = (int)RotateSpeed_Slider.value;

            SliderBackGround.color = DISABLE_COLOR;
            ControllerBackGround.color = DISABLE_COLOR;

            slow_speed_1.DisableSlider();
            slow_speed_2.DisableSlider();
            slow_speed_3.DisableSlider();
            slow_continue_1.DisableSlider();
            slow_continue_2.DisableSlider();
            slow_continue_3.DisableSlider();
            slow_transition_1.DisableSlider();
            slow_transition_2.DisableSlider();
            slow_transition_3.DisableSlider();
            slider_userInputMin.DisableSlider();
            slider_userInputMax.DisableSlider();
            startOneTime.DisableButton();
            startFountain.DisableButton();
            startVenus.DisableButton();
            launchSide.DisableDropDown();

            _startFountain = false;
            _autoMovingFountain = false;
            _startVenus = false;
            _autoMovingVenus = false;
            _startOneTime = false;
            _autoMoving = false;
            GamePhase = 0;

            userRotateInit();
            F_START_VIDEO.Stop();
            V_START_VIDEO.Stop();
            SE_audio_source.Stop();
            BGM_audio_source.Stop();
            JP_audio_source.Stop();
        }
        if (manualToggle.Value)
        {
            send_data[0] |= 0b00100000;
            launch_R_Motor.EnableButton();
            launch_L_Motor.EnableButton();
            launch_R_Ball.EnableButton();
            launch_L_Ball.EnableButton();
            rotateSpeed = (int)RotateSpeed_Slider.value;

            if (launch_R_Motor.buttonDown)
            {
                send_data[5] |= 0b00000001;
            }
            else
            {
                send_data[5] &= 0b11111110;
            }
            if (launch_L_Motor.buttonDown)
            {
                send_data[5] |= 0b00000010;
            }
            else
            {
                send_data[5] &= 0b11111101;
            }
            if (launch_R_Ball.pushed())
            {
                send_data[3] |= 0b00000001;
                Invoke("Reset_R_Ball_Launch", 1.0f);
            }
            if (launch_L_Ball.pushed())
            {
                send_data[3] |= 0b00000010;
                Invoke("Reset_L_Ball_Launch", 1.0f);
            }
        }
        else
        {
            send_data[0] &= 0b11011111;
            launch_R_Motor.DisableButton();
            launch_L_Motor.DisableButton();
            launch_R_Ball.DisableButton();
            launch_L_Ball.DisableButton();
        }

        if (_autoMoving)
        {
            RotateSpeed_Slider.value = (float)rotateSpeed;
        }
        RealRotateSpeed_text.text = realRotateSpeed.ToString();
        TargetRotateSpeed_text.text = ((int)(RotateSpeed_Slider.value)).ToString();
        if (rotateSpeed > 0)
        {
            send_data[6] &= 0b11110000;
            send_data[6] |= 0b00000100;
            send_data[6] |= (byte)(((rotateSpeed) >> 8) & 0b00000011);
            send_data[7] = (byte)((rotateSpeed) & 0xff);
        }
        else if (rotateSpeed < 0)
        {
            send_data[6] &= 0b11110000;
            send_data[6] |= 0b00001000;
            send_data[6] |= (byte)(((-rotateSpeed) >> 8) & 0b00000011);
            send_data[7] = (byte)((-rotateSpeed) & 0xff);
        }
        else
        {
            send_data[6] &= 0b11110000;
            send_data[7] = 0b00000000;
        }
    }

    (float rotateSpeed, float rotateDegree, bool _is_rotate) getRotateLottery(float nowLotteryDegree, float nowLotterySpeed, float deltaTime, float touchAngle, bool _in_lottery, bool _pushed)
    {
        float return_rotateSpeed = 0.0f;
        float return_rotateDegree = 0.0f;
        bool return_is_rotate = false;

        if (!_rotate_firstTouchedLottery)
        {
            if(_pushed && _in_lottery)
            {
                _rotate_firstTouchedLottery = true;
                _rotate_firstTouchAngle = touchAngle;
            }
        }
        else
        {
            if (_pushed)
            {
                if (_in_lottery)
                {
                    if (_rotate_TouchedOut)
                    {
                        _rotate_TouchedOutAngleSum += _rotate_TouchedOutAngleDiff;
                    }
                    return_rotateDegree = touchAngle - _rotate_firstTouchAngle - _rotate_TouchedOutAngleSum;
                    if(return_rotateDegree <= -180.0f)
                    {
                        return_rotateDegree += 360.0f;
                    }
                    if (Math.Abs(return_rotateDegree) >= 110.0f)
                    {
                        return_is_rotate = true;
                    }
                    return_rotateSpeed = (return_rotateDegree - nowLotteryDegree) / deltaTime;
                    _rotate_TouchedOut = false;
                }
                else
                {
                    return_rotateDegree = nowLotteryDegree;
                    if (!_rotate_TouchedOut)
                    {
                        _rotate_TouchedOutAngle = touchAngle;
                        _rotate_TouchedOut = true;
                    }
                    _rotate_TouchedOutAngleDiff = touchAngle - _rotate_TouchedOutAngle;
                }
                _rotate_TouchRelease = false;
            }
            else
            {
                if (!_rotate_TouchRelease && !_rotate_TouchedOut)
                {
                    _rotate_TouchedOutAngle = touchAngle;
                    _rotate_TouchRelease = true;
                    _rotate_TouchedOut = true;
                }
                _rotate_TouchedOutAngleDiff = touchAngle - _rotate_TouchedOutAngle;
                return_rotateDegree = nowLotteryDegree;
                if (Math.Abs(nowLotterySpeed) >= 10.0f)
                {
                    return_is_rotate = true;
                    return_rotateSpeed = nowLotterySpeed;
                }
            }
        }


        return (return_rotateSpeed, return_rotateDegree, return_is_rotate);
    }
    
    (bool _pos_in_lottery, float angle) getInputAngle(float pos_x, float pos_y)
    {
        bool _in_lottery;
        float calc_angle;

        float offset_pos_x = pos_x - Screen.width / 2.0f;
        float offset_pos_y = pos_y - (Screen.height / 2.0f + lotteryOrigin_diff * (Screen.width/1920.0f));
        //Debug.Log("x:" + offset_pos_x + " y:" + offset_pos_y);
        float offset_lotterSize_outside = lotterySize_outside * (Screen.width / 1920.0f);
        float offset_lotterSize_inside = lotterySize_inside * (Screen.width / 1920.0f);

        float distanceFromOrigin = (float)Math.Sqrt(offset_pos_x * offset_pos_x + offset_pos_y * offset_pos_y);

        if(distanceFromOrigin >= offset_lotterSize_inside/2.0f && distanceFromOrigin <= offset_lotterSize_outside/2.0f)
        {
            _in_lottery = true;
        }else
        {
            _in_lottery = false;
        }

        calc_angle = (float)(Math.Atan2(offset_pos_y, offset_pos_x) * (180.0/Math.PI));

        return (_in_lottery, calc_angle);
    }

    (float pos_x, float pos_y, bool _pushed) getInputData(bool touch)
    {
        float x = 0;
        float y = 0;
        bool pushed = false;
        if (touch)
        {

        }
        else
        {
            pushed = Input.GetMouseButton(0);
            Vector3 position = Input.mousePosition;
            x = position.x;
            y = position.y;
        }

        return (x, y, pushed);
    }

    void userRotateInit()
    {
        _rotate_firstTouchedLottery = false;
        _rotate_TouchedOut = false;
        _rotate_firstTouchAngle = 0.0f;
        _rotate_TouchedOutAngle = 0.0f;
        _rotate_TouchedOutAngleDiff = 0.0f;
        _rotate_TouchedOutAngleSum = 0.0f;
        _rotate_TouchRelease = false;
        _rotate_LotteryDeg = 0.0f;
        _rotate_LotterySpeed = 0.0f;
        _rotate_recentPos_x = 0.0f;
        _rotate_recentPos_y = 0.0f;
        _rotate_recentPush = false;
        _rotate_deltaTime = 0.0f;
        _rotate_is_rotated = false;
        RotateLottery.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        color_alpha_to_0(Rotate_GO_Render);
        color_alpha_to_0(Rotate_START_Render);
        color_alpha_to_0(Rotate_BG_Render);
        color_alpha_to_0(RotateLottery_Render);
    }
    void video_end(VideoPlayer vp)
    {
        if (vp.name == "F_START_VIDEO")
        {
            F_START_VIDEO.Stop();
            //Debug.Log(vp.name + " stop");
        }
        if (vp.name == "V_START_VIDEO")
        {
            V_START_VIDEO.Stop();
            //Debug.Log(vp.name + " stop");
        }
        //if (vp.name == "F_ROTATE_VIDEO")
        //{
        //    F_ROTATE_VIDEO.Stop();
        //    //Debug.Log(vp.name + " stop");
        //}
    }

    bool checkFountainContinue(string first_pocket, string second_pocket)
    {
        if (first_pocket == "JackPot") {
            return true;
        }
        else if(second_pocket == "JackPot"){
            return true;
        }
        else
        {
            if (first_pocket == second_pocket) {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    string checkFountain_JP(string first_pocket, string second_pocket , string third_pocket)
    {
        string return_string = "";

        if (first_pocket == "JackPot")
        {
            if (second_pocket == "JackPot")
            {

            }
            else
            {
                if (second_pocket != third_pocket)
                {
                    return "none";
                }
            }
        }else if (second_pocket == "JackPot")
        {
            if (first_pocket == "JackPot")
            {

            }
            else
            {
                if (first_pocket != third_pocket)
                {
                    return "none";
                }
            }
        }else if (third_pocket == "JackPot")
        {
            if (first_pocket == "JackPot")
            {

            }
            else
            {
                if (first_pocket != second_pocket)
                {
                    return "none";
                }
            }
        }
        else
        {
            if (first_pocket == second_pocket && first_pocket == third_pocket)
            {

            }
            else
            {
                return "none";
            }
        }

        if (first_pocket == "JackPot" && second_pocket == "JackPot" && third_pocket == "JackPot")
        {
            return_string = "Fountain";
        }
        else
        {
            if (first_pocket == "red" || second_pocket == "red" || third_pocket == "red")
            {
                return_string = "Sunrise";
            }else if (first_pocket == "green" || second_pocket == "green" || third_pocket == "green")
            {
                return_string = "Wind";
            }else if (first_pocket == "blue" || second_pocket == "blue" || third_pocket == "blue")
            {
                return_string = "Ocean";
            }
        }

        return return_string;
    }

    UnityAction<float> Slider_action = (float value) =>
    {
        //Debug.Log(value);
        //rotateSpeed = (int)value;
    };

    void Audio_AllStop()
    {
        SE_audio_source.Stop();
    }
    void JPC_END_50to150_Sound()
    {
        SE_audio_source.PlayOneShot(JPC_END_50TO150WIN, 1.3f);
    }
    void JPC_END_200to350_Sound()
    {
        SE_audio_source.PlayOneShot(JPC_END_200TO350WIN, 1.3f);
    }
    void JPC_END_400to750_Sound()
    {
        SE_audio_source.PlayOneShot(JPC_END_400TO750WIN, 1.3f);
    }
    void JPC_END_800_Sound()
    {
        SE_audio_source.PlayOneShot(JPC_END_800WIN, 1.3f);
    }

    void GC_ROYALJPC_BGM_Sound()
    {
        SE_audio_source.clip = GC_ROYALJPC_BGM;
        SE_audio_source.loop = true;
        SE_audio_source.Play();
    }
    void V_ThisIsGood_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_THISISGOOD_BGM, 1.3f);
    }
    void V_POWERUP_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_POWERUP_BGM, 1.3f);
    }
    void V_NEXT_VOICE_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_NEXT_VOICE_BGM, 1.3f);
    }
    void V_NEXT_SE_1_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_NEXT_SE_1_BGM, 1.3f);
    }
    void V_NEXT_SE_2_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_NEXT_SE_2_BGM, 1.3f);
    }
    void V_NEXT_SE_3_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_NEXT_SE_3_BGM, 1.3f);
    }
    void V_Launch123_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_123_START_BGM, 1.3f);
    }
    void V_VENUS_JPCVoice_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_VENUS_VOICE, 1.3f);
    }
    void V_GIGA_JPCVoice_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_GIGA_VOICE, 1.3f);
    }
    void V_MEGA_JPCVoice_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_MEGA_VOICE, 1.3f);
    }
    void V_ALL_BGM_Sound()
    {
        SE_audio_source.clip = VENUSJPC_ALL_BGM;
        SE_audio_source.loop = true;
        SE_audio_source.Play();
    }
    void V_VENUS_BGM_Sound()
    {
        SE_audio_source.clip = VENUSJPC_VENUS_BGM;
        SE_audio_source.loop = true;
        SE_audio_source.Play();
    }
    void V_GIGA_BGM_Sound()
    {
        SE_audio_source.clip = VENUSJPC_GIGA_BGM;
        SE_audio_source.loop = true;
        SE_audio_source.Play();
    }
    void V_MEGA_BGM_Sound()
    {
        SE_audio_source.clip = VENUSJPC_MEGA_BGM;
        SE_audio_source.loop = true;
        SE_audio_source.Play();
    }
    void V_START_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_START_BGM, 1.3f);
    }
    void F_LAUNCH_2and3_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCH_2and3, 1.4f);
    }
    void V_STARTEND_Sound()
    {
        SE_audio_source.PlayOneShot(VENUSJPC_STARTEND_BGM, 1.3f);
    }
    void F_Reach_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_REACH_BGM, 1.3f);
    }
    void F_2to3_OCEAN_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_OCEAN_2TO3_BGM, 1.3f);
    }
    void F_2to3_WIND_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_WIND_2TO3_BGM, 1.3f);
    }
    void F_2to3_SUNRISE_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_SUNRISE_2TO3_BGM, 1.3f);
    }
    void F_2to3_WILD_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_WILD_2TO3_BGM, 1.3f);
    }
    void F_IN_OCEAN_1_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_IN_OCEAN_1_BGM, 1.3f);
    }
    void F_IN_WIND_1_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_IN_WIND_1_BGM, 1.3f);
    }
    void F_IN_SUNRISE_1_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_IN_SUNRISE_1_BGM, 1.3f);
    }
    void F_IN_WILD_1_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_IN_WILD_1_BGM, 1.3f);
    }
    void F_START_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_START_BGM, 1.3f);
    }
    void F_STARTEND_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_STARTEND_BGM, 1.3f);
    }
    void DISPLAY_MOVE_Sound()
    {
        SE_audio_source.PlayOneShot(DISPLAY_MOVE, 1.0f);
    }
    void F_START_St_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_START_ST_BGM, 1.3f);
    }
    void F_2to3_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_2TO3_BGM, 0.5f);
    }
    void F_3_BGM_Sound()
    {
        SE_audio_source.clip = FOUNTAINJPC_3_BGM;
        SE_audio_source.loop = true;
        SE_audio_source.Play();
    }

    void F_NotJPGet_Sound()
    {
        SE_audio_source.clip = FOUNTAINJPC_NOTJPGET;
        SE_audio_source.loop = false;
        SE_audio_source.Play();
    }
    void F_JPGet_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_JPGET);
    }
    void F_JPGetBTF_Sound()
    {
        SE_audio_source.clip = FOUNTAINJPC_JPGET_BTF;
        SE_audio_source.loop = false;
        SE_audio_source.Play();
    }
    void F_Launch_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCHBALL,1.5f);
    }

    void F_Launch1_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCHBALL_FIRST,2.0f);
    }
    void F_Launch2_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCHBALL_SECOND,1.5f);
    }
    void F_Launch3_Sound()
    {
        SE_audio_source.PlayOneShot(FOUNTAINJPC_LAUNCHBALL_THIRD,2.0f);
    }

    void ResetButton_action()
    {
        RotateSpeed_Slider.value = 0;
    }

    void Reset_R_Ball_Launch()
    {
        send_data[3] &= 0b11111110;
    }
    void Reset_L_Ball_Launch()
    {
        send_data[3] &= 0b11111101;
    }
    void LaunchSound1()
    {
        SE_audio_source.clip = GAME_START_2_Sound;
        SE_audio_source.loop = false;
        SE_audio_source.Play();
        
    }
    void LaunchSound2()
    {
        SE_audio_source.clip = ROUNCH_BALL_Sound;
        SE_audio_source.loop = false;
        SE_audio_source.Play();

    }

    string getBallInPocketColor(int in_pocket)
    {
        string return_string = "";

        switch (in_pocket)
        {
            case 1:
                return_string = "JackPot";
                break;
            case 2:
                return_string = "Green";
                break;
            case 3:
                return_string = "Blue";
                break;
            case 4:
                return_string = "Red";
                break;
            case 5:
                return_string = "Green";
                break;
            case 6:
                return_string = "Blue";
                break;
            case 7:
                return_string = "Red";
                break;
            case 8:
                return_string = "Blue";
                break;
            case 9:
                return_string = "Green";
                break;
            case 10:
                return_string = "Red";
                break;
            case 11:
                return_string = "Blue";
                break;
            case 12:
                return_string = "Green";
                break;

        }

        return return_string;
    }
    string getBallInPocketString(int in_pocket)
    {
        string return_string = "";

        switch (in_pocket)
        {
            case 1:
                return_string = "JackPot (1)";
                break;
            case 2:
                return_string = "Green (2)";
                break;
            case 3:
                return_string = "Blue (3)";
                break;
            case 4:
                return_string = "Red (4)";
                break;
            case 5:
                return_string = "Green (5)";
                break;
            case 6:
                return_string = "Blue (6)";
                break;
            case 7:
                return_string = "Red (7)";
                break;
            case 8:
                return_string = "Blue (8)";
                break;
            case 9:
                return_string = "Green (9)";
                break;
            case 10:
                return_string = "Red (10)";
                break;
            case 11:
                return_string = "Blue (11)";
                break;
            case 12:
                return_string = "Green (12)";
                break;

        }

        return return_string;
    }

    static int ditect_wait = 0;
    const uint ditect_wait_count = 1;
    int BallInPocketDitect(int LotteryDirection, int ball_in_sensor)
    {
        int return_pocket = 0;
        int pocket_adjust = 0;
        //int pocketPosition = (int)(lottery_deg + Lotter_offset);
        //Debug.Log(ball_in_sensor);
        //Debug.Log(LotteryDirection);
        if (LotteryDirection > 0)
        {
            if (ball_in_sensor == 3)
            {
                ditect_wait++;
                if (ditect_wait > ditect_wait_count)
                {
                    ditect_wait = 0;
                }
                else {
                    return 0;
                }
            }
        }
        else
        {
            if (ball_in_sensor == 2)
            {
                ditect_wait++;
                if (ditect_wait > ditect_wait_count)
                {
                    ditect_wait = 0;
                }
                else
                {
                    return 0;
                }
            }
        }
        //Debug.Log("over");

        if (ball_in_sensor == 1)
        {
            pocket_adjust = -1;
        }else if (ball_in_sensor == 2)
        {
            pocket_adjust = 0;
        }else if (ball_in_sensor == 3)
        {
            pocket_adjust = 1;
        }else if (ball_in_sensor == 4)
        {
            pocket_adjust = 2;
        }


        return_pocket = Pocket_Num + pocket_adjust;

        if (return_pocket == 13)
        {
            return_pocket = 1;
        }else if (return_pocket == 14)
        {
            return_pocket = 2;
        }
        else if (return_pocket == 0)
        {
            return_pocket = 12;
            //return_pocket = 4;
        }

        return return_pocket;
    }

    float getLotterDegree(float nowDeg)
    {
        float return_deg = nowDeg;
        int ditect_num = 0;

        /*if (_Sensor_Pos_1)
        {
            //ditect_num += _Sensor_Pos_2 ? 1 : 0;
            //ditect_num += _Sensor_Pos_3 ? 2 : 0;
            //ditect_num += _Sensor_Pos_4 ? 4 : 0;
            //ditect_num += _Sensor_Pos_5 ? 8 : 0;
            ditect_num = Pocket_Num;
            if (ditect_num == 0)
            {
                return return_deg;
            }
            return_deg = ditect_num * 30;
        }*/
        ditect_num = Pocket_Num;
        if (ditect_num == 0)
        {
            return return_deg;
        }
        return_deg = ditect_num * 30;

        return return_deg;
    }

    float getLotterPocketFromDeg(float nowDeg)
    {
        int remainder = 0;
        float return_deg = 0.0f;

        if (nowDeg > 0.0f)
        {
            remainder = (int)Math.Truncate(nowDeg / 7.5f);
            return_deg = (float)Math.Truncate((remainder + 1) / 2.0f) * 0.5f + 0.5f;
        } 
        else if (nowDeg < 0.0f)
        {
            remainder = (int)Math.Truncate(-nowDeg / 7.5f);
            return_deg = 13.0f - ((float)Math.Truncate((remainder + 1) / 2.0f) * 0.5f + 0.5f);
            if(return_deg > 12.0f)
            {
                return_deg = 0.5f;
            }
        }
        else
        {
            return_deg = 0.5f;
        }
        return_deg += 6.0f;
        if (return_deg > 12.0f)
        {
            return_deg -= 12.0f;
        }

        return return_deg;
    }

    string debug_data = "";
    static int send_count = 0;
    byte[] send_data_small = new byte[5] {
        0,0,0,0,0
    };
    void OnDataReceived(string message)
    {
        Debug.Log(message);
        var in_data = message.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        string[] data = new string[] { };
        int pocket;
        float pocket_detailed;
        int pocket_in;
        if (in_data[0] == null)
        {
            return;
        }
        //Debug.Log(in_data[0]);

        for (int i=0; i<in_data.Length;i++)
        {
            if (in_data[i].Contains("flush"))
            {
                debug_flush = in_data[i] + "\n";
                data = in_data[i].Split(new string[] {" "}, System.StringSplitOptions.None);
                //medal_win_struct = OpMode_GetValiablesData(2,0,1);
                //send_data[7] = Convert.ToByte(medal_win_struct.Data);
                //serialHandler.Write(send_data,0,8);
                pocket = int.Parse(data[2]);
                pocket_in = int.Parse(data[3]);
                pocket_detailed = int.Parse(data[4]) / 2.0f;
                if (pocket != null)
                {
                    Pocket_Num = pocket;
                }
                if(pocket_in != null)
                {
                    if (pocket_in != 0)
                    {
                        AM_BallInFlag = true;
                    }
                    Pocket_In_Num = pocket_in;
                }
                if (pocket_detailed != null)
                {
                    Pocket_Num_Detailed = pocket_detailed;
                }
            }
            if (in_data[i].Contains("MD(11)"))
            {
                debug_MD1 = in_data[i] + "\n";
                MD1_data = in_data[i].Split(new string[] { "][" }, System.StringSplitOptions.None);
                if (MD1_data[2] != null && MD1_data[3] != null && MD1_data[5] != null && MD1_data[6] != null && MD1_data[7] != null)
                {
                    //new BitArray(MD2_data[7].Select())
                    MD1_rcv = StringToByteArr(MD1_data[5], MD1_data[6], MD1_data[7]);
                    _Sensor_Ball_R = MD1_rcv[1, 7] == 1 ? true : false;
                    _Sensor_Ball_L = MD1_rcv[1, 6] == 1 ? true : false;
                    _Sensor_Launch_R_1 = MD1_rcv[1, 5] == 1 ? true : false;
                    _Sensor_Launch_R_2 = MD1_rcv[1, 4] == 1 ? true : false;
                    _Sensor_Launch_L_1 = MD1_rcv[1, 3] == 1 ? true : false;
                    _Sensor_Launch_L_2 = MD1_rcv[1, 2] == 1 ? true : false;

                    S_Ball_R.color = _Sensor_Ball_R ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Ball_L.color = _Sensor_Ball_L ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Launch_R_1.color = _Sensor_Launch_R_1 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Launch_R_2.color = _Sensor_Launch_R_2 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Launch_L_1.color = _Sensor_Launch_L_1 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Launch_L_2.color = _Sensor_Launch_L_2 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                }
                send_count++;
                if (send_count >= 1)
                {
                    send_data_small[0] = send_data[0];
                    send_data_small[1] = send_data[3];
                    send_data_small[2] = send_data[5];
                    send_data_small[3] = send_data[6];
                    send_data_small[4] = send_data[7];
                    send_count = 0;
                    serialHandler.Write(send_data_small, 0, 5);
                }
            }
            if (in_data[i].Contains("MD(16)"))
            {
                debug_MD2 = in_data[i] + "\n";
                MD2_data = in_data[i].Split(new string[] { "][" }, System.StringSplitOptions.None);
                if (MD2_data[2] != null && MD2_data[3] != null && MD2_data[5] != null && MD2_data[6] != null && MD2_data[7] != null)
                {
                    //new BitArray(MD2_data[7].Select())
                    MD2_rcv = StringToByteArr(MD2_data[5], MD2_data[6], MD2_data[7]);
                    _Sensor_In_1 = MD2_rcv[1, 3] == 1 ? true : false;
                    _Sensor_In_2 = MD2_rcv[1, 2] == 1 ? true : false;
                    _Sensor_In_3 = MD2_rcv[1, 1] == 1 ? true : false;
                    _Sensor_In_4 = MD2_rcv[1, 0] == 1 ? true : false;
                    _Sensor_Pos_1 = MD2_rcv[2, 7] == 1 ? true : false;
                    _Sensor_Pos_2 = MD2_rcv[2, 6] == 1 ? true : false;
                    _Sensor_Pos_3 = MD2_rcv[2, 5] == 1 ? true : false;
                    _Sensor_Pos_4 = MD2_rcv[2, 4] == 1 ? true : false;
                    _Sensor_Pos_5 = MD2_rcv[2, 3] == 1 ? true : false;

                    S_Pos_1.color = _Sensor_Pos_1 ? new Color32(165 ,255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Pos_2.color = _Sensor_Pos_2 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Pos_3.color = _Sensor_Pos_3 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Pos_4.color = _Sensor_Pos_4 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_Pos_5.color = _Sensor_Pos_5 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_In_1.color = _Sensor_In_1 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_In_2.color = _Sensor_In_2 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_In_3.color = _Sensor_In_3 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);
                    S_In_4.color = _Sensor_In_4 ? new Color32(165, 255, 165, 255) : new Color32(255, 255, 255, 255);

                    int rotateDirection = (int)(MD2_rcv[1, 4]) * 2 + (int)MD2_rcv[1, 5];
                    int rotateSpeed_rcv = MD2_rcv[1, 6] * 512 + MD2_rcv[1, 7] * 256 + MD2_rcv[0, 0] * 128 + MD2_rcv[0, 1] * 64 + MD2_rcv[0, 2] * 32 + MD2_rcv[0, 3] * 16 + MD2_rcv[0, 4] * 8 + MD2_rcv[0, 5] * 4 + MD2_rcv[0, 6] * 2 + MD2_rcv[0, 7];
                    if(rotateDirection == 2)
                    {
                        rotateSpeed_rcv *= -1;
                    }
                    realRotateSpeed = rotateSpeed_rcv;
                }
            }
            if (in_data[i].Contains("PC:"))
            {
                debug_data += in_data[i];
                Debug.Log(in_data[i]);
                debug_data = "";

                debug_PC = in_data[i] + "\n";
            }
        }
        Debug_text.text = debug_flush + debug_MD1 + debug_MD2 + debug_PC;
        
    }

    bool M_flag = false;
    bool C_flag = true;
    bool L_flag = true;
    void KeybordInput()
    {

        if (Input.GetKey(KeyCode.R)) { status_recet_flag = true; } else { status_recet_flag = false; }
        if (Input.GetKey(KeyCode.S)) { status_game_start_flag = true; } else { status_game_start_flag = false; }
        if (Input.GetKey(KeyCode.G)) { status_rounch_ball_flag = true; } else { status_rounch_ball_flag = false; }
        if (Input.GetKey(KeyCode.O)) { status_out_ball_flag = true; } else { status_out_ball_flag = false; }
        //if (Input.GetKey(KeyCode.U)) { user_button_data = 1; } else { user_button_data = 0; }
        if (Input.GetKey(KeyCode.M))
        {
            if (M_flag)
            {
                RotateSpeed_Slider.value = 0;
                if (enableToggle.Value)
                {
                    enableToggle.SwitchToggle();
                }
            }
        }
        else
        {
            M_flag = true;
        }
        if (Input.GetKey(KeyCode.C) && C_flag) {
            //F_START_VIDEO.Play();
            //F_ROTATE_VIDEO.Stop();
            if (UICanvas.activeSelf)
            {
                UICanvas.SetActive(false);
            }
            else
            {
                UICanvas.SetActive(true);
            }
            //_rotate_is_rotated = false;
            C_flag = false;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            C_flag = true;
        }
        if (Input.GetKey(KeyCode.L) && L_flag)
        {
            GamePhase += 1;
            L_flag = false;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            L_flag = true;
        }
        if (Input.GetKey(KeyCode.K))
        {
            //F_ROTATE_VIDEO.Play();
            //F_START_VIDEO.Stop();
            //UICanvas.SetActive(true);
        }
        if (Input.GetKey(KeyCode.J))
        {
            //F_START_VIDEO.Stop();
            //UICanvas.SetActive(true);
        }
        if (Input.GetKey(KeyCode.T))
        {
            OperationMode = true;
            OpSwitch_Select = true;
        }
        else
        {
            OpSwitch_Select = false;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            OperationMode = false;
        }
        if (Input.GetKey(KeyCode.Return))
        {
            OpSwitch_Enter = true;
        }
        else
        {
            OpSwitch_Enter = false;
        }
    }

    byte[,] StringToByteArr(String data2, String data1, String data0)
    {
        byte[,] return_byte = new byte[3,8];
        char[] charArr0 = data0.ToCharArray();
        char[] charArr1 = data1.ToCharArray();
        char[] charArr2 = data2.ToCharArray();
        for (int i=0; i<8; i++)
        {
            if (charArr0[i] == '0')
            {
                return_byte[0,i] = 0;
            }
            else
            {
                return_byte[0,i] = 1;
            }
            if (charArr1[i] == '0')
            {
                return_byte[1, i] = 0;
            }
            else
            {
                return_byte[1, i] = 1;
            }
            if (charArr2[i] == '0')
            {
                return_byte[2, i] = 0;
            }
            else
            {
                return_byte[2, i] = 1;
            }
        }

        return return_byte;
    }

    void color_alpha_to_0(SpriteRenderer target)
    {
        target.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void color_alpha_to_1(SpriteRenderer target)
    {
        target.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    void color_alpha_change(SpriteRenderer target, float alpha)
    {
        if (alpha >= 1.0)
        {
            alpha = 1.0f;
        }
        target.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

}
