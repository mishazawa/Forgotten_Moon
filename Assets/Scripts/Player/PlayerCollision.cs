using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skin
public class PlayerCollision : MonoBehaviour {
    Player pl;

    void Awake() {
        pl = transform.parent.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider c) {
        pl.collision(c);
    }
}
