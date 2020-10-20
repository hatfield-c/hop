using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIController : MenuController
{
    public override void ReturnToMainMenu() {
        Time.timeScale = 1f;

        base.ReturnToMainMenu();
    }

    public void PauseGame(MenuState pauseState) {
        Time.timeScale = 0f;

        this.ChangeState(pauseState);
    }

    public void ResumeGame(MenuState playState) {
        this.ChangeState(playState);

        Time.timeScale = 1f;
    }
}
