using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatedObject : AbstractResettable
{
    public Transform pathContainer;
    public List<Transform> pathList;
    public float animationTime = 1f;
    public LeanTweenType easeType = LeanTweenType.linear;
    public bool orientToPath = true;
    public bool animate = true;

    protected Vector3[] path;
    protected int tweenId = -1;

    public override void ResetObject() {
        if(this.pathList.Count < 4 || !this.animate) {
            return;
        }

        if(this.tweenId > -1) {
            LeanTween.cancel(this.tweenId);
        }

        this.ActivateAnimation();
    }

    public void StopAnimation() {
        this.animate = false;
        LeanTween.cancel(this.tweenId);
        this.tweenId = -1;
    }

    public void StartAnimation() {
        this.animate = true;
        this.ResetObject();
    }

    protected void ActivateAnimation() {
        this.tweenId = LeanTween.moveSpline(
            this.gameObject,
            this.path,
            this.animationTime
        ).setLoopPingPong().setOrientToPath(this.orientToPath).setEase(this.easeType).id;
    }

    void Start(){
        if(this.pathList.Count < 4) {
            return;
        }

        this.path = new Vector3[this.pathList.Count];
        for(int i = 0; i < this.pathList.Count; i++) {
            this.path[i] = this.pathList[i].position;
        }

        this.ResetObject();
    }


}
