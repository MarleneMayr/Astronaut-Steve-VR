using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class AlienDieVibration : MonoBehaviour
    {

        public OVRInput.Controller controller;
        public AudioClip vibrationSource;
        private AudioSource soundSource;
        private AudioClip cachedSource;
        private OVRHapticsClip hapticsClip;
        private float hapticsClipLength;
        private float hapticsTimeout;

        private void Start()
        {
            soundSource = gameObject.GetComponent<AudioSource>();
        }

    private void OnCollisionEnter(Collision collision)
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

    }
    private void Update()
        {

        }

}