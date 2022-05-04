using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoudiniEngineUnity;

[ExecuteInEditMode]
public class CalcVector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            var store = transform.parent.GetComponent<HEU_OutputAttributesStore>();
            if (store == null) return;



            var resetVector = store.GetAttribute(Constants.RESET_VECTOR_ATTR);
            var v = new Vector3(resetVector._floatValues[0] * -1, resetVector._floatValues[1], resetVector._floatValues[2]);

            Debug.Log("Name: "+ gameObject.name);
            Debug.Log("V: "+ v);
            Debug.Log("-------------------");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
