using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class ss_slider : MonoBehaviour {

        public NVRSlider slider;
        public List<GameObject> list;
        private Renderer rend;
        public Material off;
        public Material on;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            int slidervalue;
            if (slider.CurrentValue == 1)
            {
                slidervalue = 5;
            }
            else
            {
                slidervalue = Mathf.FloorToInt(slider.CurrentValue * 5 + 1);
            }

            foreach (GameObject marker in list)
            {
                rend = marker.GetComponent<Renderer>();
                rend.material = off;
            }

            rend = list[slidervalue - 1].GetComponent<Renderer>();
            rend.material = on;
        }
    }
}
