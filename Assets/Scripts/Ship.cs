using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public Transform bulletSpawn;
    public Transform mineSpawn;
    public GameObject bullet;
    public GameObject mine;

    public int score = 0;
    public int damage = 0;
    public int startX;
    public bool isDead = false;

    private int maxAlpha = 255;
    private int turnSpeed = 3;
    private int thrust = 5;
    private int maxSpeed = 10;

    void Start() {
        ResetShip();
    }

    void Update() {
        if (isDead)return;

        float inputTurn = Input.GetAxisRaw("Horizontal");
        bool inputMove = Input.GetButton("Jump");

        if (inputTurn != 0f) {
            transform.Rotate(new Vector3(0, 0, -inputTurn * turnSpeed));
        }

        if (inputMove) {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
            rigidbody.AddRelativeForce(Vector2.right * thrust * 0.75f);
        }

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2")) {
            SetMine();
        }
    }

    public void ResetShip() {
        damage = 0;
        transform.position = new Vector2(startX, Random.Range(-3.5f, 3.5f));
        rigidbody.velocity = Vector3.zero;
        float zRotation = this.gameObject.name == "BlueShip" ? 0f : -180f;
        transform.eulerAngles = new Vector3(0, 0, zRotation);
        isDead = false;
    }

    void Shoot() {
        GameObject newBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        newBullet.GetComponent<Bullet>().owner = this.gameObject;
    }

    void SetMine() {
        GameObject newMine = Instantiate(mine, mineSpawn.position, mineSpawn.rotation);
        newMine.GetComponent<Bullet>().owner = this.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isDead = true;
            StartCoroutine(Explode());
        }
    }

    public void TakeDamage() {
        damage++;
        if (damage >= 5) {
            isDead = true;
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(0.10f);
        // Destroy(this.gameObject);
        GameManager.StartNewRound();
    }
}