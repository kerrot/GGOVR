using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuContorl : MonoBehaviour {

	[SerializeField]
	private VRButton turtorial;
	[SerializeField]
	private VRButton LV1;
	[SerializeField]
	private VRButton LV2;
	[SerializeField]
	private VRButton LV3;

	void Start()
	{
		turtorial.OnPress += LoadScene;
		LV1.OnPress += LoadScene;
		LV2.OnPress += LoadScene;
		LV3.OnPress += LoadScene;

		turtorial.paramStr = "Tutorial";
		LV1.paramStr = "LV1";
		LV2.paramStr = "LV2";
		LV3.paramStr = "LV3";
	}

	void LoadScene(VRButton button)
	{
		SceneManager.LoadScene(button.paramStr);
	}

	void OnDestroy()
	{
		turtorial.OnPress -= LoadScene;
		LV1.OnPress -= LoadScene;
		LV2.OnPress -= LoadScene;
		LV3.OnPress -= LoadScene;
	}
}
