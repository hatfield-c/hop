using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : MonoBehaviour
{

    [SerializeField] protected List<Selectable> interactList = new List<Selectable>();
    [SerializeField] protected string id = null;
    [SerializeField] protected RectTransform rectTransform = null;

    public List<Selectable> GetInteractables() {
        return this.interactList;
    }

    public string GetId() {
        return this.id;
    }

    public RectTransform GetPanelTransform() {
        return this.rectTransform;
    }

    public virtual void OnTransitionTo() { }
}
