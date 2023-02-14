using HurricaneVR.Framework.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoKeypad : MonoBehaviour
    {
        public UnityEvent Unlocked = new UnityEvent();

        public string Code;
        public TextMeshPro Display;
        public string Entry = "";
        public bool ForceUnlock;

        public int Index => Entry?.Length ?? 0;

        public int MaxLength => Code?.Length ?? 0;

        private bool _unlocked;

        protected virtual void Start()
        {
            var buttons = GetComponentsInChildren<DemoKeypadButton>();
            var colliders = GetComponentsInChildren<Collider>();

            foreach (var keyCollider in colliders)
            {
                foreach (var ourCollider in GetComponents<Collider>())
                {
                    Physics.IgnoreCollision(ourCollider, keyCollider);
                }
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                button.ButtonDown.AddListener(OnButtonDown);
                if (i >= 0 && i <= 9)
                {
                    button.Key = i.ToString()[0];
                }
                else if (i == 10)
                {
                    button.Key = '<';
                }
                else if (i == 11)
                {
                    button.Key = '+';
                }

                if (button.TextMeshPro)
                {
                    button.TextMeshPro.text = button.Key.ToString();
                }
            }

            Entry = "";
            if (Display)
            {
                Display.text = Entry.PadLeft(MaxLength, '*');
            }
        }

        public virtual void Update()
        {
            if (ForceUnlock)
            {
                ForceUnlock = false;
            
                Unlock();
            }
        }
    
        protected virtual void OnButtonDown(HVRPhysicsButton button)
        {
            var keyPadButton = button as DemoKeypadButton;

            if (keyPadButton.Key == '<')
            {
                if (Entry.Length > 0)
                {
                    Entry = Entry.Substring(0, Entry.Length - 1);
                }
                else
                {
                    return;
                }
            }
            else if (keyPadButton.Key == '+')
            {
                if (Code == Entry)
                {
                    Unlock();
                }
            }
            else if (Index >= 0 && Index < MaxLength)
            {
                Entry += keyPadButton.Key;
            }

            if (Display)
            {
                Display.text = Entry.PadLeft(MaxLength, '*');
            }
        }

        protected virtual void Unlock()
        {
            if (!_unlocked)
                Unlocked.Invoke();
            _unlocked = true;
            Debug.Log($"unlocked!");
        }
    }
}