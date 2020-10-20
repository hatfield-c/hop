using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected RectTransform canvasRect = null;
    [SerializeField] protected RectTransform bgRect = null;

    [Header("Parameters")]
    [SerializeField] protected float bgPadding;
    [SerializeField] protected float speed;

    protected Vector2 bottomLeft;
    protected Vector2 topRight;
    protected float newPadding;

    protected Vector3 target = new Vector3();

    protected void RandomizeTarget() {
        float side = UnityEngine.Random.Range(0, 4);

        float targetX = 0;
        float targetY = 0;

        switch (side) {
            case 0:
                targetX = this.bottomLeft.x;
                targetY = UnityEngine.Random.Range(this.bottomLeft.y, this.topRight.y);
                break;
            case 1:
                targetX = UnityEngine.Random.Range(this.bottomLeft.x, this.topRight.x);
                targetY = this.topRight.y;
                break;
            case 2:
                targetX = this.topRight.x;
                targetY = UnityEngine.Random.Range(this.bottomLeft.y, this.topRight.y);
                break;
            case 3:
                targetX = UnityEngine.Random.Range(this.bottomLeft.x, this.topRight.x);
                targetY = this.bottomLeft.y;
                break;
        }

        this.target.x = targetX;
        this.target.y = targetY;
        this.target.z = this.bgRect.position.z;
    }

    protected float CalcMoveTime() {
        float distance = Vector3.Distance(this.bgRect.position, this.target);

        return distance / this.speed;
    }

    protected void Animate() {
        LeanTween.move(
            this.transform.gameObject,
            this.target,
            this.CalcMoveTime()
        ).setOnComplete(
            () => {
                this.RandomizeTarget();
                this.Animate();
            }
        ).setIgnoreTimeScale(true);
    }

    void Awake()
    {
        CanvasScaler scaler = this.canvasRect.GetComponent<CanvasScaler>();

        float screenRatio = scaler.referenceResolution.x / Screen.width;
        this.newPadding = this.bgPadding / screenRatio;

        this.bottomLeft = new Vector2((Screen.width / 2) - newPadding, (Screen.height / 2) - newPadding);
        this.topRight = new Vector2((Screen.width / 2) + newPadding, (Screen.height / 2) + newPadding);

        this.RandomizeTarget();
        this.Animate();
    }

    void Update() {
        
    }
}
