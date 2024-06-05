using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UiRedocker : MonoBehaviour
{
    public GameObject ui;
    public GameObject mainScreen;
    public GameObject watchDisplay;
    public Button redockButton;


    private bool main = true;
    private void Start()
    {
        redockButton.onClick.AddListener(() =>
        {
            if (main)
                Transition(watchDisplay);   
            else
                Transition(mainScreen);                

            main = !main;
        });

        return;
        
        GameObject gender;
        
        Transition(gender);
    }
    
    void Transition(GameObject transitionObject)
    {
        ui.transform.parent = transitionObject.transform;
        ui.transform.localScale = new Vector3(-1, 1 ,-1);
        ui.transform.localPosition = Vector3.zero;
        ui.transform.localRotation = quaternion.Euler(Vector3.zero);
    }
    
}