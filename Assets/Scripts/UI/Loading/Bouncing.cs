using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : MonoBehaviour
{
    [SerializeField] protected RectTransform myRect = null;
    [SerializeField] protected float moveTime = 1f;
    [SerializeField] protected List<Transform> path = new List<Transform>();

    void Start()
    {
        this.GoRight();
    }

    void GoRight() {
        LeanTween.move(
            this.gameObject,
            this.path[1],
            this.moveTime
        ).setOnComplete(
            () => {
                this.GoLeft();
            }
        ).setIgnoreTimeScale(true);
    }

    void GoLeft() {
        LeanTween.move(
            this.gameObject,
            this.path[0], 
            this.moveTime
        ).setOnComplete(
            () => {
                this.GoRight();
            }    
        ).setIgnoreTimeScale(true);
    }

}
