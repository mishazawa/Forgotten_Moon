using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoudiniEngineUnity;
using PathCreation;

public class DistanceOnCurve : MonoBehaviour
{
    public HEU_HoudiniAssetRoot curve;
    public EndOfPathInstruction end;
    public GameObject loop;

    private VertexPath path;

    void Awake () {
        init();
    }

    public Vector3 GetDist(float percent) {
        return path.GetPointAtDistance(percent, end);
    }

    public Quaternion GetRotation(float percent) {
        return path.GetRotationAtDistance(percent, end);
    }

    private void init() {
        if (curve.HoudiniAsset.Curves[0] == null) return;
        var mesh = curve.HoudiniAsset.Curves[0].TargetGameObject.GetComponent<MeshFilter>().mesh;
        path = new VertexPath(new BezierPath(mesh.vertices), curve.gameObject.transform);
        Debug.Log("Init path");

        loop.transform.position = mesh.vertices[0];
        loop.transform.rotation = GetRotation(0);
    }
}
