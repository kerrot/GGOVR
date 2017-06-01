using UnityEngine;
using System.Collections;

// TutorialMenu setup
public class TutorialMenu : MonoBehaviour {
    [SerializeField]
    private VRButton start;
	[SerializeField]
	private VRButton menuButton;
	[SerializeField]
	private MenuContorl menu;
    [SerializeField]
    private GameObject game;
    [SerializeField]
    private GameObject aim;

    void Start()
    {
        start.OnPress += StartGame;
		menuButton.OnPress += Menu;
		aim.SetActive(true);
    }

    void StartGame(VRButton button)
    {
        game.SetActive(true);
        gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        aim.SetActive(false);
    }

	void Menu(VRButton button)
	{
		menu.TitleMenu();
		gameObject.SetActive(false);
	}

    void OnDestroy()
    {
        start.OnPress -= StartGame;
		menuButton.OnPress -= Menu;
    }
}
