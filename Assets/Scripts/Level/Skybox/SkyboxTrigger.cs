using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTrigger : MonoBehaviour
{
    [SerializeField] protected bool canTrigger = true;

    protected List<SkyboxWall> walls = new List<SkyboxWall>();

    public void AddWall(SkyboxWall wall) {
        this.walls.Add(wall);
    }

    void OnTriggerEnter(Collider other) {
        if (!this.canTrigger) {
            return;
        }

        foreach(SkyboxWall wall in this.walls) {
            wall.Fall();
        }

        this.canTrigger = false;
    }
}
