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

    [Header("Material")]
    [SerializeField] private SkinnedMeshRenderer _cookieBody;
    [SerializeField] private Material _redMat;
    [SerializeField] private Material[] _mat;

    [Header("Break")]
    [SerializeField] private GameObject _breakCookie;
    [SerializeField] private SkinnedMeshRenderer _mr;


    [Header("Baking")]
    [SerializeField] private Transform bakingPos;
    private NavMeshAgent _agent;

    [Header("Baking")] private bool _isDance = false;
    private void Start()
    {
        targetRotation = transform.rotation.eulerAngles;
        minRotation = targetRotation.y - rotationRange;
        maxRotation = targetRotation.y + rotationRange;
        
        anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _mat = _cookieBody.materials;
    }
    private void Update()
    {
        gameState = GameManager.Instance.gameState;


        if (transform.localScale.x < 0 && gameState != GameState.Fail)
        {
            // 피격 효과
            StartCoroutine(BreakCookie());
            GameManager.Instance.Fail();
        }
        else 
        {
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
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                    {
                        anim.SetBool(AnimType.run.ToString(), true);
                    }
                    _agent.SetDestination(bakingPos.position);
                    break;
                case  GameState.StartBaking:
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
                    {
                        StartCoroutine(RotateCharacter());
                        GameManager.Instance.GameOver();
                    }
                    break;
                    
            }
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

    private IEnumerator BreakCookie()
    {

        _mr.enabled = false;
        yield return new WaitForSeconds(0.2f);
        Instantiate(_breakCookie, transform.position + Vector3.up, Quaternion.identity);
    }

    public IEnumerator RedCookie()
    {

        Material[] mats = _cookieBody.materials;
        mats[2] = _redMat;
        _cookieBody.materials = mats;

        Debug.Log("RedCookie");
        yield return new WaitForSeconds(0.2f);

        _cookieBody.materials = _mat;
        
    }
    
    IEnumerator RotateCharacter()
    {
        yield return new WaitForSeconds(0.2f);

        Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotaionSpeed * Time.deltaTime);
            yield return null;
        }
    }
}