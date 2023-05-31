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
    private float originalScale;
    private float maxScale = 30f;
    private float minScale = 10f;

    [Header("Baking")]
    [SerializeField] private Transform bakingPos;
    private NavMeshAgent _agent;

    private void Start()
    {
        targetRotation = transform.rotation.eulerAngles;
        minRotation = targetRotation.y - rotationRange;
        maxRotation = targetRotation.y + rotationRange;

        originalScale = transform.localScale.x; // 캐릭터의 초기 스케일을 저장합니다.

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
                break;
            case GameState.GoToOven:

                Debug.Log(_agent.currentOffMeshLinkData.linkType);
                _agent.SetDestination(bakingPos.position);
                anim.SetBool(AnimType.run.ToString(), true);
                break;
        }
    }

    public void IncreaseSize()
    {
        float newSize = transform.localScale.x * 2f; // 현재 스케일의 2배로 크기를 증가시킵니다.
        newSize = Mathf.Clamp(newSize, originalScale, maxScale); // 크기를 최소 스케일과 최대 스케일 사이로 제한합니다.
        transform.localScale = new Vector3(newSize, newSize, newSize); // 스케일을 증가시킨 값으로 설정합니다.
    }

    public void DecreaseSize()
    {
        float newSize = transform.localScale.x * 0.5f; // 현재 스케일의 절반으로 크기를 감소시킵니다.
        newSize = Mathf.Clamp(newSize, minScale, originalScale); // 크기를 최소 스케일과 초기 스케일 사이로 제한합니다.
        transform.localScale = new Vector3(newSize, newSize, newSize); // 스케일을 감소시킨 값으로 설정합니다.
    }
}