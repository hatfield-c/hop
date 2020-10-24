using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Parameters")]
    public float SetSuperBounce = 1f;

    [Header("References")]
    [SerializeField] protected ReferenceManager references = null;

    public Action winAction;
    public Action loseAction;

    public static string superBounceTag = "super-bounce";
    public static float superBounceFactor;

    public void ResetLevel() {
        foreach(AbstractResettable resettable in this.references.resetables) {
            if(resettable == null) {
                continue;
            }

            resettable.ResetObject();
        }
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

        LevelManager.superBounceFactor = this.SetSuperBounce;
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
