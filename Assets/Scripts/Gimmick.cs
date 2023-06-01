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
    protected Rigidbody _rb;



    protected virtual void Start()
    {
        _startRotation = transform.rotation;
        _mr = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError($"Rigidbody 컴포넌트가 {gameObject.name} 장애물에 추가되어 있지 않습니다.");
        }
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
            // Key -> 아이템 획득
        }
    }
    protected abstract void ActivateGimmick(GameObject hitObject);

    ItemType StringToEnum(string alphabet)
    {
        return (ItemType)Enum.Parse(typeof(ItemType), alphabet);
    }
    
}