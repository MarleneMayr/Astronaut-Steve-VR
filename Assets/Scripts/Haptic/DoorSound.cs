using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{


    public class DoorSound : MonoBehaviour
    {
        public AudioClip open;
        public AudioClip close;
        public GameObject obj;
        private NVRButton button;
        private AudioSource openDoor;
        private AudioSource closeDoor;
        bool toggle = false;

        // Use this for initialization
        void Start()
        {
            button = gameObject.GetComponent<NVRButton>();
        }

        // Update is called once per frame
        void Update()
        {
            if (button.ButtonDown && toggle == false)
            {
                openDoor.Play();
                toggle = true;
            } else if (button.ButtonDown && toggle == true)
            {
                closeDoor.Play();
                toggle = false;
            }
        }

        private AudioSource AddClip(AudioClip clip, GameObject obj)
        {
            AudioSource newAudio = obj.AddComponent<AudioSource>();
            newAudio.clip = clip;
            newAudio.playOnAwake = false;
            newAudio.spatialBlend = 1;

            return newAudio;
        }

        private void Awake()
        {
            openDoor = AddClip(open, obj);
            closeDoor = AddClip(close, obj);
        }
    }
}