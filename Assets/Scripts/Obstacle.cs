using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Gimmick
{



    [Header("Obstacle Break Effect")]
    [SerializeField] private GameObject     _particle; 

    public float forceMagnitude = 500f; // 날아가는 힘의 세기
    private Rigidbody _rb;

    

    protected override void Rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(_startRotation.eulerAngles.x, _startRotation.eulerAngles.y, transform.rotation.eulerAngles.z + (_rotateSpeed * Time.deltaTime));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        _rb = GetComponent<Rigidbody>();

        if (_rb == null)
        {
            Debug.LogError($"Rigidbody 컴포넌트가 {gameObject.name} 장애물에 추가되어 있지 않습니다.");
        }
    }
    
    protected override void ActivateGimmick(GameObject hitObject)
    {

        // if (hitObject.transform.localScale.x > 10f)
        // {
        //     Break();
        // }
        // else 
        // {

            Debug.Log(hitObject);
             // 재료 개수 감소
            GameManager.Instance.ChangeTasty(_amount);
            Cookie cookie = hitObject.GetComponent<Cookie>();
            StartCoroutine(cookie.RedCookie());
            StartCoroutine(cookie.ChangeSize(_amount));

            // 충돌한 방향을 구하여 힘을 가해 장애물을 날려줌
            if (_rb != null)
            {
                Vector3 collisionDirection = (hitObject.transform.position - transform.position).normalized;
                _rb.AddForce(collisionDirection * forceMagnitude, ForceMode.Impulse);
            }

        // }
       
    }

    private void Break()
    {
        _particle.SetActive(true);        
        _mr.enabled = false; // 벽을 비활성화하여 부서진 것처럼 보이게 함
    }

}