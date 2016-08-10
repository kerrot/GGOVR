using UnityEngine;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PredictControl : MonoBehaviour {
    [SerializeField]
    private GameObject Hit;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.contacts.ToList().ForEach(c =>
            {
                Vector3 p = Camera.main.WorldToScreenPoint(c.point);
            });
            
        }
    }
}
