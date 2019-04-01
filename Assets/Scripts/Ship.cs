using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public SpriteRenderer sprite;
    public Rigidbody2D rigidbody;
    public Transform bulletSpawn;
    public Transform mineSpawn;
    public Transform flameSpawn;
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

    void Start() {
        HideShip();
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
            GameManager.HideInstructions();
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
            rigidbody.AddRelativeForce(Vector2.right * thrust * 0.75f);

            GameObject newFlame = Instantiate(flame, flameSpawn.position, flameSpawn.rotation);
            newFlame.transform.SetParent(transform);
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
        if (isDead)isDead = false;
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
            StartCoroutine(Explode());
        }
    }

    public void TakeDamage() {
        damage++;
        if (damage >= 5) {
            StartCoroutine(Explode());
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