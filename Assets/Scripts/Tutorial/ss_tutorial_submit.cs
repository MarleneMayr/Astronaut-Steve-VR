using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR.Example
{
    public class ss_tutorial_submit : MonoBehaviour
    {
        public GameObject submit_controller;

        private ss_tutorial_start instructionsScript;
        private NVRButton Button;

        private void Start()
        {
            Button = gameObject.GetComponent<NVRButton>();
            instructionsScript = submit_controller.GetComponent<ss_tutorial_start>();
        }


        private void Update()
        {
            if (instructionsScript.Ss_InstructionsEnabled && Button.ButtonDown)
            {
                instructionsScript.InstructionsCorrect();
            }
        }
    }
}
