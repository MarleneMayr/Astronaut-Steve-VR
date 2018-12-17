using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class PhysicsSound : MonoBehaviour
    {
        //private NVRLever lever;
        //public bool useHaptics = false;
        //public bool useSound = true;

        public OVRInput.Controller controller;
        public AudioClip source;
        private AudioClip cachedSource;
        private OVRHapticsClip hapticsClip;
        private float hapticsClipLength;
        private float hapticsTimeout;
        private Vector3 temp;

        private void Start()
        {

                //lever = gameObject.GetComponent<NVRLever>();
                temp = gameObject.GetComponent<Transform>().position;
                //temp = transform.position;
                Debug.Log(temp);
            
        }

        private void Update()
        {
                Vector3 current = gameObject.GetComponentInParent<Transform>().position;


                if (temp != current)
                {
                    if (Time.time < hapticsTimeout)
                        return;

                    hapticsTimeout = Time.time + hapticsClipLength;

                    if (controller == OVRInput.Controller.LTouch)
                        OVRHaptics.LeftChannel.Preempt(hapticsClip);
                    else
                        OVRHaptics.RightChannel.Preempt(hapticsClip);

                }

        }

        /*
        void OnCollisionEnter(Collision c)
        {
            if (useHaptics)
            {
                Debug.Log("Use haptic funkt");
                PlayHaptics(c.collider);

            }

            if (useSound)
                PlaySound();


        }

        void PlayHaptics(Collider c)
        {
            AudioSource source = GetComponent<AudioSource>();
            if (source == null)
            {
                Debug.Log("IsNull");
                return;
            }

            if (source != cachedSource)
            {
                Debug.Log("sollte gehn");
                Debug.Log(c.name);
                hapticsClip = new OVRHapticsClip(source.clip);
                hapticsClipLength = source.clip.length;
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

        void PlaySound()
        {
            AudioSource source = GetComponent<AudioSource>();
            if (source && !source.isPlaying)
                source.PlayDelayed(0.1f);
        }
        */
    }
}