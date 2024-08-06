using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObscuringItemFader : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeOutCoroutine()
        {
            float currentAlpha = spriteRenderer.color.a;
            float distance = currentAlpha * Settings.targetAlpha;

            while (currentAlpha - Settings.targetAlpha > 0.01f)
            {
                currentAlpha = currentAlpha - distance / Settings.fadeOutSeconds * Time.deltaTime;
                spriteRenderer.color = new Color(1, 1, 1, currentAlpha);
                yield return null; 
            }

            spriteRenderer.color = new Color(1, 1, 1, Settings.targetAlpha);
        }



        public void FadeIn()
        {
            StartCoroutine(FadeInCoroutine());
        }

        private IEnumerator FadeInCoroutine()
        {
            float currentAlpha = spriteRenderer.color.a;
            float distance = 1f - currentAlpha;
            while (1f-currentAlpha>0.01f)
            {
                currentAlpha = currentAlpha + distance / Settings.fadeInSeconds * Time.deltaTime;
                spriteRenderer.color = new Color(1, 1, 1, currentAlpha);
                yield return null;
            }
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }
}
