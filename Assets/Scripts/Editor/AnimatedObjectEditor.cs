using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimatedObject))]
public class AnimatedObjectEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Compile Path")) {
            AnimatedObject animatedObject = (AnimatedObject)target;
            animatedObject.pathList.Clear();

            foreach(Transform pathTransform in animatedObject.pathContainer) {
                animatedObject.pathList.Add(pathTransform);
            }
        }

        string helpMsg = "The first and the last elements in the path container determine the 'angle' of the beginning/ending of the path.\n\n";
        helpMsg += "Set them to have the same position as the beginning/ending of the path to ignore this.\n\n";
        helpMsg += "Requires at least four positions for a functional path, as only the middle two points are used.";
        EditorGUILayout.HelpBox(helpMsg, MessageType.Info);

        helpMsg = "Recommended easings:\n";
        helpMsg += "  - Linear\n";
        helpMsg += "  - Quad\n";
        helpMsg += "  - Quart\n";
        helpMsg += "  - Sine\n";
        helpMsg += "  - Quint\n";
        helpMsg += "  - Circ\n";
        helpMsg += "  - Cubic\n";
        helpMsg += "  - Expo\n\n";
        helpMsg += "Visit easings.net for help choosing the right easing.";
        EditorGUILayout.HelpBox(helpMsg, MessageType.Info);

        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
