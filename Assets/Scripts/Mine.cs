using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public GameObject owner;
    public float lifeTime = 10f;

    void Start() {
        StartCoroutine(Explode());
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && other != owner) {
            other.GetComponent<Ship>().TakeDamage();
        }
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}