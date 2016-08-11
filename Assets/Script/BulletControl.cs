using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletControl : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private GameObject hitPoint;
    [SerializeField]
    private TextMesh timeText;

    public delegate void BulletEvent();
    public BulletEvent OnFire;

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
                timeText.gameObject.SetActive(false);

                if (OnFire != null)
                {
                    OnFire();
                }
            }
            else
            {
                
                double t = Math.Round(waitTime - (Time.time - startTime), 1, MidpointRounding.AwayFromZero);
                timeText.text = t.ToString();
            }
        }

        hitPoint.SetActive(false);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hitPoint.SetActive(true);
                hitPoint.transform.position = hit.point;
                hitPoint.transform.LookAt(hitPoint.transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up);
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
            Destroy(gameObject);
            PlayerControl player = GameObject.FindObjectOfType<PlayerControl>();
            if (player != null)
            {
                player.BloodEffect();
            }
        }
    }
}
