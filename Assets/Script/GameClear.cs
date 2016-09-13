using UnityEngine;
using System.Collections;

public class GameClear : MonoBehaviour {
    [SerializeField]
    private MenuContorl menu;

	public void Clear()
    {
        menu.ShowMenu(true);
    }
}
