using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class DoorOpenButton : MonoBehaviour
    {
        public GameObject Led;
        public GameObject Door1;
        //public GameObject Door2;

        private Animator anim1;
        //private Animator anim2;
        private Renderer rend;
        private bool doorisopen;
        private float speed;

        private NVRButton Button;
        
        private void Start()
        {
            Button = gameObject.GetComponent<NVRButton>();

            doorisopen = false;

            rend = Led.GetComponent<Renderer>();
            rend.material.color = new Color(1, 1, 1, 0.8f);
            rend.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));

            anim1 = Door1.GetComponent<Animator>();
            //anim2 = Door2.GetComponent<Animator>();
        }

        private void Update()
        {
            if (Button.ButtonDown)
            {
                if(doorisopen)
                {
                    doorisopen = false;

                    rend.material.color = new Color(1, 1, 1, 0.8f);
                    rend.material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));

                    anim1.Play("Close");
                    //anim2.Play("Open");
                }
                else
                {
                    doorisopen = true;

                    rend.material.color = new Color(1, 1, 1, 1);
                    rend.material.SetColor("_EmissionColor", new Color(6, 6, 6));

                    anim1.Play("Open");
                    //anim2.Play("Close");
                }
            }
        }
    }
}