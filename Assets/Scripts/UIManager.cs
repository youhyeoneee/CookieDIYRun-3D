using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameUI;

    [Header("Button")]
    [SerializeField] private Button startButton;
    
    [Header("Text")]
    [SerializeField] private Text coinText;
    [SerializeField] private Text tastyText;
    
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(GameManager.Instance.StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        gameState = GameManager.Instance.gameState;

        switch(gameState)
        {
            case GameState.GoToOven:
            case GameState.Run:
                // 시작 UI 비활성화
                if (startUI.activeSelf)
                    startUI.SetActive(false);

                // 게임 UI 활성화
                if (!gameUI.activeSelf)
                    gameUI.SetActive(true);



                // 재료 개수 UI
                int size = GameManager.Instance.cookieSize;
                if (size < 0) size = 0;
                tastyText.text = size.ToString();

                break;
            case GameState.Fail:
            case GameState.StartBaking:
                 // 게임 UI 활성화
                if (gameUI.activeSelf)
                    gameUI.SetActive(false);
                break;
        }


      

        
    }

}
