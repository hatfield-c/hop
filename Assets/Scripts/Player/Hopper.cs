using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Hopper : MonoBehaviour
{
    [Header("Parameters")]
    public int uses = 3;
    public float turnForce = 0.1f;
    public float bounceForce = 1f;
    public float cooldown = 0.08f;

    [Header("References")]
    [SerializeField] protected Rigidbody body = null;
    [SerializeField] protected Transform shellContainer = null;

    protected bool canBounce = true;

    protected Vector3 startPosition;
    protected List<Shell> shellList = new List<Shell>();

    public void Bounce(Vector3 dir) {
        if(!this.canBounce) {
            return;
        }

        this.body.velocity = Vector3.zero;
        this.body.angularVelocity = Vector3.zero;

        this.body.AddForce(dir * this.bounceForce);
        this.canBounce = false;

        LeanTween.delayedCall(
            this.cooldown,
            () => {
                this.canBounce = true;
            }
        );
    }

    public void Reset() {
        this.transform.position = this.startPosition;
        this.transform.rotation = Quaternion.identity;
        this.body.velocity = Vector3.zero;
        this.body.angularVelocity = Vector3.zero;
        
        this.ResetShell();

        this.body.Sleep();
        this.body.AddForce(Vector3.up);
    }

    public bool CanBeBounced() {
        return this.canBounce;
    }

    protected void BuildShell() {
        foreach(Transform shellTransform in this.shellContainer) {
            Shell shell = shellTransform.gameObject.GetComponent<Shell>();

            shell.Init(this, this.uses);
            this.shellList.Add(shell);
        }
    }

    protected void ResetShell() {
        foreach (Shell shell in this.shellList) {
            shell.ResetShell();
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Lose! Try again!");
        this.Reset();
    }

    void Start() {
        this.startPosition = this.transform.position;
        this.BuildShell();
        this.Reset();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            this.body.AddTorque(Vector3.right * turnForce);
        }
        if (Input.GetKey(KeyCode.S)) {
            this.body.AddTorque(-Vector3.right * turnForce);
        }
        if (Input.GetKey(KeyCode.A)) {
            this.body.AddTorque(Vector3.forward * turnForce);
        }
        if (Input.GetKey(KeyCode.D)) {
            this.body.AddTorque(-Vector3.forward * turnForce);
        }
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
