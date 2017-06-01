using UnityEngine;
using System.Collections;

// when clear, show menu
public class GameClear : MonoBehaviour {
    [SerializeField]
    private MenuContorl menu;

    //call the animation event
	public void Clear()
    {
        menu.ShowMenu(true);
    }
}
