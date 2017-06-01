using UnityEngine;
using System.Collections;

//button in VR. use gaze to click. check the sacle of ready object to determine
public class VRButton : MonoBehaviour {
    
	public string paramStr;
    
    [SerializeField]
    private int time;

    private GameObject observer;
    private int layerMask;
	private Transform ready;

    public delegate void ButtonEvent(VRButton button);
    public ButtonEvent OnPress;

    void Start()
    {
        Camera ca = Camera.main;
        if (ca == null)
        {
            ca = GameObject.FindObjectOfType<Camera>();
        }

        observer = ca.gameObject;
        layerMask = LayerMask.GetMask("UI");
		ready = transform.FindChild("Ready");
    }

    void Update()
    {
        // if time not set, always be 1 second.
        float step = (time > 0) ? 1f / time : 1f;
        RaycastHit hit;
		Debug.DrawRay(observer.transform.position, observer.transform.forward);
        if (Physics.Raycast(observer.transform.position, observer.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Vector3 tmp = ready.transform.localScale;
                tmp.x += step;
                ready.transform.localScale = tmp;

                //check x scale
                if (tmp.x >= 1)
                {
                    tmp.x = 0;
                    ready.transform.localScale = tmp;
                    if (OnPress != null)
                    {
                        OnPress(this);
                    }
                }

				return;
            }
        }

		Vector3 reset = ready.transform.localScale;
        reset.x = 0;
        ready.transform.localScale = reset;
    }
}
