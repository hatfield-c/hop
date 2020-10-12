using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Spin spin = other.gameObject.GetComponent<Spin>();

        if(spin == null) {
            return;
        }

        Debug.Log("Success!");
        spin.Reset();
    }
}
