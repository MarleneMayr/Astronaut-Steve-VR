using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class ss_submit : MonoBehaviour
    {        
        public GameObject submit_controller;

        private ss_instructions instructionsScript;
        private NVRButton Button;

        private void Start()
        {
            Button = gameObject.GetComponent<NVRButton>();
            instructionsScript = submit_controller.GetComponent<ss_instructions>();
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
