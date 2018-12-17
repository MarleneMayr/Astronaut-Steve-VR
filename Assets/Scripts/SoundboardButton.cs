using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class SoundboardButton : MonoBehaviour
    {

        public Soundboard board;
        public AudioClip Sound;
        
        private NVRButton Button;

        private void Start()
        {
            Button = gameObject.GetComponent<NVRButton>();
        }

        private void Update()
        {
            if (Button.ButtonDown)
            {
                board.PlaySound(Sound);
            }
        }
    }
}
