using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] particles;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(TagType.Player.ToString()))
        {
            // 파티클 
            for(int i=0; i<particles.Length; i++)
                particles[i].SetActive(true);

            // 게임 상태 변화
            GameManager.Instance.GoToOven();
        }
    }
}