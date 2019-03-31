using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public GameObject owner;
    public float speed = 5f;
    public float lifeTime = 4f;

    void Start() {
        rigidbody.velocity = transform.right * speed;
        StartCoroutine(Explode());
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (owner.name.Length < 1) return;
        if (other.CompareTag("Player") && other.name != owner.name) {
            Ship enemyShip = other.GetComponent<Ship>();
            if (enemyShip.isDead) return;

            enemyShip.TakeDamage();
            if (enemyShip.isDead) {
                owner.GetComponent<Ship>().score++;
            }
        }
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}