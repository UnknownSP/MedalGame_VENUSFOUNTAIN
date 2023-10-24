# MedalGame_VENUSFOUNTAIN

KONAMI様のヴィーナスファウンテンのハードウェアを基にして、回路及びプログラムを作成し動作させたものです。

Unity上で動作しているアプリケーションから実機のような抽選機の操作(回転盤の操作)が可能です。

またサウンドも実機のように流れるようにしております。

## 動作例

https://github.com/UnknownSP/MedalGame_VENUSFOUNTAIN/assets/39638661/89701bb2-7c5f-4397-bd33-bee34a1020e7

https://github.com/UnknownSP/MedalGame_VENUSFOUNTAIN/assets/39638661/6b00caa1-f9ab-427e-927d-58256262c0ec

## 回路

![VENUS_cercuit](https://github.com/UnknownSP/MedalGame_VENUSFOUNTAIN/assets/39638661/451f3d06-8b4b-45c5-96d3-0894413e7b1f)

## 仕様

stm32f401_VENUSFOUNTAIN がマスタであり、Unity上から動作の管理を行っています。 PICは分割された各部分の処理を行っており、以下のように分担されています。

- type1
  - 入賞センサの管理と抽選機回転用インバータへの指令処理 
- type2
  - 発射モータ及びセンサの管理

※本プロジェクトで使用している音源は全て実機の録音源を使用しております。
