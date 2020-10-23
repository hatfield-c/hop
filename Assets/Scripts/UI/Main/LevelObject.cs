using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelX", menuName = "UI Level")]
public class LevelObject : ScriptableObject
{
    [System.Serializable]
    public struct HazardTypes {
        public bool death;
        public bool bounce;
        public bool breakable;
        public bool moving;

        public HazardTypes(bool initial = false) {
            this.death = initial;
            this.bounce = initial;
            this.breakable = initial;
            this.moving = initial;
        }
    };

    [Header("UI")]
    public Color color;
    public string number;

    [Header("Parameters")]
    public Session.Stages stage;
    public Object scene;
    public HazardTypes hazards = new HazardTypes();
    public LevelObject nextLevel;

    public string GetSceneName() {
        return ((SceneAsset)this.scene).name;
    }

}
