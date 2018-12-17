using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSoundFlip : MonoBehaviour {

    public OVRInput.Controller controller;
    public AudioClip source;
    public AudioClip On;
    public AudioClip Off;
    private AudioSource offSource;
    private AudioSource onSource;
    private AudioClip cachedSource;
    private OVRHapticsClip hapticsClip;
    private float hapticsClipLength;
    private float hapticsTimeout;
    private float currentMax;
    private float currentMin;
    HingeJoint hinge;
    JointLimits limits;
    // Use this for initialization

    void Start () {
        hinge = GetComponent<HingeJoint>();
        limits = hinge.limits;

        currentMax = transform.rotation.x;

    }

    private AudioSource AddClip(AudioClip clip)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.playOnAwake = false;
        newAudio.spatialBlend = 1;
        newAudio.minDistance = 0.2f;

        return newAudio;
    }

    private void Awake()
    {
        onSource = AddClip(On);
        offSource = AddClip(Off);
    }

    private void OnTriggerEnter(Collider other)
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

        if (other.gameObject.name == "CollOn")
        {
            onSource.Play();
        }
        else if (other.gameObject.name == "CollOff")
        {
            offSource.Play();
        }

    }


}
