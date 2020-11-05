using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Hopper : AbstractResettable {
    [Header("Parameters")]
    [SerializeField] protected int uses = 3;
    [SerializeField] protected float turnForce = 0.1f;
    [SerializeField] protected float bounceForce = 1f;
    [SerializeField] protected float cooldown = 0.08f;

    [Header("Metadata")]
    [SerializeField] protected Sprite previewImage = null;
    [SerializeField] protected string previewName = "";

    [Header("References")]
    [SerializeField] protected Rigidbody body = null;
    [SerializeField] protected Transform shellContainer = null;

    public Action loseAction = null;

    protected bool canBounce = true;
    protected PlayerInput playerInput = new PlayerInput();

    protected Vector3 startPosition;
    protected List<Shell> shellList = new List<Shell>();

    public void Bounce(Vector3 dir, float multiplier = 1) {
        if(!this.canBounce) {
            return;
        }

        this.body.velocity = Vector3.zero;
        this.body.angularVelocity = Vector3.zero;

        this.body.AddForce(dir * this.bounceForce * multiplier);
        this.canBounce = false;

        LeanTween.delayedCall(
            this.cooldown,
            () => {
                this.canBounce = true;
            }
        );
    }

    public override void ResetObject() {
        this.transform.position = this.startPosition;
        this.transform.rotation = Quaternion.identity;
        this.body.velocity = Vector3.zero;
        this.body.angularVelocity = Vector3.zero;
        
        this.ResetShell();

        this.body.Sleep();
        this.body.AddForce(Vector3.up);
    }

    public void Die() {
        this.loseAction?.Invoke();
    }

    public bool CanBeBounced() {
        return this.canBounce;
    }

    public Sprite GetPreviewImage() {
        return this.previewImage;
    }

    public string GetPreviewName() {
        return this.previewName;
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
        this.Die();
    }

    void Start() {
        this.startPosition = this.transform.position;
        this.BuildShell();
        this.ResetObject();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            this.playerInput.SetVertical(1);
        }
        if (Input.GetKey(KeyCode.S)) {
            this.playerInput.SetVertical(-1);
        }
        if (Input.GetKey(KeyCode.A)) {
            this.playerInput.SetHorizontal(1);
        }
        if (Input.GetKey(KeyCode.D)) {
            this.playerInput.SetHorizontal(-1);
        }
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void FixedUpdate() {
        this.body.AddTorque(Vector3.right * this.turnForce * this.playerInput.GetVertical());
        this.body.AddTorque(Vector3.forward * this.turnForce * this.playerInput.GetHorizontal());

        this.playerInput.Clear();
    }
}
