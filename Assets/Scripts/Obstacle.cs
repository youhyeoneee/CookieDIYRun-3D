using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Gimmick
{



    [Header("Obstacle Break Effect")]
    [SerializeField] private GameObject     _particle; 
    [SerializeField] private float wallSize = 0;
    public float forceMagnitude = 500f; // 날아가는 힘의 세기

    protected override void Rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(_startRotation.eulerAngles.x, _startRotation.eulerAngles.y, transform.rotation.eulerAngles.z + (_rotateSpeed * Time.deltaTime));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }
    
    protected override void ActivateGimmick(GameObject hitObject)
    {

        Debug.Log(hitObject);
        // 사이즈 감소
        GameManager.Instance.ChangeSize(_amount);
        Cookie cookie = hitObject.GetComponent<Cookie>();
        StartCoroutine(cookie.ChangerCookieColor(cookie._redMat));
        StartCoroutine(cookie.ChangeSize(_amount));

        Debug.Log($"{GameManager.Instance.cookieSize} {wallSize}");
        // 벽보다 크면
        if (GameManager.Instance.cookieSize >= wallSize)
        {
            // 뚫고 감
            if (_particle != null)
                Break();
        }
        else
        {
            StartCoroutine(cookie.BreakCookie());
        }
    }

    private void Break()
    {
        _particle.SetActive(true);        
        _mr.enabled = false; // 벽을 비활성화하여 부서진 것처럼 보이게 함
    }

}