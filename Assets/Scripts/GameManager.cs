using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public Ship BlueShip;
    public Ship RedShip;
    public Text BlueScore;
    public Text RedScore;
    public Text Instructions;

    void Awake() {
        if (!instance) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    void Start() {
        SetScore();
    }

    void Update() {

    }

    public static void SetScore() {
        instance.BlueScore.text = instance.BlueShip.score.ToString();
        instance.RedScore.text = instance.RedShip.score.ToString();
    }

    public static void StartNewRound() {
        SetScore();
        // fade everything out
        instance.BlueShip.ResetShip();
        instance.RedShip.ResetShip();
    }
}