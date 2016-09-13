using UnityEngine;
using System.Collections;

public class TutorialHelper : MonoBehaviour {
    [SerializeField]
    private GameObject hint;
    [SerializeField]
    private GameObject tutorialMenu;

    private BulletControl current;

    private int count;
    private int total;

    void Start()
    {
        total = GameObject.FindObjectsOfType<SetupTarget>().Length;
    }

    void OnTriggerEnter(Collider other)
    {
        BulletControl b = other.GetComponent<BulletControl>();
        if (b != null && b.IsHit)
        {
            current = b;
            Time.timeScale = 0;

            tutorialMenu.SetActive(true);
            hint.SetActive(true);

            ++count;
            if (count == total)
            {
                TutorialClear();
            }
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
    }

    void TutorialClear()
    {

    }
}
