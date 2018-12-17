using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class MusicSettings : MonoBehaviour
    {
        public NVRButton Button;
        public NVRSlider slider;
        public GameObject audiosourceObject;

        private AudioSource audiosource;
        private bool isPlaying = false;

        void Start()
        {
            audiosource = audiosourceObject.GetComponent<AudioSource>();
            audiosource.Play();
            audiosource.Pause();

            //audiosource.playOnAwake = false;
            //audiosource.loop = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Button.ButtonDown)
            {
                if (isPlaying)
                {
                    Debug.Log("Pause MUSIC");
                    audiosource.Pause();
                    isPlaying = false;
                }
                else
                {
                    Debug.Log("UnPause MUSIC");
                    audiosource.UnPause();
                    isPlaying = true;
                }
            }
            audiosource.volume = slider.CurrentValue;
        }
    }
}