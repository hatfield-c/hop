using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    protected void UpdatePosition() {
        this.transform.position = this.target.transform.position + this.offset;
    }

    void Start() {
        this.UpdatePosition();
    }

    void FixedUpdate() {
        this.UpdatePosition();
    }
}
