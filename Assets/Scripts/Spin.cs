using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [Header("Parameters")]
    public float radius = 3f;
    public int shellCount = 8;
    public int uses = 3;
    public float startAngle = 0f;
    public float turnForce = 0.1f;
    public float bounceForce = 1f;
    public float cooldown = 0.08f;
    public Vector3 startPosition = new Vector3(0, 10, 0);

    [Header("References")]
    public Rigidbody body;
    public GameObject shellPrefab;

    protected float bouncedown = 0f;
    public bool canBounce = true;

    public List<Shell> shellList = new List<Shell>();

    public void Bounce(Vector3 dir) {
        if(!this.canBounce) {
            return;
        }

        this.body.velocity = Vector3.zero;
        this.body.angularVelocity = Vector3.zero;

        this.body.AddForce(dir * this.bounceForce);
        this.canBounce = false;
    }

    public void Reset() {
        this.transform.position = this.startPosition;
        this.transform.rotation = Quaternion.identity;
        this.body.velocity = Vector3.zero;
        this.body.angularVelocity = Vector3.zero;
        
        this.BuildShell();

        this.body.Sleep();
    }

    protected void BuildShell() {
        this.DestroyShell();

        this.BuildSubshell(Vector3.right, Vector3.forward);
        this.BuildSubshell(Vector3.right, Vector3.up);
        this.BuildSubshell(Vector3.forward, Vector3.up);
    }

    protected void BuildSubshell(Vector3 xMult, Vector3 yMult) {
        float angle = startAngle;
        float angleStep = 360f / this.shellCount;

        Vector3 xPos;
        Vector3 yPos;
        Vector3 pos;

        for (int i = 0; i < this.shellCount; i++) {
            xPos = Mathf.Cos(angle * Mathf.Deg2Rad) * this.radius * xMult;
            yPos = Mathf.Sin(angle * Mathf.Deg2Rad) * this.radius * yMult;

            pos = xPos + yPos;

            GameObject shellObject = Instantiate(this.shellPrefab);
            shellObject.transform.position = pos + this.transform.position;

            Shell shell = shellObject.GetComponent<Shell>();
            shell.Init(this, this.uses);
            this.shellList.Add(shell);

            angle += angleStep;
        }
    }

    protected void DestroyShell() {
        for(int i = 0; i < this.shellList.Count; i++) {
            Shell shell = this.shellList[i];
            Destroy(shell.gameObject);
        }

        this.shellList.Clear();
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Lose! Try again!");
        this.Reset();
    }

    void FixedUpdate() {
        if(!this.canBounce) {
            this.bouncedown += Time.deltaTime;
        }

        if(this.bouncedown >= this.cooldown) {
            this.canBounce = true;
            this.bouncedown = 0f;
        }

    }

    void Start() {
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
