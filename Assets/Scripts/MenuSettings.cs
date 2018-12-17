using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewtonVR
{
    public class MenuSettings : MonoBehaviour
    {
        private NVRLever lever;
        public GameObject menu;
        private GraphicRaycaster ray;
        bool toggle = false;

        // Use this for initialization
        void Start()
        {
            lever = gameObject.GetComponent<NVRLever>();
            //ray = menu.GetComponent<GraphicRaycaster>();
            StartCoroutine(setActive()); 

        }

        IEnumerator setActive()
        {
            yield return new WaitForSeconds(0.01f);
            menu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

            if (menu.activeSelf == true)
            {
                toggle = true;
            } else
            {
                toggle = false;
            }

            if (lever.LeverEngaged == true && toggle == false)
            {
                menu.SetActive(true);
                //menu.AddComponent<GraphicRaycaster>();
                //ray.enabled = true;
            }
            else if (lever.LeverEngaged == true && toggle == true)
            {
                menu.SetActive(false);
                //Destroy(menu.GetComponent<GraphicRaycaster>());
                //ray.enabled = false;
            }

        }

    }
}