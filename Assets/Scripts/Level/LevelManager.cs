using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected ReferenceManager references = null;

    public Action winAction;
    public Action loseAction;

    public void ResetLevel() {
        this.references.player.ResetObject();
    }

    public void WinLevel() {
        this.references.uiController.FinishLevel();
    }

    public void LoseReset() {
        this.references.uiController.LoseLevel();
        this.ResetLevel();
    }

    public float GetDistance() {
        return Vector3.Distance(
            this.references.player.transform.position,
            this.references.goalZone.transform.position
        );
    }

    void Start() {
        this.references.player.loseAction = this.loseAction;
        this.references.goalZone.winAction = this.winAction;
    }

    void OnEnable() {
        this.winAction += this.WinLevel;
        this.loseAction += this.LoseReset;
    }

    void OnDisable() {
        this.winAction -= this.WinLevel;
        this.loseAction -= this.LoseReset;
    }

}
