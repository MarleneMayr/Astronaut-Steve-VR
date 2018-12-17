using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewtonVR
{

    public class KnobTutSound : MonoBehaviour
    {
        
        public NVRLever ControlLever;
        public NVRInteractableItem ControlKnob;
        private int currentKnobState;
        public GameObject[] Indicators;
        private Renderer[] rendIndicators;
        [HideInInspector] public bool isFinish;

        private AudioSource source;
        private AudioSource leverSource;

        public Material off;
        public Material on;

        // Use this for initialization
        void Start()
        {
            rendIndicators = new Renderer[Indicators.Length];
            for (int i = 0; i < Indicators.Length; i++)
            {
                rendIndicators[i] = Indicators[i].GetComponent<Renderer>();
            }
            
            isFinish = false;

            source = ControlKnob.GetComponent<AudioSource>();
            leverSource = ControlLever.GetComponent<AudioSource>();

            currentKnobState = 1;

        }

        // Update is called once per frame
        void Update()
        {
            UpdateKnobState();

            if (ControlLever.LeverEngaged == true )
            {
                leverSource.Play();

            }
        }

        private void UpdateKnobState()
        {
            int previousKnobState = currentKnobState;

            int rotation = ((int)ControlKnob.transform.localEulerAngles.y);

            foreach (Renderer renderer in rendIndicators)
            {
                renderer.material = off;
            }

            if ((rotation >= 317) && (rotation < 358))
            {
                currentKnobState = 0;

            }
            else if ((rotation > 36) && (rotation <= 76))
            {
                currentKnobState = 2;
            }
            else
            {
                currentKnobState = 1;
            }

            rendIndicators[currentKnobState].material = on;

            if (currentKnobState != previousKnobState)
            {
                source.Play();
            }

        }


    }
}