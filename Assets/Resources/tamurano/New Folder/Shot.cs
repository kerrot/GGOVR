using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	public GameObject bullet;
	public Transform spawn;
	public float speed = 1000;

	// Update is called once per frame(毎フレーム呼ばれる)
	void Update () {
		if(Input.GetButton("Fire1")){
			Shoot();
		}
	}

	void Shoot () {
		GameObject obj = GameObject.Instantiate(bullet)as GameObject;
		obj.transform.position = spawn.position;
		Vector3 force;
		force = this.gameObject.transform.forward * speed;
	}
}