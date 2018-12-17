using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{ 
    public class bulb : MonoBehaviour {

        public NVRSlider RSlide;
        public NVRSlider GSlide;
        public NVRSlider BSlide;

        private Renderer rend;
        private Color color;
        private Color emissionColor;

        void Start ()
        {
            rend = gameObject.GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update ()
        {
            float slideraverage = (RSlide.CurrentValue + GSlide.CurrentValue + BSlide.CurrentValue) / 3;
            //color = new Color(RSlide.CurrentValue, GSlide.CurrentValue, BSlide.CurrentValue, slideraverage * 0.2f + 0.8f);
            color = new Color(1, 1, 1, slideraverage * 0.2f + 0.8f);
            rend.material.color = color;

            emissionColor = new Color(RSlide.CurrentValue, GSlide.CurrentValue, BSlide.CurrentValue);
            rend.material.SetColor("_EmissionColor", emissionColor);
        }
    }
}

