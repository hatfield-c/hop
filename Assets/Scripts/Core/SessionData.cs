using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData {

    protected LevelObject currentLevel;
    protected LevelObject nextLevel;

    public SessionData() {

    }

    public LevelObject GetNextLevel() {
        return this.nextLevel;
    }
}
