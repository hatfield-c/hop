using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIController : MenuController
{
    [Header("Level References")]
    [SerializeField] ReferenceManager references = null;
    [SerializeField] MenuState pauseState = null;
    [SerializeField] MenuState playState = null;
    [SerializeField] MenuState finishState = null;

    public override void ReturnToMainMenu() {
        Time.timeScale = 1f;

        base.ReturnToMainMenu();
    }

    public void NextLevel() {
        Session.NextLevel();
    }

    public void FinishLevel() {
        Time.timeScale = 0f;

        this.ChangeState(this.finishState);
    }

    public void RestartLevel() {
        this.references.levelManager.ResetLevel();
        this.ChangeState(this.playState);

        Time.timeScale = 1f;
    }

    public void PauseGame() {
        Time.timeScale = 0f;

        this.ChangeState(this.pauseState);
    }

    public void ResumeGame() {
        this.ChangeState(this.playState);

        Time.timeScale = 1f;
    }
}
