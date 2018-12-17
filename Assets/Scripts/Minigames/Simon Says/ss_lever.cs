using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class ss_lever : MonoBehaviour
    {

        private NVRLever lever;
        private Renderer rend;
        public Material off;
        public Material on;

        // Use this for initialization
        void Start()
        {
            lever = gameObject.GetComponent<NVRLever>();
            rend = lever.GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (lever.CurrentLeverPosition == NVRLever.LeverPosition.On)
            {
                rend.material = off;
            }
            else
            {
                rend.material = on;
            }
        }
    }
}
