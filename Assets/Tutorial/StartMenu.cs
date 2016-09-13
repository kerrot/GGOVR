using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    [SerializeField]
    private VRButton start;
    [SerializeField]
    private GameObject tutorialMenu;

    void Start()
    {
        start.OnPress += TutorialMenu;
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
