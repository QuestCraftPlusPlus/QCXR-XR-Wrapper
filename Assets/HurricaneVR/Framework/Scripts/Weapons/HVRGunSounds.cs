using System.Collections.Generic;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HurricaneVR.Framework.Weapons
{
    public class HVRGunSounds : MonoBehaviour
    {
        public AudioClip Fired;
        public AudioClip OutOfAmmo;

        //public AudioClip AmmoInserted;
        //public AudioClip AmmoReleased;

        public AudioClip SlideBack;
        public AudioClip SlideForward;

        public List<AudioClip> FiredVariations = new List<AudioClip>();
        //public List<AudioClip> AmmoInsertedVariations = new List<AudioClip>();
        //public List<AudioClip> AmmoReleasedVariations = new List<AudioClip>();

        public List<AudioClip> SlideBackVariations = new List<AudioClip>();
        public List<AudioClip> SlideForwardVariations = new List<AudioClip>();

        private void Start()
        {
            AddVariation(Fired, FiredVariations);
            //AddVariation(AmmoInserted, AmmoInsertedVariations);
            //AddVariation(AmmoReleased, AmmoReleasedVariations);
            AddVariation(SlideBack, SlideBackVariations);
            AddVariation(SlideForward, SlideForwardVariations);
        }

        public virtual void PlayGunFire()
        {
            PlayVariation(FiredVariations);
        }

        //public virtual void PlayAmmoInserted()
        //{
        //    PlayVariation(AmmoInsertedVariations);
        //}

        //public virtual void PlayAmmoReleased()
        //{
        //    PlayVariation(AmmoReleasedVariations);
        //}

        public virtual void PlayOutOfAmmo()
        {
            PlayClip(OutOfAmmo);
        }

        public virtual void PlaySlideForward()
        {
            PlayVariation(SlideForwardVariations);
        }

        public virtual void PlaySlideBack()
        {
            PlayVariation(SlideBackVariations);
        }

        protected virtual void PlayVariation(List<AudioClip> variations)
        {
            if (variations.Count == 0)
                return;

            var index = Random.Range(0, variations.Count);
            var sfx = variations[index];

            PlayClip(sfx);
        }

        protected virtual void PlayClip(AudioClip clip)
        {
            if (!clip)
                return;

            if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(clip, transform.position);
        }

        private void AddVariation(AudioClip clip, List<AudioClip> variations)
        {
            if (clip && !variations.Contains(clip))
            {
                variations.Add(clip);
            }
        }
    }
}
