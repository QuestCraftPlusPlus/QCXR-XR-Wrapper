using UnityEngine;

public class DesktopCam : MonoBehaviour
{
    public GameObject VrCam;
    public GameObject DesktopCamera;
    public Canvas[] Canvases;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            return;
        
        VrCam.SetActive(false);
        DesktopCamera.SetActive(true);
        
        foreach (Canvas screencanvas in Canvases)
            screencanvas.worldCamera = DesktopCamera.GetComponent<Camera>();
    }
}
