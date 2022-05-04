using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasdMovement : MonoBehaviour
{
    public float speed = 10f;

    public float sensitivity = 1000;

    Vector3 eyePos;

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        eyePos = transform.rotation.eulerAngles;
    }
    void Update() {
        input();
    }

    void LateUpdate() {
        look();
    }


    void look () {
        var shiftx = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var shifty = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        eyePos = new Vector3(
            Mathf.Clamp((eyePos.x - shifty) + 90, 0, 150) - 90,
            eyePos.y + shiftx,
            eyePos.z
        );

        transform.rotation = Quaternion.Euler(eyePos);
    }

    void input() {
        var vec = Vector3.zero;
        if (Input.GetKey(KeyCode.Space)) {
            vec += Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            vec += -Vector3.up;
        }
        if (Input.GetKey(KeyCode.W)) {
            vec += transform.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            vec += -transform.forward;
        }
        if (Input.GetKey(KeyCode.A)) {
            vec += -transform.right;
        }
        if (Input.GetKey(KeyCode.D)) {
            vec += transform.right;
        }

        transform.position += (vec * speed * Time.deltaTime);

        if (Input.GetMouseButtonUp(0)) {
            gameObject.GetComponent<Projectile>().Fire(eyePos);
        }
    }
}
