using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HurricaneVR.Framework.Core.Utils
{
    public class SFXPlayer : MonoBehaviour
    {
        public class PlayEvent
        {
            public float Time;
        }

        static SFXPlayer s_Instance;
        public static SFXPlayer Instance => s_Instance;
         static int id;
        public AudioSource SFXReferenceSource;
        public int SFXSourceCount;

        [Tooltip("Changes object name to the clip. This will create garbage.")]
        public bool SetClipName;

        Dictionary<Guid, PlayEvent> m_PlayEvents = new Dictionary<Guid, PlayEvent>();
        List<int> m_PlayingSources = new List<int>();

        AudioSource[] m_SFXSourcePool;

        int m_UsedSource = 0;
        private List<Guid> IDToRemove = new List<Guid>();

        void Awake()
        {
            if (s_Instance != null)
            {
                Destroy(this);
                return;
            }

            s_Instance = this;

            m_SFXSourcePool = new AudioSource[SFXSourceCount];

            for (int i = 0; i < SFXSourceCount; ++i)
            {
                m_SFXSourcePool[i] = Instantiate(SFXReferenceSource, transform);
                m_SFXSourcePool[i].gameObject.SetActive(false);
            }
        }

        void Update()
        {

            foreach (var playEvent in m_PlayEvents)
            {
                playEvent.Value.Time -= Time.deltaTime;

                if (playEvent.Value.Time <= 0.0f)
                    IDToRemove.Add(playEvent.Key);
            }

            foreach (var id in IDToRemove)
            {
                m_PlayEvents.Remove(id);
            }

            for (int i = 0; i < m_PlayingSources.Count; ++i)
            {
                int id = m_PlayingSources[i];
                
                if (!m_SFXSourcePool[id].isPlaying)
                {
                    m_SFXSourcePool[id].gameObject.SetActive(false);
                    m_PlayingSources.RemoveAt(i);
                    i--;
                }
            }

            IDToRemove.Clear();
        }

        public AudioSource GetNewSource()
        {
            return Instantiate(SFXReferenceSource);
        }

        public void PlaySFXRandomPitch(AudioClip clip, Vector3 position, float min, float max)
        {
            PlaySFX(clip, position, Random.Range(min, max), 1f);
        }

        public void PlaySFXCooldown(AudioClip clip, Vector3 position, Guid sourceId, float pitch = 1f, float volume = 1f, float cooldownTime = 0.5f)
        {
            if (clip == null)
                return;

            //can't play this sound again as the previous one with the same source was too early
            if (m_PlayEvents.ContainsKey(sourceId))
                return;

            PlaySFX(clip, position, pitch, volume);

            m_PlayEvents.Add(sourceId, new PlayEvent() { Time = cooldownTime });
        }

        public void PlaySFX(AudioClip clip, Vector3 position)
        {
            PlaySFX(clip, position, 1f, 1f);
        }

        public void PlaySFX(AudioClip clip, Vector3 position, float pitch, float volume)
        {
            if (clip == null)
                return;

            var audioSource = m_SFXSourcePool[m_UsedSource];

            if (!audioSource)
                return;

            m_PlayingSources.Add(m_UsedSource);

            m_UsedSource = m_UsedSource + 1;
            if (m_UsedSource >= m_SFXSourcePool.Length) m_UsedSource = 0;

            if (SetClipName && Application.isEditor)
            {
                audioSource.name = clip.name;
            }

            audioSource.gameObject.SetActive(true);
            audioSource.transform.position = position;
            audioSource.clip = clip;

            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }
}
