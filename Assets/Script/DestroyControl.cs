using UnityEngine;
using System.Collections;

// for clean the bullet dodged
public class DestroyControl : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
