using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Gimmick
{
    [SerializeField] private GameObject _effectObj;

    protected override void Rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(_startRotation.eulerAngles.x, transform.rotation.eulerAngles.y + (_rotateSpeed * Time.deltaTime), _startRotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }
    
    protected override void ActivateGimmick(GameObject hitObject)
    {
        // _particleObj.SetActive(false);
        _effectObj.SetActive(true);
        _mr.enabled = false;
        
        GameManager.Instance.ChangeTasty(_amount);
        StartCoroutine(hitObject.GetComponent<Cookie>().ChangeSize(_amount));
    }
}