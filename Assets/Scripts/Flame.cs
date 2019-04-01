using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public SpriteRenderer sprite;
    public float speed = 10f;
    public float lifeTime = 0.5f;

    void Start() {
        rigidbody.velocity = -transform.right + Random.insideUnitSphere * 0.85f;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        float elapsedTime = 0f;
        Color color = sprite.color;
        while (elapsedTime < lifeTime) {
            yield return null;
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / lifeTime);
            sprite.color = color;
        }
        Destroy(this.gameObject);
    }
}