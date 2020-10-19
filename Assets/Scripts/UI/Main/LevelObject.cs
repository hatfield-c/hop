using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelX", menuName = "UI Level")]
public class LevelObject : ScriptableObject
{
    [Header("UI")]
    public Color color;
    public string number;

    [Header("Parameters")]
    public Loader.StageScene stage;
    public GameObject levelGeometry;
    public GameObject spawnPoint;
    public GameObject goalZone;

    [Header("Next Level")]
    public LevelObject nextLevel;

}
