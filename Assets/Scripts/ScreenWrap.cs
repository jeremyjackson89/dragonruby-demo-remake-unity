using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {
    public float distanceY = 5.5f;
    public float distanceX = 9.5f;

    void Update() {
        if (transform.position.x > distanceX) {
            transform.position = new Vector3(-distanceX, transform.position.y, 0);
        }
        if (transform.position.x < -distanceX) {
            transform.position = new Vector3(distanceX, transform.position.y, 0);
        }
        if (transform.position.y > distanceY) {
            transform.position = new Vector3(transform.position.x, -distanceY, 0);
        }
        if (transform.position.y < -distanceY) {
            transform.position = new Vector3(transform.position.x, distanceY, 0);
        }
    }
}