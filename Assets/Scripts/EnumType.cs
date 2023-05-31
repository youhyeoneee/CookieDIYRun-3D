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
    offset,
    ready,
    dance,
    destroy
}

public enum GameState
{
    NotStart,
    Run,
    GoToOven,
    StartBaking,
    Ended
}

public enum FoodType
{
    None,
    Chocolate,
    Papryka
}