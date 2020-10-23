using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIController : MenuController
{
    [Header("Level References")]
    [SerializeField] protected ReferenceManager references = null;
    [SerializeField] protected MenuState pauseState = null;
    [SerializeField] protected MenuState playState = null;
    [SerializeField] protected MenuState finishState = null;
    [SerializeField] protected LosePanel losePanel = null;

    [Header("Parameters")]
    [SerializeField] protected float loseFadeTime = 1f;

    public override void ReturnToMainMenu() {
        Time.timeScale = 1f;

        base.ReturnToMainMenu();
    }

    public void NextLevel() {
        Time.timeScale = 1f;

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

    public void LoseLevel() {
        this.RestartLevel();
        this.losePanel.Display();
    }

    public void PauseGame() {
        Time.timeScale = 0f;

        this.ChangeState(this.pauseState);
    }

    public void ResumeGame() {
        this.ChangeState(this.playState);

        Time.timeScale = 1f;
    }

    public ReferenceManager GetReferenceManager() {
        return this.references;
    }

    void Awake() {
        base.OnAwake();

        Time.timeScale = 0f;
    }
}
