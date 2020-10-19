using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Session
{
    private static SessionData data = null;
    private static bool initialized = false;

    public static void Init() {
        if (Session.initialized) {
            return;
        }
    }

    public static SessionData GetData() {
        Session.Init();

        return Session.data;
    }

    public static void SetData(SessionData data) {
        Session.Init();

        Session.data = data;
    }

    public static LevelObject GetNextLevel() {
        Session.Init();

        return Session.data.GetNextLevel();
    }
}
