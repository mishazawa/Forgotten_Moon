using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoudiniEngineUnity;

public class Player : MonoBehaviour {
    private float speed = 0f;
    private float yaw   = 0f;
    private float dist  = 0f;

    public DistanceOnCurve curve;
    private int laps = 0;

    void Update () {
        input();
        move();
        rotate();
    }

    public void Turn (int direction = Constants.RIGHT) {
        yaw = transform.eulerAngles.z + direction * Constants.ANGULAR_SPEED * speed * Time.deltaTime;
    }

    public void Accelerate (int direction = Constants.FORWARD) {
        speed += Constants.ACCELERATION * direction * Time.deltaTime;
        speed = Mathf.Clamp(speed, Constants.MIN_SPEED, Constants.MAX_SPEED);
    }

    private void move() {
        dist += speed * Time.deltaTime;
        transform.position = curve.GetDist(dist);
    }

    private void rotate () {
        var pathTangent  = curve.GetRotation(dist);
        var pathRotation = Quaternion.Lerp(transform.rotation, pathTangent, Constants.LERP_SPEED * Time.deltaTime).eulerAngles;
        pathRotation.z = yaw;
        transform.eulerAngles = pathRotation;
    }

    private void input() {
        if (Input.GetKey(KeyCode.S)) Accelerate(Constants.BACKWARD * Constants.DECELERATION);

        if (Input.GetKey(KeyCode.W)) {
            Accelerate(Constants.FORWARD);
        } else {
            Accelerate(Constants.BACKWARD);
        }
    }


    private void loop() {
        Debug.Log("Laps: " + laps++);
    }

    public void collision(Collider col) {
        // exception for loop
        // it is handmade =)
        if (col.transform.name == "Loop") {
            loop();
            return;
        }

        // ignore collider if type is undefined
        var store = col.transform.parent.GetComponent<HEU_OutputAttributesStore>();
        if (store == null) return;

        var collisionType = store.GetAttribute(Constants.COLLISION_TYPE_ATTR);

        if (collisionType == null) return;

        var val = (Constants.Collision)collisionType._intValues[0];

        if (val == Constants.Collision.OBSTACLE) {
            var resetVector = store.GetAttribute(Constants.RESET_VECTOR_ATTR);

            var vec = new Vector3(resetVector._floatValues[0] * -1, resetVector._floatValues[1], resetVector._floatValues[2]);

            var pos = col.transform.position;
            var eulers = col.transform.localRotation.eulerAngles;

            var npos = Quaternion.Euler(eulers) * (pos + vec);
            Debug.Log(npos + " " + (pos - vec));
            var collider_center = new Vector3(transform.position.x, transform.position.y, col.transform.position.z);

            dist -= 10 * Time.deltaTime * 10;
            speed = 0f;

            // yaw = col.transform.eulerAngles.z;
        }
        if (val == Constants.Collision.CHECKPOINT) {
            Debug.Log("CHECKPOINT");
        }
    }
}
