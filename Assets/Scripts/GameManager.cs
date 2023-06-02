using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{

    public GameState gameState = GameState.NotStart;
    public int cookieSize;

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
    
    void Start()
    {
        cookieSize = 5;
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


    public void ChangeSize(int amount)
    {
        cookieSize += amount;
    }
    
    public void StartGame()
    {
        // 사이즈 초기화
        cookieSize = 5;

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
    
    public void Fail()
    {
        // 게임 상태 변경 
        gameState = GameState.Fail;
    }
    
    public void GameOver()
    {
        // 게임 상태 변경 
        gameState = GameState.GameOver;
    }
}