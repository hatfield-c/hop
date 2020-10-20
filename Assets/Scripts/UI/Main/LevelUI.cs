using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Button button = null;
    [SerializeField] protected Image overlay = null;
    [SerializeField] protected TextMeshProUGUI number = null;

    [Header("Parameters")]
    [SerializeField] protected LevelObject levelData = null;
    [SerializeField] protected float opacity = 1f;

    public void LoadLevel() {
        Session.StartLevel(this.levelData);
    }

    void Start()
    {
        Color overlayColor = new Color(
            this.levelData.color.r,
            this.levelData.color.g,
            this.levelData.color.b,
            this.opacity
        );
        this.overlay.color = overlayColor;

        this.number.text = this.levelData.number;

        this.button.onClick.AddListener(this.LoadLevel);
    }

}
