using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRGhostHand : MonoBehaviour
    {
        [Tooltip("Distance to meet before showing the ghost hand.")]
        public float Threshold = .2f;

        public Transform GhostHand;
        public Transform ActualHand;
        public bool DisplayGhostHand;

        private void Start()
        {
            foreach (var c in GetComponentsInChildren<Collider>())
            {
                c.enabled = false;
            }

            if (GhostHand && ActualHand)
                StartCoroutine(CheckDistance());
        }

        private IEnumerator CheckDistance()
        {
            while (true)
            {
                if (DisplayGhostHand)
                {
                    if (Vector3.Distance(GhostHand.position, ActualHand.position) > Threshold)
                    {
                        GhostHand.gameObject.SetActive(true);
                    }
                    else
                    {
                        GhostHand.gameObject.SetActive(false);
                    }
                }

                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
