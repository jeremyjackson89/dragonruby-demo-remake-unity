﻿using System.Collections;
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
        if (other.CompareTag("Player") && other != owner) {
            other.GetComponent<Ship>().TakeDamage();
        }
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}