using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagType
{
    Player, 
}

public enum AnimType
{
    run, 
    jump,
    attack,
    jump2,
    slide,
}

public enum GameState
{
    NotStart, // 게임 시작 전 
    Run, // 달리기 게임 중
    GoToOven, // 오븐에 달려가는 중
    StartBaking, // 오븐 엔딩  
    Fail, // 사이즈가 너무 줄어들었을 경우 실패 
    GameOver // 게임 끝
}

public enum ItemType
{
    Plus,
    Minus
}