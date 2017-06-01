using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// control bullet when to fire, where to fire, and behavior in this process
[RequireComponent(typeof(Rigidbody))]
public class BulletControl : MonoBehaviour {
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

    public bool IsHit { get { return lineMat.color == hitColor; } }

    public delegate void BulletEvent();
    public BulletEvent OnFire;          //call back when bullet fire

    private float waitTime;
    public float WaitTime
    {
        get { return waitTime; }
        set { waitTime = value; }
    }
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private GameObject target;  // the target to attack
    private Material lineMat;   
    
    float startTime;        //the time when ready to fire
    bool isfired = false;
    AudioSource sound;
    SphereCollider coll;
    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        startTime = Time.time;
        sound = GetComponent<AudioSource>();
        lineMat = line.GetComponent<MeshRenderer>().material;
        coll = GetComponent<SphereCollider>();
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
        if (isfired)
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
                player.Hitted();
            }
        }
    }

    // show the predict line
    void ShowPredict()
    {
        hitPoint.SetActive(false);
        warnPoint.SetActive(false);
        RaycastHit[] hits;
        if (coll != null)
        {
            hits = Physics.SphereCastAll(transform.position, coll.radius, transform.forward);
        }
        else
        {
            hits = Physics.RaycastAll(transform.position, transform.forward);
        }
        if (hits.Length > 0)
        {
            RaycastHit p = hits.FirstOrDefault(h => h.collider.gameObject.GetComponent<PlayerControl>() != null);
            if (p.collider != null)
            {
                // hit player
                lineMat.color = hitColor;
                RaycastHit hit = hits.FirstOrDefault(h => h.collider.gameObject.tag == "UI");
                if (hit.collider != null)
                {
                    // hit camera area
                    hitPoint.SetActive(true);
                    hitPoint.transform.parent = hit.collider.gameObject.transform;

                    #region Test Code For Oculus CV1, Unity5.6. UI disappear even in the visible region of camera
                    RaycastHit[] tmpHits = Physics.RaycastAll(transform.position, transform.forward);
                    RaycastHit tmpHit = tmpHits.SingleOrDefault(t => t.collider.gameObject == hit.collider.gameObject);
                    if (tmpHit.collider != null)
                    {
                        hitPoint.transform.position = tmpHit.point;
                    }
                    else
                    {
                        hitPoint.transform.position = hit.point;
                    }
                    Vector3 tmp = hitPoint.transform.localPosition;
                    tmp.y = 0.02f;
                    hitPoint.transform.localPosition = tmp;

                    hitPoint.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                    #endregion
                }
                else
                {
                    // when player hitted but the place not in the camera area
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

    //Compute the position to UI according to the place the player hitted
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

    // check when to fire
    void CheckFire()
    {
        double leftTime = waitTime;
        if (!isfired)
        {
            if (Time.time - startTime > waitTime)
            {
                isfired = true;
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

                leftTime = Math.Round(waitTime - (Time.time - startTime), 1, MidpointRounding.AwayFromZero);
                timeText.text = leftTime.ToString();
            }
        }
    }

    private void OnDestroy()
    {
        DestroyObject(hitPoint);
    }
}
