using UnityEngine;
using UnityEngine.VR;
using System.Collections;

// for recenter function
public class VRControl : MonoBehaviour {
    void Update () {
        if (Input.GetKey(KeyCode.R))
        {
            InputTracking.Recenter();
        }
    }
}
