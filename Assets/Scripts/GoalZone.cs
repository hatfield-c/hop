using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected ReferenceManager references = null;

    private void OnTriggerEnter(Collider other) {
        Spin spin = other.gameObject.GetComponent<Spin>();

        if(spin == null) {
            return;
        }

        Debug.Log("Success!");
        this.references.uiController.FinishLevel();
    }
}
