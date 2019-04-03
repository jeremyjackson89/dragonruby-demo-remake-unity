using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public GameObject owner;
    public GameObject flame;
    public float speed = 5f;
    public float lifeTime = 4f;

    void Start() {
        rigidbody.velocity = transform.right * speed;
        StartCoroutine(SelfDetonate());
    }

    void LateUpdate() {
        if (GameManager.globalFadeInProgress) {
            SpriteRenderer parentSprite = owner.GetComponent<SpriteRenderer>();
            Color parentSpriteColor = parentSprite.color;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = parentSpriteColor.a;
            GetComponent<SpriteRenderer>().color = color;
        }
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
            Explode();
            Destroy(this.gameObject);
        }
    }

    IEnumerator SelfDetonate() {
        yield return new WaitForSeconds(lifeTime);
        Explode();
        Destroy(this.gameObject);
    }

    void Explode() {
        int flamesToMake = Random.Range(7, 12);
        for (int i = 0; i < flamesToMake; i++) {
            GameObject newFlame = Instantiate(flame, transform.position, transform.rotation);
            newFlame.transform.SetParent(owner.transform);
            newFlame.GetComponent<Flame>().speed = 4f;
            newFlame.GetComponent<Flame>().lifeTime = 0.33f;
        }
    }
}