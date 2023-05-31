using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cookie : MonoBehaviour
{
    GameState gameState;

    [Header("Move")]
    [SerializeField] private float      moveXSpeed          = 50f;
    [SerializeField] private float      moveZSpeed          = 18f;
    private float dragDirection;

    [Header("Rotation")]
    [SerializeField] private float      rotaionSpeed            = 10f;
    [SerializeField] private float      rotationSmoothness      = 20f;
    private Vector3     targetRotation; // 드래그 입력에 따라 조정되는 목표 회전값
    private float       rotationRange = 25f;
    private float       minRotation;
    private float       maxRotation;

    [Header("Animation")]
    public Animator anim;

    [Header("Size")]
    private float maxScale = 30f;
    private float minScale = 10f;
    [SerializeField] private float _growthDuration = 0.1f; // 

    [Header("Baking")]
    [SerializeField] private Transform bakingPos;
    private NavMeshAgent _agent;

    private void Start()
    {
        targetRotation = transform.rotation.eulerAngles;
        minRotation = targetRotation.y - rotationRange;
        maxRotation = targetRotation.y + rotationRange;
        
        anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        gameState = GameManager.Instance.gameState;
        switch (gameState)
        {
            case GameState.Run:
                if (Input.GetMouseButton(0))
                {
                    anim.SetBool(AnimType.run.ToString(), true);

                    // 앞으로 이동
                    float moveZ = moveZSpeed * Time.fixedDeltaTime;
                    transform.Translate(Vector3.forward * moveZ);

                    // 마우스 드래그 입력 받기
                    dragDirection = Input.GetAxis("Mouse X");

                    // 좌우로 이동
                    float moveX = dragDirection * moveXSpeed * Time.fixedDeltaTime;
                    transform.Translate(Vector3.right * moveX);

                    // 회전
                    float rotateY = dragDirection * rotaionSpeed;
                    targetRotation.y = Mathf.Clamp(targetRotation.y + rotateY, minRotation, maxRotation);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotationSmoothness);

                }
                if (Input.GetMouseButtonUp(0))
                {
                    anim.SetBool(AnimType.run.ToString(), false);
                }

                if (transform.localScale.x == 0)
                    GameManager.Instance.GameOver();
                break;
            case GameState.GoToOven:
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    anim.SetBool(AnimType.run.ToString(), true);
                }
                _agent.SetDestination(bakingPos.position);
                break;
        }
    }
    
    public IEnumerator ChangeSize(float amount)
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;        
        float newSize = transform.localScale.x + amount;
        Vector3 targetScale = new Vector3(newSize, newSize, newSize); 

        while (elapsedTime < _growthDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / _growthDuration);
            yield return null;
        }
    }
}