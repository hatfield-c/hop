using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoState : MenuState
{
    [Header("Info References")]
    [SerializeField] protected LevelUIController parentController = null;

    [SerializeField] protected TextMeshProUGUI levelTitle = null;
    [SerializeField] protected TextMeshProUGUI hopperName = null;
    [SerializeField] protected TextMeshProUGUI distanceText = null;
    [SerializeField] protected Image previewImage = null;

    [Header("Hazard Sprites")]
    [SerializeField] protected GameObject deathHazard = null;
    [SerializeField] protected GameObject bounceHazard = null;
    [SerializeField] protected GameObject breakableHazard = null;
    [SerializeField] protected GameObject movingHazard = null; 

    void Awake() {
        if(Session.GetData() == null) {
            return;
        }

        ReferenceManager globalReferences = this.parentController.GetReferenceManager();
        LevelObject levelData = Session.GetData().GetLevel();

        this.levelTitle.text = levelData.GetSceneName();
        this.hopperName.text = globalReferences.player.GetPreviewName();
        this.distanceText.text = ((int)globalReferences.levelManager.GetDistance()).ToString();
        this.previewImage.sprite = globalReferences.player.GetPreviewImage();

        this.deathHazard.SetActive(levelData.hazards.death);
        this.bounceHazard.SetActive(levelData.hazards.bounce);
        this.breakableHazard.SetActive(levelData.hazards.breakable);
        this.movingHazard.SetActive(levelData.hazards.moving);
    }
}
