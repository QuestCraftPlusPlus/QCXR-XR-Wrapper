using UnityEngine;

public class DesktopCam : MonoBehaviour
{
	public GameObject VrCam;
	public GameObject DesktopCamera;
	public Canvas[] Canvases;

#if UNITY_EDITOR
    void Awake()
    {
        VrCam.SetActive(false);
		DesktopCamera.SetActive(true);
		
		foreach (Canvas screencanvas in Canvases)
			screencanvas.worldCamera = DesktopCamera.GetComponent<Camera>();
    }
#endif
}
