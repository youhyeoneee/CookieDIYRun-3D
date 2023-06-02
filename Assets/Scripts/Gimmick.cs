using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gimmick : MonoBehaviour
{


    [Header("Gimmick for Rotate")] 
    [SerializeField] protected bool _isRotate;
    [SerializeField] protected float _rotateSpeed;
    protected Quaternion _startRotation;
    [SerializeField] protected int _amount;
    [SerializeField] protected MeshRenderer   _mr;



    protected virtual void Start()
    {
        _startRotation = transform.rotation;
        _mr = GetComponent<MeshRenderer>();

    }

    protected virtual void Update()
    {
        if (_isRotate)
            Rotate();
    }

    protected abstract void Rotate();
    protected virtual void OnCollisionEnter(Collision other)
    {
        GameObject hitObject = other.collider.gameObject;
        // 플레이어와 충돌했을 경우
        if (hitObject.CompareTag(TagType.Player.ToString()))
        {
            ActivateGimmick(hitObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {
        GameObject hitObject = other.gameObject;
        // 플레이어와 충돌했을 경우
        if (hitObject.CompareTag(TagType.Player.ToString()))
        {
            ActivateGimmick(hitObject);
        }
    }

    protected abstract void ActivateGimmick(GameObject hitObject);

    ItemType StringToEnum(string alphabet)
    {
        return (ItemType)Enum.Parse(typeof(ItemType), alphabet);
    }
    
}