using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelX", menuName = "UI Level")]
public class LevelObject : ScriptableObject
{
    public Color color;
    public string number;
    public Loader.Scene stage;
    public GameObject levelGeometry;
}
