using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class LedSwitch : MonoBehaviour
    {
        public GameObject LedOne;
        public GameObject LedTwo;
        private NVRLever lever;
        private Renderer rendOne;
        private Renderer rendTwo;

        // Use this for initialization
        void Start()
        {
            lever = gameObject.GetComponent<NVRLever>();
            rendOne = LedOne.GetComponent<Renderer>();
            rendTwo = LedTwo.GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (lever.CurrentLeverPosition == NVRLever.LeverPosition.On)
            {
                rendOne.material.color = new Color(1, 1, 1, 1);
                rendOne.material.SetColor("_EmissionColor", new Color(6, 6, 6));

                rendTwo.material.color = new Color(1, 1, 1, 0.8f);
                rendTwo.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));
            }
            else
            {
                rendOne.material.color = new Color(1, 1, 1, 0.8f);
                rendOne.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));

                rendTwo.material.color = new Color(1, 1, 1, 1);
                rendTwo.material.SetColor("_EmissionColor", new Color(6, 6, 6));
            }
        }
    }
}
