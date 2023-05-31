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
    [SerializeField] private Text chochText;
    [SerializeField] private Text papryText;

    [Header("Image")]
    [SerializeField] private GameObject chochImg;
    [SerializeField] private GameObject papryImg;

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
            case GameState.Run:
                // 시작 UI 비활성화
                if (startUI.activeSelf)
                    startUI.SetActive(false);

                // 게임 UI 활성화
                if (!gameUI.activeSelf)
                    gameUI.SetActive(true);

                // 재료 개수 UI
                chochText.text = GameManager.Instance.chocoFoodCounts.cnt.ToString();
                papryText.text = GameManager.Instance.papryFoodCounts.cnt.ToString();
                
                // 더 많은 재료 말풍선 UI
                FoodType nowFood = GameManager.Instance.CompareCount();
                switch (nowFood)
                {
                    case FoodType.Chocolate:
                        chochImg.SetActive(true);
                        papryImg.SetActive(false);
                        break;
                    case FoodType.Papryka:
                        chochImg.SetActive(false);
                        papryImg.SetActive(true);
                        break;
                    case FoodType.None:
                        chochImg.SetActive(false);
                        papryImg.SetActive(false);
                        break;

                }                
                break;


        }
      

        
    }
}
