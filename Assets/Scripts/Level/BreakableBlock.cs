using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : AbstractResettable
{
    [SerializeField] protected List<Material> usesMats = new List<Material>();
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected int maxUses = 3;
    [SerializeField] protected int minUses = 0;

    protected int uses;
    protected bool canCollide = true;

    public override void ResetObject() {
        this.gameObject.SetActive(true);
        this.uses = this.minUses;
        this.UpdateMaterial(this.uses);
    }

    public void DisableBlock() {
        this.gameObject.SetActive(false);
    }

    protected void UpdateMaterial(int uses) {
        uses = Mathf.Clamp(uses, 0, this.usesMats.Count - 1);

        this.meshRenderer.material = this.usesMats[uses];
    }

    void OnTriggerEnter(Collider collider) {
        if (!this.canCollide) {
            return;
        }

        this.uses++;
        this.canCollide = false;

        LeanTween.delayedCall(
            0.02f,
            () => {
                this.canCollide = true;
            }
        );

        if (this.uses >= this.maxUses) {
            LeanTween.delayedCall(
                0.02f,
                () => {
                    this.DisableBlock();
                }
            );
        } else {
            this.UpdateMaterial(this.uses);
        }
    }

    void Start() {
        this.uses = this.minUses;
        this.UpdateMaterial(this.uses);
    }
}
