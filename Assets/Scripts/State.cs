using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            player.Turn(Constants.LEFT);
        }

        if (Input.GetKey(KeyCode.D)) {
            player.Turn(Constants.RIGHT);
        }
    }
}
