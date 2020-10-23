using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public Action winAction = null;

    private void OnTriggerEnter(Collider other) {
        Hopper hopper = other.gameObject.GetComponent<Hopper>();

        if(hopper == null) {
            return;
        }

        this.winAction?.Invoke();
    }
}
