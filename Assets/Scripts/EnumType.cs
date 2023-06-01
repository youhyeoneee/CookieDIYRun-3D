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
}

public enum GameState
{
    NotStart,
    Run,
    GoToOven,
    StartBaking,
    Fail,
    Ended
}

public enum ItemType
{
    Plus,
    Minus
}