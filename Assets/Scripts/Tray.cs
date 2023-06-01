using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    [SerializeField] private GameObject[] _trays;
    [SerializeField] private GameObject _particle;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(TagType.Player.ToString()))
        {
            Debug.Log(other.gameObject.name);
            if (!_particle.activeSelf)
            {
                Debug.Log("End!");

                GameManager.Instance.StartBaking();
                _particle.SetActive(true);
                for(int i=0; i<_trays.Length; i++)
                    _trays[i].SetActive(false);
            }

        }
    }
}
