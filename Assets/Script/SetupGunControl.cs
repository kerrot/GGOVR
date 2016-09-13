using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SetupGunControl : MonoBehaviour {
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject playerBase;

    private AudioSource au;

    private PlayerControl player;

    public struct SetupData
    {
        public bool isRelative;
        public Vector3 position;
        public float shotTime;
        public float waitTime;
        public float speed;
    }

    private List<SetupData> datas = new List<SetupData>();

    private float startTime;

    void Start()
    {
        au = GetComponent<AudioSource>();
        player = GameObject.FindObjectOfType<PlayerControl>();

        SetupTarget[] targets = GameObject.FindObjectsOfType<SetupTarget>();
        foreach (SetupTarget t in targets)
        {
            SetupData d = new SetupData();
            d.isRelative = t.isRelative;
            d.shotTime = t.shotTime;
            d.waitTime = t.waitTime;
            d.speed = t.speed;
            d.position = (t.isRelative) ? t.transform.localPosition : t.transform.position;
            datas.Add(d);
            DestroyObject(t.gameObject);
        }

        startTime = Time.time;
    }

    void Update()
    {
        List<SetupData> tmp = datas.FindAll(d => d.shotTime + startTime < Time.time);
        tmp.ForEach(t =>
        {
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            BulletControl b = obj.GetComponent<BulletControl>();
            b.WaitTime = t.waitTime;
            b.speed = t.speed;
            b.SetTarget((t.isRelative) ? player.transform.position + t.position : t.position);
            if (b != null)
            {
                b.OnFire = FireAudio;
            }
        });

        tmp.ForEach(t => datas.Remove(t));
    }

    void FireAudio()
    {
        au.Play();
    }
}
