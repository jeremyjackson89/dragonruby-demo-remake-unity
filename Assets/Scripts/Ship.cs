﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public SpriteRenderer sprite;
    public Rigidbody2D rigidbody;
    public Transform bulletSpawn;
    public Transform mineSpawn;
    public Transform flameSpawn;
    public GameObject healthContainer;
    public GameObject hpDot;
    public GameObject bullet;
    public GameObject mine;
    public GameObject flame;

    public int score = 0;
    public int damage = 0;
    public int startX;
    public bool isDead = false;

    private int turnSpeed = 3;
    private int thrust = 5;
    private int maxSpeed = 10;
    private Quaternion healthRotation;
    private bool isPlayerOne;

    void Awake() {
        isPlayerOne = this.gameObject.name == "BlueShip";
        HideShip();
        ResetShip();
        healthRotation = healthContainer.transform.rotation;
    }

    void Update() {
        if (isDead)return;

        if (Input.GetJoystickNames().Length > 0) {
            Debug.Log(Input.GetJoystickNames().Length + " joysticks connected");
            for (int i = 0; i < Input.GetJoystickNames().Length; i++) {
                Debug.Log(Input.GetJoystickNames()[i]);
            }
        }

        float inputTurn = Input.GetAxisRaw("Horizontal");
        bool inputMove = Input.GetButton("Jump");

        if (isPlayerOne) {
            if (Input.GetButtonDown("Fire1")) {
                Shoot();
            }
            if (Input.GetButtonDown("Fire2")) {
                SetMine();
            }
            if (inputTurn != 0f) {
                transform.Rotate(new Vector3(0, 0, -inputTurn * turnSpeed));
            }

            if (inputMove) {
                GameManager.HideInstructions();
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
                rigidbody.AddRelativeForce(Vector2.right * thrust * 0.75f);

                GameObject newFlame = Instantiate(flame, flameSpawn.position, flameSpawn.rotation);
                newFlame.transform.SetParent(transform);
            }
        }

        ResetHealthContainer();
    }

    public void ResetShip() {
        damage = 0;
        transform.position = new Vector2(startX, Random.Range(-3.5f, 3.5f));
        rigidbody.velocity = Vector3.zero;
        float zRotation = isPlayerOne ? 0f : -180f;
        transform.eulerAngles = new Vector3(0, 0, zRotation);
        ResetHealthContainer();
        DrawHealth();
        if (isDead)isDead = false;
    }

    void ResetHealthContainer() {
        healthContainer.transform.rotation = healthRotation;
        float xModifier = isPlayerOne ? -0.7f : -0.5f;
        healthContainer.transform.position = new Vector3(
            (transform.position.x + xModifier),
            (transform.position.y + 0.85f),
            0
        );
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
            damage = 5;
            DrawHealth();
            StartCoroutine(Explode());
        }
    }

    public void TakeDamage() {
        damage++;
        DrawHealth();
        if (damage >= 5) {
            StartCoroutine(Explode());
        }
    }

    void DrawHealth() {
        foreach (Transform child in healthContainer.transform) {
            Destroy(child.gameObject);
        }

        int remainingHP = 5 - damage;
        for (int i = 0; i < remainingHP; i++) {
            Vector3 position = healthContainer.transform.position;
            position.x += i * 0.35f;
            GameObject newHpDot = Instantiate(hpDot, position, healthContainer.transform.rotation);
            newHpDot.transform.SetParent(healthContainer.transform);
        }
    }

    void HideShip() {
        Color color = sprite.color;
        color.a = 0f;
        sprite.color = color;
    }

    IEnumerator Explode() {
        isDead = true;
        HideShip();
        int flamesToMake = Random.Range(10, 20);

        for (int i = 0; i < flamesToMake; i++) {
            GameObject newFlame = Instantiate(flame, transform.position, transform.rotation);
            newFlame.transform.SetParent(transform);
            newFlame.GetComponent<Flame>().speed = 5.5f;
            newFlame.GetComponent<Flame>().lifeTime = 0.5f;
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.StartNewRound();
    }
}