using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuilderUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Selectable startButton = null;
    [SerializeField] protected Selectable nextButton = null;
    [SerializeField] protected List<Selectable> rewardButtons = new List<Selectable>();
    [SerializeField] protected TextMeshProUGUI genText = null;
    [SerializeField] protected TextMeshProUGUI typeText = null;
    [SerializeField] protected WorldAcademy worldAcademy = null;

    public BuildStates currentState = BuildStates.freshRun;

    public void ChangeState(BuildStates newState) {
        switch (newState) {
            case BuildStates.freshRun:
                break;
            case BuildStates.typeCreate:
                WorldAcademy.LearnStep();
                break;
            case BuildStates.typeReward:
                this.SetRewardStatus(true);
                break;
            case BuildStates.typeFinished:
                this.typeText.gameObject.SetActive(true);
                this.SetRewardStatus(false);
                this.nextButton.interactable = true;
                break;
            case BuildStates.generationFinished:
                this.PrepNewGeneration();
                break;
            default:
                Debug.LogError("ERROR: UNKNOWN STATE");
                break;
        }

        this.currentState = newState;
    }

    public void ApplyReward(float reward) {
        WorldAcademy.currentAgent.AddReward(reward);

        if (WorldAcademy.currentAgent.IsFinished()) {
            WorldAcademy.currentAgent.Finish();
            this.ChangeState(BuildStates.typeFinished);
        } else {
            this.ChangeState(BuildStates.typeCreate);
        }
    }

    public void StartButtonClick() {
        this.worldAcademy.NewTerrain();
        this.startButton.interactable = false;
        this.genText.gameObject.SetActive(false);
    }

    public void NextButtonClick() {
        this.worldAcademy.NextTerrainType();
        this.nextButton.interactable = false;
        this.typeText.gameObject.SetActive(false);
    }

    protected void PrepNewGeneration() {
        this.genText.gameObject.SetActive(true);
        this.startButton.interactable = true;
    }

    protected void SetRewardStatus(bool status) {
        foreach(Selectable button in this.rewardButtons) {
            button.interactable = status;
        }
    }

    void Awake() {
        this.genText.gameObject.SetActive(false);
        this.typeText.gameObject.SetActive(false);

        this.SetRewardStatus(false);
        this.startButton.interactable = false;
        this.nextButton.interactable = false;
    }

    public enum BuildStates {
        freshRun,
        typeCreate,
        typeReward,
        typeFinished,
        generationFinished
    }
}
