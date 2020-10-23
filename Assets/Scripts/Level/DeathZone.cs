using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Hopper hopper = other.gameObject.GetComponent<Hopper>();

        if (hopper == null) {
            return;
        }

        hopper.Die();
    }
}
