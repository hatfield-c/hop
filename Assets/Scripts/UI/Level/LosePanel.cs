using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{

    [SerializeField] protected Image backgroundPanel = null;
    [SerializeField] protected TextMeshProUGUI text = null;
    [SerializeField] protected float fadeLength = 1f;

    public void Display() {
        this.backgroundPanel.CrossFadeAlpha(1f, 0f, true);
        this.backgroundPanel.CrossFadeAlpha(0f, this.fadeLength, true);
        LeanTween.value(
            this.text.gameObject,
            (float newAlpha) => {
                this.text.alpha = newAlpha;
            },
            1,
            0,
            this.fadeLength
        ).setIgnoreTimeScale(true);
    }

    void Start() {
        this.backgroundPanel.CrossFadeAlpha(0f, 0f, true);
        this.text.alpha = 0f;
    }

}
