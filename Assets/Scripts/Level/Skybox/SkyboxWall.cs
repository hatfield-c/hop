using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxWall : MonoBehaviour
{
    [SerializeField] protected SkyboxTrigger fallTrigger = null;
    [SerializeField] protected Transform pivot = null;
    [SerializeField] protected Transform cloudContainer = null;
    [SerializeField] protected Vector3 orientation = new Vector3();
    [SerializeField] protected float fallAngle = 90f;
    [SerializeField] protected float fallTime = 1f;
    [SerializeField] protected LeanTweenType easeType = LeanTweenType.linear;
    [SerializeField] protected bool flipFallDirection = false;

    protected float curAngle = 0f;

    public void Fall() {
        AnimatedObject cloudBuffer;
        foreach (Transform cloudTransform in this.cloudContainer) {
            cloudBuffer = cloudTransform.gameObject.GetComponent<AnimatedObject>();
            cloudBuffer.StopAnimation();
        }

        LeanTween.value(
            this.gameObject,
            this.FallStep,
            0f,
            this.fallAngle,
            this.fallTime
        ).setEase(this.easeType);
    }

    protected void FallStep(float newValue) {
        float angleDelta = newValue - this.curAngle;

        int dir = this.flipFallDirection ? -1 : 1;
        
        this.transform.RotateAround(this.pivot.position, this.orientation, dir * angleDelta);

        this.curAngle = newValue;
    }

    void Start()
    {
        this.fallTrigger.AddWall(this);
    }
   
}
