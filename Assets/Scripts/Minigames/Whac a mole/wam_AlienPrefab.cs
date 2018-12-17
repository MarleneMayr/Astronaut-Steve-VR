using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class wam_AlienPrefab : MonoBehaviour
    {
        public AudioClip clip;
        private GameObject Spawnhandler;
        private wam_Spawnhandler spawnscript;
        private bool iscollided;
        private Animator anim;
        private MinigameWhacAMole gamescript;
        private TextMesh instructionsText;
        private AudioSource audio;
        private float starttime;

        public OVRInput.Controller controller;
        public AudioClip vibrationSource;
        private AudioSource soundSource;
        private AudioClip cachedSource;
        private OVRHapticsClip hapticsClip;
        private float hapticsClipLength;
        private float hapticsTimeout;


        private void Start()
        {
            spawnscript = GameObject.Find("AlienSpawnhandler").GetComponent<wam_Spawnhandler>(); ;
            iscollided = false;
            anim = gameObject.GetComponent<Animator>();
            gameObject.transform.localScale = new Vector3(8, 8, 8);
            anim.Play("slime_idle");
            instructionsText = GameObject.Find("Screentext").GetComponent<TextMesh>();
            gamescript = GameObject.Find("MinigameWhacAMole").GetComponent<MinigameWhacAMole>();
            audio = gameObject.GetComponent<AudioSource>();
            audio.minDistance = 0.2f;
        }

        private void Awake()
        {
            starttime = Time.time;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!iscollided)
            {
                StartCoroutine(ObjectDestroy());
                spawnscript.amountKilled++;
                instructionsText.text = "Smashed: " + (spawnscript.amountKilled);
            }
            iscollided = true;
        }

        private void Update()
        {
            float timeelapsed = Time.time - starttime;
            if (timeelapsed >= 3.0f)
            {
                audio.Stop();
                //add funny sound
                Destroy(gameObject);
            }
        }

        IEnumerator ObjectDestroy()
        {
            anim.Play("slime_die_2");

            audio.Stop();
            audio.clip = clip;
            audio.Play();
            audio.loop = false;

            /*
            if (vibrationSource != cachedSource)
            {
                hapticsClip = new OVRHapticsClip(vibrationSource);
                hapticsClipLength = vibrationSource.length;
                cachedSource = vibrationSource;
            }

            hapticsTimeout = Time.time + hapticsClipLength;

            if (controller == OVRInput.Controller.LTouch)
            {
                OVRHaptics.LeftChannel.Preempt(hapticsClip);
            }
            else
            {
                OVRHaptics.RightChannel.Preempt(hapticsClip);
            }
            */

            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject);
        }
    }
}
