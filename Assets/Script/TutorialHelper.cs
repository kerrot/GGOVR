using UnityEngine;
using System.Collections;

// show the waring in Tutorial stage when player is going to be hitted
public class TutorialHelper : MonoBehaviour {
    [SerializeField]
    private GameObject hint;
    [SerializeField]
    private GameObject tutorialMenu;
    [SerializeField]
    private MenuContorl menu;

    private BulletControl current;

    private int count = 0;
    private int total = 0;

    void Start()
    {
        total = GameObject.FindObjectsOfType<SetupTarget>().Length;
    }

    void OnTriggerEnter(Collider other)
    {
        BulletControl b = other.GetComponent<BulletControl>();
        if (b != null)
        {
            if (b.IsHit)
            {
                current = b;
                Time.timeScale = 0;

                tutorialMenu.SetActive(true);
                hint.SetActive(true);
            }

            ++count;
        }
    }

    void Update()
    {
        if (current != null && !current.IsHit)
        {
            current = null;
            Time.timeScale = 1;

            tutorialMenu.SetActive(false);
            hint.SetActive(false);
        }
        else if (count == total)
        {
            ++count;
            StartCoroutine(TutorialClear());
        }
    }

    IEnumerator TutorialClear()
    {
        yield return new WaitForSeconds(3);
        menu.ShowMenu(true);
    }
}
