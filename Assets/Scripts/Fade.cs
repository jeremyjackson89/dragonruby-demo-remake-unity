using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
    private float fadeTime = 2f;
    public SpriteRenderer sprite;

    public void FadeIn() {
        StartCoroutine(FadeSprite(true));
    }

    public void FadeOut() {
        StartCoroutine(FadeSprite(false));
    }

    IEnumerator FadeSprite(bool fadeIn) {
        float elapsedTime = 0f;
        Color color = sprite.color;
        if (!fadeIn && color.a == 0f) {
            yield return null;
        } else {
            while (elapsedTime < fadeTime) {
                yield return null;
                elapsedTime += Time.deltaTime;
                if (fadeIn) {
                    color.a = Mathf.Clamp01(elapsedTime / fadeTime);
                } else {
                    color.a = 1f - Mathf.Clamp01(elapsedTime / fadeTime);
                }
                sprite.color = color;
            }
        }

    }
}