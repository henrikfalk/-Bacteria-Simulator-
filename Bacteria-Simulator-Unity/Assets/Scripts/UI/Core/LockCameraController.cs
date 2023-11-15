using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraController : MonoBehaviour
{

    public GameObject selectedObject; // { private get; set; }

    void Start() {
    }

    void Update()
    {
        if (selectedObject != null) {
            transform.position = selectedObject.transform.position + new Vector3(0, 2f, -5);
        }
    }
    
}
