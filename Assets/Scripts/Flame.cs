using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {
    public Rigidbody2D rigidbody;
    public SpriteRenderer sprite;
    public float speed = 0.85f;
    public float lifeTime = 0.5f;
    private IEnumerator fadeCoroutine;

    void Start() {
        rigidbody.velocity = -transform.right + Random.insideUnitSphere * speed;
        fadeCoroutine = FadeOut();
        StartCoroutine(fadeCoroutine);
        Destroy(this.gameObject, lifeTime);
    }

    void LateUpdate() {
        if (GameManager.globalFadeInProgress) {
            StopCoroutine(fadeCoroutine);
            SpriteRenderer parentSprite = transform.parent.GetComponent<SpriteRenderer>();
            Color parentSpriteColor = parentSprite.color;
            Color color = sprite.color;
            color.a = parentSpriteColor.a;
            sprite.color = color;
        }
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
    }
}