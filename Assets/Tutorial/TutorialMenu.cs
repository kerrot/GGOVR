using UnityEngine;
using System.Collections;

public class TutorialMenu : MonoBehaviour {
    [SerializeField]
    private VRButton start;
    [SerializeField]
    private GameObject game;
    [SerializeField]
    private GameObject aim;

    void Start()
    {
        start.OnPress += StartGame;
    }

    void StartGame(VRButton button)
    {
        game.SetActive(true);
        gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        aim.SetActive(false);
    }

    void OnDestroy()
    {
        start.OnPress -= StartGame;
    }
}
