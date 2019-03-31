using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public Ship blueShip;
    public Ship redShip;
    public Text blueScore;
    public Text redScore;
    public Text instructions;
    public Transform starsContainer;
    public GameObject starPrefab;

    void Awake() {
        if (!instance) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    void Start() {
        MakeStars();
        SetScore();
    }

    void Update() {
    }

    void MakeStars() {
        for (int i = 0; i < 100; i++) {
            GameObject newStar = Instantiate(starPrefab);
            float scale = Random.Range(0.1f, 0.35f);
            newStar.transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
            newStar.transform.localScale = new Vector2(scale, scale);
            newStar.transform.SetParent(starsContainer);
        }
    }

    public static void SetScore() {
        instance.blueScore.text = instance.blueShip.score.ToString();
        instance.redScore.text = instance.redShip.score.ToString();
    }

    public static void StartNewRound() {
        SetScore();
        // fade everything out
        instance.blueShip.ResetShip();
        instance.redShip.ResetShip();
    }
}