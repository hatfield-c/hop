using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Session
{
    private static SessionData data = null;

    public static SessionData GetData() {
        return Session.data;
    }

    public static void SetData(SessionData data) {
        Session.data = data;
    }
}
