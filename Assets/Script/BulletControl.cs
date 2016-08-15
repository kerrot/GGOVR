using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletControl : MonoBehaviour {
    public float WaitTime;
    public float speed;

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

    Scoremanager scoreMgr;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        startTime = Time.time;
        sound = GetComponent<AudioSource>();
        scoreMgr = GameObject.FindObjectOfType<Scoremanager>();
    }

    public void SetTarget(Vector3 position)
    {
        transform.LookAt(position);
    }

    void Update()
    {
        double leftTime = WaitTime;
        if (!start)
        {
            if (Time.time - startTime > WaitTime)
            {
                start = true;
                sound.Play();
                timeText.gameObject.SetActive(false);
                GetComponent<Collider>().enabled = true;

                body.velocity = transform.forward * speed;
                
                if (OnFire != null)
                {
                    OnFire();
                }
            }
            else
            {
                
                leftTime = Math.Round(WaitTime - (Time.time - startTime), 1, MidpointRounding.AwayFromZero);
                timeText.text = leftTime.ToString();
            }
        }

        transform.LookAt(transform.position + body.velocity);

        hitPoint.SetActive(false);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hitPoint.SetActive(true);
                hitPoint.transform.position = hit.point;

                Camera ca = Camera.main;
                if (ca == null)
                {
                    ca = GameObject.FindObjectOfType<Camera>();
                }

                hitPoint.transform.LookAt(hitPoint.transform.position + ca.transform.rotation * Vector3.forward,
                ca.transform.rotation * Vector3.up);

                scoreMgr.AddPredictHit(leftTime);
            }
        }
    }

    public void SetVelocity(Vector3 v)
    {
        body.velocity = v.normalized * speed;
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
