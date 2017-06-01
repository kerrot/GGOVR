using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    [SerializeField]
    private VRButton start;
    [SerializeField]
    private GameObject tutorialMenu;
	[SerializeField]
	private GameObject aim;

    void Start()
    {
        start.OnPress += TutorialMenu;
		aim.SetActive(true);
    }

    void TutorialMenu(VRButton button)
    {
        tutorialMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        start.OnPress -= TutorialMenu;
    }
}
