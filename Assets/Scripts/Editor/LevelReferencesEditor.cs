using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReferenceManager))]
public class LevelReferencesEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Compile Resettables")) {
            ReferenceManager referenceManager = (ReferenceManager)target;
            referenceManager.resetables.Clear();

            object[] resettableObjects = Object.FindObjectsOfType(typeof(AbstractResettable));

            foreach(object resObj in resettableObjects) {
                AbstractResettable resettable = (AbstractResettable)resObj;
                referenceManager.resetables.Add(resettable);
            }
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
