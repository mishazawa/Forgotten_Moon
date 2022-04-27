using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TestFollow : MonoBehaviour
{
    public PathCreator pc;
    public EndOfPathInstruction end;
    public float speed = 1;

    private float dist = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
        dist += speed * Time.deltaTime;
        transform.position = pc.path.GetPointAtDistance(dist, end);
        transform.rotation = pc.path.GetRotationAtDistance(dist, end);
    }
}
