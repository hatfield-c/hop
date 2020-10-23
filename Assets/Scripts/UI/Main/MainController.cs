using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MenuController {
    [Header("Main References")]
    [SerializeField] protected MenuState stageComplete = null;
    [SerializeField] protected MenuState gameComplete = null;

    void Awake() {
        MenuState startState = null;

        if(Session.GetData() != null) {
            SessionData sessionData = Session.GetData();

            if(sessionData.GetPreviousLevel()!= null && sessionData.GetPreviousLevel().isLastLevel) {
                startState = this.gameComplete;
            } else if(sessionData.GetTransitionCause() == Session.TransitionCause.StageComplete) {
                startState = this.stageComplete;
                Debug.Log($"Stage complete: {startState}");
            }
        }

        base.OnAwake(startState);
    }
}
