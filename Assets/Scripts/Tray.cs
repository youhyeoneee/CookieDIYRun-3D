using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    [SerializeField] private GameObject[] _trays;
    [SerializeField] private GameObject _destroyParticle;
    [SerializeField] private GameObject _ovenParticle;
    private Vector3 _initialPosition;  // 초기 위치
    private float _targetZ;
    private float _moveSpeed = 3f;
    private GameState _gameState;
    private bool isMove = false;
    [SerializeField] private GameObject trayWithCookie;
    [SerializeField] private Door door;
    [SerializeField] private GameObject _heartParticle;


    private void Start()
    {
        _initialPosition = trayWithCookie.transform.localPosition;
        _targetZ = -1;
    }

    private void Update()
    {
        _gameState = GameManager.Instance.gameState;

        if (_gameState == GameState.Fail && !isMove)
        {
            StartCoroutine(MoveTray());
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(TagType.Player.ToString()))
        {
            Debug.Log(other.gameObject.name);

            if (GameManager.Instance.cookieSize > 0)
                StartCoroutine(DestroyTray());

        }
    }

    IEnumerator DestroyTray()
    {
        yield return new WaitForSeconds(1f);
        if (!_destroyParticle.activeSelf)
        {
            Debug.Log("End!");

            _destroyParticle.SetActive(true);
            for(int i=0; i<_trays.Length; i++)
                _trays[i].SetActive(false);

            _heartParticle.SetActive(true);
            GameManager.Instance.StartBaking();
        
        }
    }

    public  IEnumerator MoveTray()
    {
        yield return new WaitForSeconds(5f);
            
        // 들어감 
        if (trayWithCookie.transform.localPosition.z > _targetZ)
        {
            Vector3 target = new Vector3(trayWithCookie.transform.localPosition.x, trayWithCookie.transform.localPosition.y, _targetZ);
            trayWithCookie.transform.localPosition = Vector3.MoveTowards(trayWithCookie.transform.localPosition, target, _moveSpeed * Time.deltaTime);
        }
        
        yield return StartCoroutine(door.CloseDoor());
        yield return new WaitForSeconds(3f);
        _ovenParticle.SetActive(true);

        isMove = true;
    }
}
