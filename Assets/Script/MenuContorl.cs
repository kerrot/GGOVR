using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuContorl : MonoBehaviour {

	[SerializeField]
	private VRButton title;
	[SerializeField]
	private VRButton LV1;
	[SerializeField]
	private VRButton LV2;
	[SerializeField]
	private VRButton LV3;

    [SerializeField]
    private GameObject clear;
    [SerializeField]
    private GameObject gameover;
    [SerializeField]
    private GameObject aim;
	[SerializeField]
	private GameObject logo;

    void Start()
	{
		title.OnPress += LoadScene;
		LV1.OnPress += LoadScene;
		LV2.OnPress += LoadScene;
		LV3.OnPress += LoadScene;

		title.paramStr = "Tutorial";
		LV1.paramStr = "LV1";
		LV2.paramStr = "LV2";
		LV3.paramStr = "LV3";

		aim.SetActive(true);
	}

	void LoadScene(VRButton button)
	{
		SceneManager.LoadScene(button.paramStr);
        Time.timeScale = 1;
    }

	void OnDestroy()
	{
		title.OnPress -= LoadScene;
		LV1.OnPress -= LoadScene;
		LV2.OnPress -= LoadScene;
		LV3.OnPress -= LoadScene;
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
        title.gameObject.SetActive(false);
	}
}
