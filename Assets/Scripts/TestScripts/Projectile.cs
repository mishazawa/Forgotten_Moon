using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject prefab;

    public void Fire (Vector3 dir) {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
