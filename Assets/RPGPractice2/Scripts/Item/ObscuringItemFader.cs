using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObscuringItemFader : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutRoutine());
        }

        private IEnumerator FadeOutRoutine()
        {
            float currentAlpha = spriteRenderer.color.a;
            float distance = currentAlpha - Settings.targetAlpha;

            while (currentAlpha-Settings.targetAlpha > 0.01f)
            {
                currentAlpha = currentAlpha - distance / Settings.fadeOutSecond * Time.deltaTime;
                spriteRenderer.color = new Color(1, 1, 1, currentAlpha);
                yield return null;
            }
            spriteRenderer.color = new Color(1, 1, 1, Settings.targetAlpha);
        }


        public void FadeIn()
        {
            StartCoroutine(FadeInRoutine());
        }

        private IEnumerator FadeInRoutine()
        {
            float currentAlpha = spriteRenderer.color.a;
            float distance = 1 - currentAlpha;
            while (1-currentAlpha>0.01f)
            {
                currentAlpha = currentAlpha + distance / Settings.fadeInSeconds * Time.deltaTime;
                spriteRenderer.color = new Color(1,1,1,currentAlpha);
                yield return null;
            }
            spriteRenderer.color = new Color(1,1,1,1);
        }
    }
}
