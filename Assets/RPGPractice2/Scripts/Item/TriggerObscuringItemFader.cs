using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    public class TriggerObscuringItemFader : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {

            ObscuringItemFader[] allFaders  = collision.transform.GetComponentsInChildren<ObscuringItemFader>();

            for (int i = 0; i < allFaders.Length; i++)
            {
                allFaders[i].FadeOut();
            }
          
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ObscuringItemFader[] allFaders = collision.transform.GetComponentsInChildren<ObscuringItemFader>();

            for (int i = 0; i < allFaders.Length; i++)
            {
                allFaders[i].FadeIn();
            }
        }
    }
}
