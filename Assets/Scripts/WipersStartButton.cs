using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NewtonVR
{
    public class WipersStartButton : MonoBehaviour
    {
        public GameObject indicator;
        public GameObject wipers;
        public GameObject button;

        private NVRButton nvrButton;
        private Animation anim;
        private float speed;
        private int currentvalue;

        // Use this for initialization
        void Start()
        {
            nvrButton = button.GetComponent<NVRButton>();
            anim = wipers.GetComponent<Animation>();
            speed = 1f;
        }

        private void Update()
        {
            //-88 bis 91
            //currentvalue = indicator.transform.rotation.y;
            currentvalue = ((int)indicator.transform.localEulerAngles.y);
            //Debug.Log(currentvalue);

            if (currentvalue < 270)
            {
                speed = 1f - 0.8f * (currentvalue / 100);
            }
            else
            {
                speed = 1f + 0.8f * ((360 - currentvalue) / 100); //Range 0.1 to 1.9
            }

            anim["Wipe"].speed = speed;

            if (nvrButton.ButtonDown)
            {
                anim.wrapMode = WrapMode.Loop;
                anim.Play();
            }
        }
    }
}
