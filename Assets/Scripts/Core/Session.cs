using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Session
{
    private static SessionData data = null;
    private static bool initialized = false;

    public static void Init(SessionData data = null) {
        if (Session.initialized) {
            return;
        }

        if(data == null) {
            Session.data = new SessionData();
        }

        Session.initialized = true;
    }

    public static void ReturnToMainMenu() {
        Loader.LoadScene(Loader.CoreScenes.MainMenu.ToString());
    }

    public static void StartLevel(LevelObject levelData) {
        LevelObject emptyLevel = ScriptableObject.CreateInstance<LevelObject>();
        emptyLevel.nextLevel = levelData;

        Session.data.SetLevel(emptyLevel);
        Session.NextLevel();
    }

    public static void NextLevel() {
        LevelObject next = Session.data.GetNextLevel();

        if(next == null) {

        }

        Session.data.SetLevel(next);
        Loader.LoadLevel(Session.data.GetLevel());
    }

    public static SessionData GetData() {
        return Session.data;
    }

    public static void SetData(SessionData data) {
        Session.data = data;
    }

    public static LevelObject GetNextLevel() {
        return Session.data.GetNextLevel();
    }

    public enum Stages {
        Stage1,
        Stage2
    }
}
