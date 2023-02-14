using UnityEngine;

namespace HurricaneVR.Framework.Core.Utils
{
    [ExecuteInEditMode]
    public class AxisLines : MonoBehaviour
    {
        public bool DrawX = true;
        public bool DrawY = true;
        public bool DrawZ = true;

        public float Length = 1;
        void Start()
        {

        }

        void Update()
        {
            if (DrawZ)
                Debug.DrawLine(transform.position, transform.position + (transform.forward * Length), Color.blue);
            if (DrawX)
                Debug.DrawLine(transform.position, transform.position + (transform.right * Length), Color.red);
            if (DrawY)
                Debug.DrawLine(transform.position, transform.position + (transform.up * Length), Color.green);
        }
    }
}
