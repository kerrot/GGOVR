using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SetupGunControl : MonoBehaviour {
    [SerializeField]
    private GameObject bullet;
	[SerializeField]
	private float offset;
	[SerializeField]
	private float cloneCount;
	[SerializeField]
	private GameObject setup;

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

		if (setup == null) 
		{
			setup = gameObject;
		}

        player = GameObject.FindObjectOfType<PlayerControl>();

		SetupTarget[] targets = setup.GetComponentsInChildren<SetupTarget>();
        foreach (SetupTarget t in targets)
        {
            SetupData d = new SetupData();
            d.isRelative = t.isRelative;
            d.shotTime = t.shotTime;
            d.waitTime = t.waitTime;
            d.speed = t.speed;
            d.position = (t.isRelative) ? t.transform.position - player.transform.position : t.transform.position;
            datas.Add(d);
            DestroyObject(t.gameObject);
        }
			
        startTime = Time.time;
		//Debug.Log (startTime);
    }

    void Update()
    {
        List<SetupData> tmp = datas.FindAll(d => d.shotTime + startTime < Time.time);
        tmp.ForEach(t =>
        {
				
			if (offset > 0 && cloneCount > 0)
			{
					//Debug.Log (t.position);
					float tmpOffset = 0;

					for (int i = 0; i <= cloneCount; ++i)
					{
						if ( i == 0)
						{
							GenerateBullet(t, 0);
						}
						else
						{
							GenerateBullet(t, tmpOffset);
							GenerateBullet(t, -tmpOffset);
						}
						tmpOffset += offset;
					}
			}
			else
			{
					GenerateBullet(t, 0);
			}
        });

        tmp.ForEach(t => datas.Remove(t));
    }

    void FireAudio()
    {
        au.Play();
    }

	void GenerateBullet(SetupData t, float offset)
	{
		GameObject obj = Instantiate(bullet, transform.position + new Vector3(0, offset, 0), Quaternion.identity) as GameObject;
		BulletControl b = obj.GetComponent<BulletControl>();
		b.WaitTime = t.waitTime;
		b.speed = t.speed;
		Vector3 tmpTarget = (t.isRelative) ? player.transform.position + t.position : t.position;
		b.SetTarget(tmpTarget + new Vector3(0, offset, 0));

		if (b != null)
		{
			b.OnFire = FireAudio;
		}
	}
}
