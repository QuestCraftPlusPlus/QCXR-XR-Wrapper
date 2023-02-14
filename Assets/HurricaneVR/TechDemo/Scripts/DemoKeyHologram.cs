using System.Collections.Generic;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoKeyHologram : MonoBehaviour
    {
        public float Interval = .2f;
        public float LastMove;
        public List<Vector3> Positions = new List<Vector3>();
        public int Index;

#if UNITY_EDITOR
        [InspectorButton("AddPositionMethod")] public string AddPosition;

        public void AddPositionMethod()
        {
            Positions.Add(transform.localPosition);
        }
#endif

        void Start()
        {

        }

        void Update()
        {
            if (Time.time > LastMove && Time.time - LastMove > Interval && Positions.Count > 0)
            {
                if (Index >= Positions.Count)
                    Index = 0;

                transform.localPosition = Positions[Index];
                Index++;
                LastMove = Time.time;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
