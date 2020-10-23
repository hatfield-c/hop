using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected ReferenceManager references = null;

    public void ResetLevel() {
        this.references.player.Reset();
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

    void OnEnable() {
        this.references.goalZone.winAction += this.WinLevel;
        this.references.player.loseAction += this.LoseReset;
    }

    void OnDisable() {
        this.references.goalZone.winAction -= this.WinLevel;
        this.references.player.loseAction -= this.LoseReset;
    }

}
