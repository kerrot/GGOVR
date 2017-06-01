using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

// show menu to change scene
public class MenuContorl : MonoBehaviour {

    [System.Serializable]
    public struct LVData
    {
        public VRButton btn;
        public string levelName;
    }

    [SerializeField]
    private List<LVData> levels = new List<LVData>();

    [SerializeField]
    private GameObject clear;
    [SerializeField]
    private GameObject gameover;
    [SerializeField]
    private GameObject aim;
	[SerializeField]
	private GameObject logo;
    [SerializeField]
    private GameObject title;

    float oriTimeSpeed;

    private void Awake()
    {
        oriTimeSpeed = Time.timeScale;
    }

    void Start()
	{
        levels.ForEach(l =>
        {
            l.btn.OnPress += LoadScene;
            l.btn.paramStr = l.levelName;
        });

		aim.SetActive(true);
	}

	void LoadScene(VRButton button)
	{
		SceneManager.LoadScene(button.paramStr);
        Time.timeScale = oriTimeSpeed;
    }

	void OnDestroy()
	{
        levels.ForEach(l => l.btn.OnPress -= LoadScene);
    }

    public void ShowMenu(bool win)
    {
        gameObject.SetActive(true);
        clear.SetActive(win);
        gameover.SetActive(!win);
        aim.SetActive(true);
        if (logo)
        {
            logo.SetActive(false);
        }
    }

	public void TitleMenu()
	{
		gameObject.SetActive(true);
		clear.SetActive(false);
		gameover.SetActive(false);
		aim.SetActive(true);
        if (logo)
        {
            logo.SetActive(true);
        }
        title.SetActive(false);
	}
}
