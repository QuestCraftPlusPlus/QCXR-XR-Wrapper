using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.UI;

[ExcludeFromDocs]
public class DefaultImageCursor : MonoBehaviour
{
    public Image dot;

    public GameObject cursorGameObject => this.gameObject;

    public Transform cursorTransform => this.transform;

    public void UpdateEffect(CursorConfiguration configuration, float distance, Vector3 normal, GameObject hitGameObject)
    {
        float scaleParam = Mathf.Max(configuration.cursorMinScale, distance) * 0.1f;
        transform.localScale = new Vector3(scaleParam, scaleParam, transform.localScale.z);
        transform.localPosition = Vector3.forward * distance;
        transform.forward = normal;
        if (dot)
            dot.color = configuration.cursorDotColor;
    }

    public void Show(bool display)
    {
        if (gameObject) gameObject.SetActive(display);
    }
}
