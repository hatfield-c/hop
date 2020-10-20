using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelX", menuName = "UI Level")]
public class LevelObject : ScriptableObject
{
    [Header("UI")]
    public Color color;
    public string number;

    [Header("Parameters")]
    public Session.Stages stage;
    public Object scene;
    public LevelObject nextLevel;
   

}
