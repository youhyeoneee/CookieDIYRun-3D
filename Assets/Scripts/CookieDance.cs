using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieDance : MonoBehaviour
{
    public float rotationSpeed = 10f;

    GameState gameState;

    private Quaternion targetRotation;

    private bool isFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameState = GameManager.Instance.gameState;

        if (gameState == GameState.StartBaking)
        {
            targetRotation = Quaternion.Euler(90,  90, -90f);
            StartCoroutine(RotateCharacter());
        }
        
        else if (gameState == GameState.GameOver)
        {
            if (isFlag)
            {
                targetRotation = Quaternion.Euler(90f, 90, -90f);
                StartCoroutine(RotateCharacter());
            }
            else
            {            
                targetRotation = Quaternion.Euler(120f, 90, -90f);
                StartCoroutine(RotateCharacter());
            }
        
        
        }

    }
    
    IEnumerator RotateCharacter()
    {
        yield return new WaitForSeconds(1f);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        isFlag = !isFlag;
    }
}
