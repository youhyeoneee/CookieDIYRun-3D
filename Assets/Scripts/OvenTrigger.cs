using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTrigger : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.collider.gameObject.CompareTag(TagType.Player.ToString()))
        {
            GameManager.Instance.StartBaking();
        }
    }
}
