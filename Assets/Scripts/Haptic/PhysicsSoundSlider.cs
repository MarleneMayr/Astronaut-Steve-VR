using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{


    public class PhysicsSoundSlider : MonoBehaviour
    {

        private NVRSlider slider;
        public OVRInput.Controller controller;
        public AudioClip source;
        private AudioClip cachedSource;
        private OVRHapticsClip hapticsClip;
        private float hapticsClipLength;
        private float hapticsTimeout;
        private float last;
        private float current;

        private void Start()
        {
            slider = gameObject.GetComponent<NVRSlider>();
        }

        private void Awake()
        {
            last = transform.position.z;
        }
        private void Update()
        {
            current = transform.position.z;

            if (last != current)
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
                {
                    OVRHaptics.LeftChannel.Preempt(hapticsClip);
                }
                else
                {
                    OVRHaptics.RightChannel.Preempt(hapticsClip);
                }

                last = current;


            }
        }
    }
}