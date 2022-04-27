using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBall : MonoBehaviour
{
    public float multiplier = 100f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.right * multiplier);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
