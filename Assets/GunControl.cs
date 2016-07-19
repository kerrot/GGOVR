using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private float spawnTime = 1f;

    private AudioSource au;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        au = GetComponent<AudioSource>();
        
    }

    void Spawn()
    {
        Instantiate(bullet, fire.transform.position, Quaternion.Euler(-90, 0, 0));
        au.Play();
    }
}
