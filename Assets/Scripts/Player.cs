using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using HoudiniEngineUnity;

public class Player : MonoBehaviour {
    public HEU_HoudiniAssetRoot curve;
    public EndOfPathInstruction end;

    private VertexPath path;

    private float dist  = 0f;
    private float speed = 0f;
    private float yaw   = 0f;

    void Start () {
        init();
    }

    void Update() {
        input();
        move();
        rotate();
        loop();
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
        transform.position = path.GetPointAtDistance(dist, end);
    }

    private void rotate () {
        var pathTangent  = path.GetRotationAtDistance(dist, end);
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

    private void init() {
        if (curve.HoudiniAsset.Curves[0] == null) return;
        var mesh = curve.HoudiniAsset.Curves[0].TargetGameObject.GetComponent<MeshFilter>().mesh;
        path = new VertexPath(new BezierPath(mesh.vertices), curve.gameObject.transform);
        Debug.Log("Init path");
    }

    private void loop() {}
}
