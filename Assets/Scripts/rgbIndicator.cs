using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class rgbIndicator : MonoBehaviour
    {
        public NVRSlider slider;
        public int colorIndex;
        private Color color;
        private Color emissionColor;
        private Renderer rend;

        void Start()
        {
            rend = gameObject.GetComponent<Renderer>();
            color = new Color(0, 0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
            color[colorIndex] = 1;
            color[3] = slider.CurrentValue * 0.2f + 0.8f;
            rend.material.color = color; //e.g. (1, 0, 0, 0.94f)
            emissionColor = new Color(0, 0, 0);
            emissionColor[colorIndex] = slider.CurrentValue * 6;
            rend.material.SetColor("_EmissionColor", emissionColor); //e.g. (6*x, 0, 0)
        }
    }
}
