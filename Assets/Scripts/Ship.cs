using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public Transform bulletSpawn;
    public Transform mineSpawn;
    public GameObject bullet;
    public GameObject mine;

    public int damage = 0;
    public int startX;

    private bool isDead = false;
    private int maxAlpha = 255;
    private int turnSpeed = 3;
    private int thrust = 5;
    private int maxSpeed = 10;

    void Start() {
        transform.position = new Vector2(startX, transform.position.y);
    }

    void Update() {
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

    void Shoot() {
        GameObject newBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        newBullet.GetComponent<Bullet>().owner = this.gameObject;
    }

    void SetMine() {
        GameObject newMine = Instantiate(mine, mineSpawn.position, mineSpawn.rotation);
        newMine.GetComponent<Mine>().owner = this.gameObject;
    }

    public void TakeDamage() {
        damage++;
        if (damage >= 5) {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(0.25f);
    }
}