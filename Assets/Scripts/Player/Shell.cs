using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] protected List<Material> usesMats = new List<Material>();
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected int maxUses = 0;

    protected Hopper parent;
    protected bool isActive;
    protected int uses;

    public void Init(Hopper parent, int maxUses) {
        this.parent = parent;

        if (this.maxUses == 0) {
            this.maxUses = maxUses;
        }

        this.meshRenderer.material = this.usesMats[0];
    }

    public void ResetShell() {
        this.isActive = true;
        this.uses = 0;
        this.meshRenderer.enabled = true;
        this.meshRenderer.material = this.usesMats[this.uses];
    }

    public void DisableShell() {
        this.isActive = false;
        this.meshRenderer.enabled = false;
    }

    protected void UpdateMaterial(int uses) {
        uses = Mathf.Clamp(uses, 0, this.usesMats.Count - 1);

        this.meshRenderer.material = this.usesMats[uses];
    }

    void OnTriggerEnter(Collider other) {
        if (!parent.CanBeBounced() || other.isTrigger || !this.isActive) {
            return;
        }

        Vector3 toParent = this.parent.transform.position - this.transform.position;
        toParent = toParent / toParent.magnitude;

        float bounceMultiplier = 1f;

        if(other.tag == LevelManager.superBounceTag) {
            bounceMultiplier = LevelManager.superBounceFactor;
        }

        this.parent.Bounce(toParent, bounceMultiplier);

        this.uses++;
        
        if (this.uses >= this.maxUses) {
            this.DisableShell();
        } else {
            this.UpdateMaterial(this.uses);
        }
    }

}
