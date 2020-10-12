using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] List<Material> usesMats = new List<Material>();
    public MeshRenderer meshRenderer;

    protected Spin parent;
    protected int uses;
    protected int maxUses;

    public void Init(Spin parent, int maxUses) {
        this.transform.parent = parent.transform;
        this.parent = parent;
        this.maxUses = maxUses;

        this.meshRenderer.material = this.usesMats[0];
    }

    void OnTriggerEnter(Collider other) {
        if (!parent.canBounce || other.isTrigger) {
            return;
        }

        Vector3 toParent = this.parent.transform.position - this.transform.position;
        toParent = toParent / toParent.magnitude;
        this.parent.Bounce(toParent);

        this.uses++;
        
        if (this.uses >= this.maxUses) {
            this.parent.shellList.Remove(this);
            Destroy(this.gameObject);
        } else {
            this.meshRenderer.material = this.usesMats[this.uses];
        }
    }

}
