using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTrigger : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.collider.gameObject.CompareTag(TagType.Player.ToString()))
        {
            // 오븐 있는 바닥에 닿았을 때 공격 모션
            other.gameObject.GetComponent<Cookie>().anim.SetBool(AnimType.attack.ToString(), true);
        }
    }
}
