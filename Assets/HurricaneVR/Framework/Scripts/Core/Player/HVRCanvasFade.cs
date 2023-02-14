using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{
    public class HVRCanvasFade : HVRScreenFade
    {
        public CanvasGroup CanvasGroup;

        public override float CurrentFade => CanvasGroup.alpha;

        protected override void Disable()
        {
            if (CanvasGroup)
                CanvasGroup.gameObject.SetActive(false);
        }

        protected override void Enable()
        {
            if (CanvasGroup)
                CanvasGroup.gameObject.SetActive(true);
        }

        public override void UpdateFade(float alpha)
        {
            if(CanvasGroup)
                CanvasGroup.alpha = alpha;
        }
    }
}