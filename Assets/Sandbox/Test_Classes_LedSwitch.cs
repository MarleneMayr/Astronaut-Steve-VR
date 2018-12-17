using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR.Example
{
    public class Test_Classes_LedSwitch : MonoBehaviour
    {
        public GameObject LedLightOne;
        public GameObject LedLightTwo;
        private NVRLever lever;
        private Led LedOne;
        private Led LedTwo;

        public class Led
        {
            public bool isActive;
            GameObject LedLight;
            public Renderer rend;

            public Led(bool newisActive, GameObject newLedLight)
            {
                isActive = newisActive;
                LedLight = newLedLight;
                rend = LedLight.GetComponent<Renderer>();
            }
        }

        // Use this for initialization
        void Start()
        {
            lever = gameObject.GetComponent<NVRLever>();
            LedOne = new Led(true, LedLightOne);
            LedOne = new Led(true, LedLightOne);
        }

        // Update is called once per frame
        void Update()
        {
            if (lever.LeverEngaged == true)
            {
                ChangeLedState(LedOne);
                ChangeLedState(LedTwo);
            }
        }

        void ChangeLedState(Led led)
        {
            led.isActive = !led.isActive;
            led.rend.material.color = (led.isActive) ? Color.yellow : Color.black;
        }
    }
}
