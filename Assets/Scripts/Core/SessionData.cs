using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData {

    protected LevelObject currentLevel = null;
    protected LevelObject previousLevel = null;
    protected Session.TransitionCause transCause = Session.TransitionCause.ERROR;

    public SessionData() {

    }

    public LevelObject GetLevel() {
        return this.currentLevel;
    }

    public LevelObject GetPreviousLevel() {
        return this.previousLevel;
    }

    public void SetLevel(LevelObject level) {
        this.previousLevel = this.currentLevel;
        this.currentLevel = level;
    }

    public LevelObject GetNextLevel() {
        return this.currentLevel.nextLevel;
    }

    public Session.TransitionCause GetTransitionCause() {
        return this.transCause;
    }

    public void SetTransitionCause(Session.TransitionCause cause) {
        this.transCause = cause;
    }
}
