using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {
    public float yThreshold = 5.5f;
    public float xThreshold = 9.5f;

    void Update() {
        if (transform.position.x > xThreshold) {
            transform.position = new Vector3(-xThreshold, transform.position.y, 0);
        } else if (transform.position.x < -xThreshold) {
            transform.position = new Vector3(xThreshold, transform.position.y, 0);
        } else if (transform.position.y > yThreshold) {
            transform.position = new Vector3(transform.position.x, -yThreshold, 0);
        } else if (transform.position.y < -yThreshold) {
            transform.position = new Vector3(transform.position.x, yThreshold, 0);
        }
    }
}