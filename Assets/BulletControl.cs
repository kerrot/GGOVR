using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletControl : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private TextMesh TimeText;

    Rigidbody body;
    float startTime;
    bool start = false;
    AudioSource sound;

	void Start ()
    {
        body = GetComponent<Rigidbody>();
        startTime = Time.time;
        sound = GetComponent<AudioSource>();
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
                TimeText.text = t.ToString();
            }
        }
    }

    void FixedUpdate()
    {
        if (start)
        {
            body.velocity = Vector3.back * speed;
        }
    }
}
