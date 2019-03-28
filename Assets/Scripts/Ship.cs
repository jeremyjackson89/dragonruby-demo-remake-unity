using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject mine;

    public int damage = 0;
    private bool isDead = false;
    private int maxAlpha = 255;
    private int speed = 3;

    void Start() {}

    void Update() {
        float inputTurn = Input.GetAxisRaw("Horizontal");
        bool inputMove = Input.GetKey(KeyCode.Space);

        if (inputTurn != 0f) {
            transform.Rotate(new Vector3(0, 0, -inputTurn * speed));
        }

        if (inputMove) {
            rigidbody.AddRelativeForce(Vector2.right * speed / 2);
        }

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}