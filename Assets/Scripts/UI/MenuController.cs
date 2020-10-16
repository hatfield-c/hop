using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected RectTransform canvasRect = null;

    [Header("Parameters")]
    [SerializeField] protected List<MenuState> menuStates = new List<MenuState>();
    [SerializeField] protected float animateTime = 1f;

    protected MenuState currentState = null;

    protected Vector3 enterPosition;
    protected Vector3 exitPosition;
    protected Vector2 screenRatio;

    public void ChangeState(MenuState targetState) {

        if(this.currentState != null) {
            this.DisableState(this.currentState);
            this.AnimateMenu(
                this.currentState.GetPanelTransform(),
                this.currentState.GetPanelTransform().anchoredPosition3D,
                this.exitPosition,
                () => {
                    this.AnimateMenu(
                        targetState.GetPanelTransform(),
                        this.enterPosition,
                        Vector3.zero,
                        () => {
                            this.EnableState(targetState);
                            this.currentState = targetState;
                        }
                    );
                }
            );

        } else {
            this.AnimateMenu(
                targetState.GetPanelTransform(),
                this.enterPosition,
                Vector3.zero,
                () => {
                    this.EnableState(targetState);
                    this.currentState = targetState;
                }
            );
        }

    }

    void Awake() {
        CanvasScaler scaler = this.GetComponent<CanvasScaler>();

        this.screenRatio = new Vector2(1, scaler.referenceResolution.x / Screen.width);

        this.enterPosition = new Vector3(0, Screen.height * this.screenRatio.y, 0);
        this.exitPosition = new Vector3(0, -Screen.height * this.screenRatio.y, 0);

        //Debug.Log(this.canvasRect.rect.height);

        if (this.menuStates.Count > 0) {
            foreach(MenuState state in this.menuStates) {
                state.GetPanelTransform().anchoredPosition3D = this.exitPosition;
                this.DisableState(state);
            }

            this.ChangeState(this.menuStates[0]);
        }
    }

    void Update() {
        
    }

    protected void EnableState(MenuState state) {
        foreach (Selectable selectable in state.GetInteractables()) {
            selectable.interactable = true;
        }
    }

    protected void DisableState(MenuState state) {
        foreach(Selectable selectable in state.GetInteractables()) {
            selectable.interactable = false;
        }
    }

    protected void AnimateMenu(
        RectTransform menuTransform, 
        Vector3 start, 
        Vector3 end, 
        System.Action followUp = null
    ) {
        if(followUp == null) {
            followUp = this.Empty;
        }

        menuTransform.anchoredPosition3D = start;

        LeanTween.move(
            menuTransform,
            end,
            this.animateTime
        ).setOnComplete(followUp);
    }

    protected void Empty() { }
}
