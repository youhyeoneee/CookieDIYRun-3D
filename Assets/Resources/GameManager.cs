using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class FoodCounts
{
    public FoodType food;
    public int cnt;
    public FoodCounts(FoodType _food, int _cnt)
    {
        food = _food;
        cnt = _cnt;
    }

}

public class GameManager : MonoBehaviour
{

    public GameState gameState = GameState.NotStart;    
    public FoodCounts chocoFoodCounts;
    public FoodCounts papryFoodCounts;

    public FoodType nowFood;

    #region instance

    // 매니저 싱글톤의  Prefab 경로
    private static readonly string managerPrefabPath = "GameManager";

    public static GameManager Instance;

    // 이미 싱글 톤이 존재한다면 그것을 돌려주고, 없는 경우 만든다.
    private static GameManager instance
    {
        get
        {
            if (Instance != null) return Instance;
            if (GameManager.instance == null)
            {
                var resource = Resources.Load(managerPrefabPath);
                Object.Instantiate(resource);
            }
            Instance = GameManager.instance;
            return Instance;
        }
    }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start() 
    {
       
    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.NotStart:    
                if (Input.GetMouseButtonDown(0))
                    StartGame();
                break;
        }

    }


    public void ChangeCount(FoodType food, int changeCount)
    {
        switch(food)
        {
            case FoodType.Chocolate:
                chocoFoodCounts.cnt += changeCount;
                if (chocoFoodCounts.cnt < 0) chocoFoodCounts.cnt = 0;
                
                break;
            case FoodType.Papryka:
                papryFoodCounts.cnt += changeCount;
                if (papryFoodCounts.cnt < 0) papryFoodCounts.cnt = 0;
                break;
        }


    }

    public FoodType CompareCount()
    {
        if (chocoFoodCounts.cnt > 0 || papryFoodCounts.cnt > 0)
        {
            if (chocoFoodCounts.cnt >= papryFoodCounts.cnt)
                return chocoFoodCounts.food;
            else   
                return papryFoodCounts.food;
        }

        return FoodType.None;
    }
    
    public void StartGame()
    {
        // 재료 개수 초기화
        chocoFoodCounts = new FoodCounts(FoodType.Chocolate, 0);
        papryFoodCounts = new FoodCounts(FoodType.Papryka, 0);

        // 게임 상태 변경 
        gameState = GameState.Run;
    }

    public void GoToOven()
    {
        // 게임 상태 변경 
        gameState = GameState.GoToOven;
    }

    public void StartBaking()
    {
        // 게임 상태 변경 
        gameState = GameState.StartBaking;
    }
}