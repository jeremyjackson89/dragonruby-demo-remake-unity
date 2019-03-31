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
    public GameObject instructions;
    public GameObject starPrefab;
    public Transform starsContainer;
    private Fade[] fadeItems;

    void Awake() {
        if (!instance) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    void Start() {
        instance.fadeItems = GameObject.FindObjectsOfType<Fade>();
        MakeStars();
        SetScore();
        foreach (Fade fadeItem in instance.fadeItems) {
            fadeItem.FadeIn();
        }
    }

    void Update() {}

    void MakeStars() {
        for (int i = 0; i < 100; i++) {
            GameObject newStar = Instantiate(starPrefab);
            float scale = Random.Range(0.1f, 0.35f);
            newStar.transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
            newStar.transform.localScale = new Vector2(scale, scale);
            newStar.transform.SetParent(starsContainer);
        }
    }

    public static void HideInstructions() {
        instance.instructions.SetActive(false);
    }

    public static void SetScore() {
        instance.blueScore.text = instance.blueShip.score.ToString();
        instance.redScore.text = instance.redShip.score.ToString();
    }

    public static void StartNewRound() {
        SetScore();
        instance.StartCoroutine(ResetShips());
    }

    static IEnumerator ResetShips() {
        float waitTime = 2f;
        instance.fadeItems = GameObject.FindObjectsOfType<Fade>();
        foreach (Fade fadeItem in instance.fadeItems) {
            fadeItem.FadeOut();
        }
        
        yield return new WaitForSeconds(waitTime);
        
        GameObject[] liveBullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in liveBullets) {
            Destroy(bullet);
        }

        instance.blueShip.ResetShip();
        instance.redShip.ResetShip();

        instance.fadeItems = GameObject.FindObjectsOfType<Fade>();
        foreach (Fade fadeItem in instance.fadeItems) {
            fadeItem.FadeIn();
        }

        instance.instructions.SetActive(true);
    }
}