using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{


    public class PhysicsSoundLever : MonoBehaviour
    {
        private NVRLever lever;
        public OVRInput.Controller controller;
        public AudioClip source;
        private AudioClip cachedSource;
        private OVRHapticsClip hapticsClip;
        private float hapticsClipLength;
        private float hapticsTimeout;
        private float temp;

        private void Start()
        {
            lever = gameObject.GetComponent<NVRLever>();
        }

        private void Update()
        {
            if (lever.IsAttached == true)
            {
                if (source == null)
                {
                    return;
                }

                if (source != cachedSource)
                {
                    hapticsClip = new OVRHapticsClip(source);
                    hapticsClipLength = source.length;
                    cachedSource = source;
                }

                if (Time.time < hapticsTimeout)
                    return;

                hapticsTimeout = Time.time + hapticsClipLength;

                if (controller == OVRInput.Controller.LTouch)
                    OVRHaptics.LeftChannel.Preempt(hapticsClip);
                else
                    OVRHaptics.RightChannel.Preempt(hapticsClip);


            }
        }

    }
}