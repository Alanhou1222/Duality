using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

    void Start() {
        Destroy(gameObject,1f);
    }
}
