﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private float bloodTime;

    private float startTime;

	void Update ()
    {
	    if (Time.realtimeSinceStartup - startTime > bloodTime)
        {
            blood.SetActive(false);
        }
	}

    public void BloodEffect()
    {
        blood.SetActive(true);
        startTime = Time.realtimeSinceStartup;
    }
}
