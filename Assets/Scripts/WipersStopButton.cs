using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class WipersStopButton : MonoBehaviour
    {
        public GameObject wipers;
        public GameObject button;


        private NVRButton nvrButton;
        private Animation anim;

        // Use this for initialization
        void Start()
        {
            nvrButton = button.GetComponent<NVRButton>();
            anim = wipers.GetComponent<Animation>();
        }

        private void Update()
        {
            if (nvrButton.ButtonDown)
            {
                anim.wrapMode = WrapMode.Once;
            }
        }
    }
}