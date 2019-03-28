using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 5f;
    public Rigidbody2D rigidbody;

    void Start() {
        rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            
        }
    }
}