using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam : MonoBehaviour
{
    [SerializeField] private float jamAmount = 0.05f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerJam.CollectJam(jamAmount);
            Destroy(gameObject);
        }
    }
}
