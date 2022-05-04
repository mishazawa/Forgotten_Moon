using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoudiniEngineUnity;

public class TestJoints : MonoBehaviour {
    // Start is called before the first frame update

    HEU_OutputAttributesStore store = null;
    Rigidbody rb;

    List<GameObject> siblings = new List<GameObject>();

    void Awake() {
        store = gameObject.GetComponent<HEU_OutputAttributesStore>();
        if (store == null) return;

        rb = GetComponent<Rigidbody>();
        if (rb == null) return;

        ConfigureBigidbody();
        CreateJoints();
    }

    void SetKinematic (bool val) {
        rb.isKinematic = val;
    }

    void ConfigureBigidbody () {
        SetKinematic(true);

        rb.mass = (float)store.GetAttribute(Constants.MASS_ATTR)?._floatValues[0];

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.maxDepenetrationVelocity = 0.1f;
        rb.ResetInertiaTensor();
        rb.Sleep();
    }

    void CreateJoints() {
        renameItself();

        var breakForce = (float)store.GetAttribute(Constants.BREAK_FORCE_ATTR)?._floatValues[0];
        var points = store.GetAttribute(Constants.CONSTRAINTS_ATTR);
        if (points._stringValues != null) {
            var names = points._stringValues[0].Split(',');

            foreach (Transform child in transform.parent) {
                foreach (string name in names) {
                    if (name == child.name) {
                        var fj = gameObject.AddComponent<FixedJoint>();
                        fj.connectedBody = child.GetComponent<Rigidbody>();
                        fj.enableCollision = true;
                        fj.enablePreprocessing = false;
                        fj.breakForce = breakForce < 0 ? Mathf.Infinity : breakForce;
                    }
                }
            }
        }
    }

    void OnCollisionEnter () {
        SetKinematic(false);
    }

    void renameItself () {
        var name = store.GetAttribute(Constants.NAME_ATTR);
        gameObject.name = name._stringValues[0];
    }
}
