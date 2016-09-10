using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class BulletControl : MonoBehaviour {
    public float WaitTime;
    public float speed;

    [SerializeField]
    private GameObject hitPoint;
    [SerializeField]
    private GameObject warnPoint;
    [SerializeField]
    private TextMesh timeText;
    [SerializeField]
    private GameObject line;
    [SerializeField]
    private Color hitColor;
    [SerializeField]
    private Color missColor;

    private Material lineMat;

    public delegate void BulletEvent();
    public BulletEvent OnFire;

    private GameObject target;

    Rigidbody body;
    float startTime;
    bool start = false;
    AudioSource sound;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        startTime = Time.time;
        sound = GetComponent<AudioSource>();
        lineMat = line.GetComponent<MeshRenderer>().material;
    }

    public void SetTarget(Vector3 position)
    {
        transform.LookAt(position);
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    void Update()
    {
        CheckFire();

        ShowPredict();

        FocusTarget();
    }

    void FocusTarget()
    {
        if (start)
        {
            transform.LookAt(transform.position + body.velocity);
        }
        else if (target != null)
        {
            transform.LookAt(target.transform);
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

    void ShowPredict()
    {
        hitPoint.SetActive(false);
        warnPoint.SetActive(false);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward);
        if (hits.Length > 0)
        {
            RaycastHit p = hits.FirstOrDefault(h => h.collider.gameObject.GetComponent<PlayerControl>() != null);
            if (p.collider != null)
            {
                lineMat.color = hitColor;
                RaycastHit hit = hits.FirstOrDefault(h => h.collider.gameObject.tag == "UI");
                if (hit.collider != null)
                {
                    hitPoint.SetActive(true);
                    hitPoint.transform.position = hit.point;

                    FaceCamera(hitPoint);
                }
                else
                {
                    warnPoint.SetActive(true);
                    warnPoint.transform.position = ComputeWarnPosition(p);
                    FaceCamera(warnPoint);
                }
                return;
            }
        }

        lineMat.color = missColor;
    }

    void FaceCamera(GameObject obj)
    {
        Camera ca = Camera.main;
        if (ca == null)
        {
            ca = GameObject.FindObjectOfType<Camera>();
        }

        obj.transform.LookAt(obj.transform.position + ca.transform.rotation * Vector3.forward, ca.transform.rotation * Vector3.up);
    }

    Vector3 ComputeWarnPosition(RaycastHit hit)
    {
        Vector3 local = hit.collider.transform.InverseTransformPoint(hit.point);
        BoxCollider box = hit.collider as BoxCollider;
        local.x /= box.size.x;
        local.y /= box.size.y;

        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        Vector3 uiCollider = ui.GetComponent<BoxCollider>().size;
        Vector3 uiLocal = new Vector3(uiCollider.x * local.x, 0, 
                                      uiCollider.z * -local.y);

        return ui.transform.TransformPoint(uiLocal);
    }

    void CheckFire()
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
    }
}
