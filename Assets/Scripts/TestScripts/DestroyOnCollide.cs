using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    public float strength = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * strength * rb.mass);
        StartCoroutine(lifespan());
    }

    IEnumerator lifespan() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
