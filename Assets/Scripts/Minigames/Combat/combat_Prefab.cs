using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewtonVR
{
    public class combat_Prefab : MonoBehaviour
    {
        private GameObject ShipSpawnhandler;
        private combat_Spawnhandler spawnscript;

        private bool isShot;
        private Animator anim;
        private MinigameCombat gamescript;
        private TextMesh instructionsText;

        private Button obj;

        private void Start()
        {
            spawnscript = GameObject.Find("ShipSpawnhandler").GetComponent<combat_Spawnhandler>();
            isShot = false;
            //anim = gameObject.GetComponent<Animator>();
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            //anim.Play("slime_idle");
            instructionsText = GameObject.Find("Screentext").GetComponent<TextMesh>();
            gamescript = GameObject.Find("MinigameCombat").GetComponent<MinigameCombat>();

            obj = gameObject.GetComponent<Button>();
            obj.onClick.AddListener(ObjectDestroy);
        }

        void ObjectDestroy()
        {
            if (!isShot)
            {
                instructionsText.text = "Destroyed: " + (spawnscript.amountSpawned);

                if (spawnscript.amountSpawned < 5)
                {
                    spawnscript.SpawnObject();
                }
                else
                {
                    instructionsText.text += "\nCombat Training completed!";
                    gamescript.EndGame();
                }
            }
            isShot = true;

            //anim.Play("slime_die");
            //yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}
