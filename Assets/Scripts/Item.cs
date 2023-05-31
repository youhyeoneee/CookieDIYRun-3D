using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Gimmick
{
    [Header("Cookie")]
    [SerializeField] private GameObject _cookie;
    private Rigidbody _rb;

    [Header("Item")]
    [SerializeField] private MeshRenderer[] _mrs;
    [SerializeField] private GameObject _particleObj;
    [SerializeField] private GameObject _effectObj;
    [SerializeField] private float _growthDuration = 1f; // 
    [SerializeField] private float _returnDuration = 5f;
    private Vector3 _targetScale;
    private Vector3 _originalScale;

    protected override void Start()
    {
        base.Start();
        _originalScale = _cookie.transform.localScale;
        _targetScale = _originalScale * 2f;
        _rb = _cookie.GetComponent<Rigidbody>();
    }

    protected override void Rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(_startRotation.eulerAngles.x, transform.rotation.eulerAngles.y + (_rotateSpeed * Time.deltaTime), _startRotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }

    protected override void ActivateGimmick(GameObject hitObject)
    {
        _particleObj.SetActive(false);
        _effectObj.SetActive(true);

        for(int i=0; i<_mrs.Length; i++)
            _mrs[i].enabled = false;       

        // 앞으로 빨리 가게
        // _rb.AddForce(transform.forward * -10f, ForceMode.Impulse);

        // 커져라
        StartCoroutine(ChangeSize());
    }

    IEnumerator ChangeSize()
    {
        float elapsedTime = 0f;
        Vector3 startScale = _cookie.transform.localScale;
        Vector3 targetScale = this._targetScale;

        while (elapsedTime < _growthDuration)
        {
            elapsedTime += Time.deltaTime;
            _cookie.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / _growthDuration);
            yield return null;
        }

        yield return new WaitForSeconds(_returnDuration);

        elapsedTime = 0f;
        startScale = _cookie.transform.localScale;
        targetScale = _originalScale;

        while (elapsedTime < _growthDuration)
        {
            elapsedTime += Time.deltaTime;
            _cookie.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / _growthDuration);
            yield return null;
        }
    }
}