using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData {

    protected LevelObject currentLevel;

    public SessionData() {

    }

    public LevelObject GetLevel() {
        return this.currentLevel;
    }

    public void SetLevel(LevelObject level) {
        this.currentLevel = level;
    }

    public LevelObject GetNextLevel() {
        return this.currentLevel.nextLevel;
    }
}
