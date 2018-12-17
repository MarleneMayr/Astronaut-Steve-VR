using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{


    public class PhysicsSoundButton : MonoBehaviour
    {
        private NVRButton button;

        public OVRInput.Controller controller;
        public AudioClip vibrationSource;
        private AudioSource soundSource;
        private AudioClip cachedSource;
        private OVRHapticsClip hapticsClip;
        private float hapticsClipLength;
        private float hapticsTimeout;

        private void Start()
        {
            button = gameObject.GetComponent<NVRButton>();
            soundSource = gameObject.GetComponent<AudioSource>();
        }

        private void Update()
        {

            if (button.ButtonDown)
            {
                if (vibrationSource == null)
                {
                    return;
                }

                if (vibrationSource != cachedSource)
                {
                    hapticsClip = new OVRHapticsClip(vibrationSource);
                    hapticsClipLength = vibrationSource.length;
                    cachedSource = vibrationSource;
                }

                if (Time.time < hapticsTimeout)
                    return;

                hapticsTimeout = Time.time + hapticsClipLength;

                if (controller == OVRInput.Controller.LTouch)
                {
                    OVRHaptics.LeftChannel.Preempt(hapticsClip);
                }
                else
                {
                    OVRHaptics.RightChannel.Preempt(hapticsClip);
                }

                soundSource.Play();

            }
        }

    }
}