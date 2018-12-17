using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class Timer : MonoBehaviour
    {
        public GameObject bar;
        public GameObject text;
        private TextMesh textcontent;
        private int max;
        private int currentvalue;
        Vector3 initpos;

        private void Awake()
        {
            textcontent = text.GetComponent<TextMesh>();
            
            initpos = bar.transform.position;
        }

        public void decrease()
        {
            if (currentvalue > 0)
            {
                currentvalue--;
                updateView();
            }
        }

        public void enable()
        {
            gameObject.SetActive(true);
        }

        public void disable()
        {
            gameObject.SetActive(false);
        }

        public void setMax(int value)
        {
            max = value;
            currentvalue = value;
            updateView();
        }

        private void updateView()
        {
            textcontent.text = ("" + currentvalue);

            float height = 0.2f + (100 * currentvalue / max) * 1.2f / 100;
            bar.GetComponent<SpriteRenderer>().size = new Vector2(0.22f, height);

            float posY = (100 * currentvalue / max) * 0.6f / 100;
            Vector3 pos = initpos;
            pos[1] = pos[1] - 0.6f + posY;
            bar.transform.position = pos;
        }
    }
}
