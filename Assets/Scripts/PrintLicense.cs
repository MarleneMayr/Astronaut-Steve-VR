using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class PrintLicense : MonoBehaviour
    {
        public GameObject License;
        public GameObject Light;
        public Material win;
        public Material idle;

        private Animation anim;
        private Renderer rend;
        private NVRButton Button;

        public GameObject minigameManager;
        private MinigameManager gamescript;

        private bool printed;
        private bool blinking;

        private void Start()
        {
            printed = false;
            Button = gameObject.GetComponent<NVRButton>();
            rend = gameObject.GetComponent<Renderer>();
            anim = License.GetComponent<Animation>();
            gamescript = minigameManager.GetComponent<MinigameManager>();

            rend.material.color = Color.black;
            blinking = false;
        }

        private void Update()
        {
            if (gamescript.isFinish)
            {
                
                if (!blinking)
                {
                    blinking = true;
                    StartCoroutine("blinkButton");
                    Light.SetActive(true);
                }
                

                if (Button.ButtonDown)
                {
                    if (!printed)
                    {
                        printed = true;
                        anim.Play();
                        StopCoroutine("blinkButton");
                        rend.material = idle;
                    }
                }
            }
        }

        private IEnumerator blinkButton( )
        {
            while (printed == false)
            {
                yield return new WaitForSeconds(1f);
                rend.material = win;

                yield return new WaitForSeconds(1f);
                rend.material = idle;
            }
        }
    }
}