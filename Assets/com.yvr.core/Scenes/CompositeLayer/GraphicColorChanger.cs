using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.UI;

[ExcludeFromDocs]
public class GraphicColorChanger : MonoBehaviour
{

    private Graphic currentGraphic = null;

    private void Start()
    {
        currentGraphic = GetComponent<Graphic>();
    }

    private void Update()
    {
        float sinVal = Mathf.Abs(Mathf.Sin(Time.time));
        currentGraphic.color = new Color(sinVal, sinVal, sinVal, 1);
    }
}
