using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Gimmick
{

    [Header("Break Effect")]
    [SerializeField] private GameObject     _particle; 
    [SerializeField] private MeshRenderer   _mr;

    [Header("Damage Effect")]
    [SerializeField] private Light _redLight;
    private float   _flashDuration  = 0.2f;
    private int     _flashCount     = 3;
    

    protected override void Rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(_startRotation.eulerAngles.x, _startRotation.eulerAngles.y, transform.rotation.eulerAngles.z + (_rotateSpeed * Time.deltaTime));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }
    
    protected override void ActivateGimmick(GameObject hitObject)
    {

        if (hitObject.transform.localScale.x > 10f)
        {
            Break();
        }
        else 
        {
            Debug.Log("OnDamage");
             // 재료 개수 감소
            GameManager.Instance.ChangeTasty(-5);
            StartCoroutine(hitObject.GetComponent<Cookie>().ChangeSize(-5));
            // 피격 효과
            StartCoroutine(FlashLight());
        }
       
    }

    IEnumerator FlashLight()
    {
        for (int i = 0; i < _flashCount; i++)
        {
            _redLight.enabled = true;
            yield return new WaitForSeconds(_flashDuration);

            _redLight.enabled = false;
            yield return new WaitForSeconds(_flashDuration);
        }

    }

    private void Break()
    {
        _particle.SetActive(true);        
        _mr.enabled = false; // 벽을 비활성화하여 부서진 것처럼 보이게 함
    }
}