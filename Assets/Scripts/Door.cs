using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _effectObj;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform _targetRotation;
    
    private Vector3 initialPosition;  // 초기 위치
    private Quaternion _startRotation;
    private bool isMovingDown = true; // 아래로 이동 중인지 여부
    private MeshRenderer _mr;

    private bool isDoorClosing = false;
    float _rotateSpeed = 30f;
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _startRotation = transform.rotation;
    }

    private void Update() 
    {
        if (isDoorClosing)
        {
            StartCoroutine(CloseDoor());
        }    
    }
    
    private void OnCollisionEnter(Collision other)
    {
        GameObject hitObject = other.collider.gameObject;

        Debug.Log("Door!!" + hitObject.name);
        Debug.Log(GameManager.Instance.gameState);
        Debug.Log(hitObject.tag);
        if (GameManager.Instance.gameState == GameState.Fail)
        {


            // StartCoroutine(OpenDoor());
            isDoorClosing = true;
            StartCoroutine(DestroyDoor());
            // _effectObj.transform.parent = null;
            // _effectObj.SetActive(true);
            // _mr.enabled = false;
            // player.SetActive(false);
            // GameManager.Instance.isEndConquer = true;
        }
    }

    public IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(1f);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation.rotation, _rotateSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, _targetRotation.rotation) < 0.1f)
            isDoorClosing = false;
    }

    private IEnumerator DestroyDoor()
    {
        yield return StartCoroutine(CloseDoor());
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    
}
