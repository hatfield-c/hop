using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected ReferenceManager references = null;

    private void OnTriggerEnter(Collider other) {
        Hopper hopper = other.gameObject.GetComponent<Hopper>();

        if(hopper == null) {
            return;
        }

        Debug.Log("Success!");
        this.references.uiController.FinishLevel();
    }
}
