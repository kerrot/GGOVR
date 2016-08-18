using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private float bloodTime;
    [SerializeField]
    private float slowSpeed;
    [SerializeField]
    private float slowTime;

    private float startTime;
    private float slowStartTime;

    void Start()
    {
        
    }

	void Update ()
    {
	    if (Time.realtimeSinceStartup - startTime > bloodTime)
        {
            blood.SetActive(false);
        }

        if (Time.realtimeSinceStartup - slowStartTime > slowTime)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SlowMotion();
        }

        if (Input.GetKey(KeyCode.R))
        {
            InputTracking.Recenter();
        }
    }

    public void BloodEffect()
    {
        blood.SetActive(true);
        startTime = Time.realtimeSinceStartup;
    }

    public void SlowMotion()
    {
        Time.timeScale = slowSpeed;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        slowStartTime = Time.realtimeSinceStartup;
    }
}
