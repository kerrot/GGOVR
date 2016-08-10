using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletControl : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private GameObject hitPoint;

    Rigidbody body;
    float startTime;
    bool start = false;
    AudioSource sound;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        startTime = Time.time;
        sound = GetComponent<AudioSource>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.LookAt(player.transform);
        }
    }

    void Update()
    {
        if (!start)
        {
            if (Time.time - startTime > waitTime)
            {
                start = true;
                sound.Play();
            }
            else
            {
                int t = (int)(waitTime - (Time.time - startTime));
            }
        }

        hitPoint.SetActive(false);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hitPoint.SetActive(true);
                //Vector3 p = Camera.main.WorldToScreenPoint(hit.point);
                hitPoint.transform.position = hit.point;
            }
        }
    }

    void FixedUpdate()
    {
        if (start)
        {
            body.velocity = transform.forward * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}
