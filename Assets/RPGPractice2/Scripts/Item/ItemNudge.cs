using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmingRPG2
{
    public class ItemNudge : MonoBehaviour
    {
        private WaitForSeconds pause;
        private bool isAnimating = false;

        private void Awake()
        {
            pause = new WaitForSeconds(0.04f);
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isAnimating)
            {
                if (gameObject.transform.position.x < collision.transform.position.x)
                {
                    StartCoroutine(RotateAntiClockwise());
                }
                else
                {
                    StartCoroutine(RotateClockwise());
                }
            }

        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isAnimating)
            {
                if (gameObject.transform.position.x > collision.transform.position.x)
                {
                    StartCoroutine(RotateAntiClockwise());
                }
                else
                {
                    StartCoroutine(RotateClockwise());
                }
            }

        }



        //顺时针方向抖动  //
        private IEnumerator RotateClockwise()
        {
            isAnimating = true;

            for (int i = 0; i < 4; i++)
            {
                gameObject.transform.GetChild(0).Rotate(0, 0, -2);
                yield return pause;
            }


            for (int i = 0; i < 5; i++)
            {
                gameObject.transform.GetChild(0).Rotate(0, 0, 2);
            }

            gameObject.transform.GetChild(0).Rotate(0, 0, -2f);
            yield return pause;

            isAnimating = false;
        }



        private IEnumerator RotateAntiClockwise()
        {
            isAnimating = true;

            for (int i = 0; i < 4; i++)
            {
                gameObject.transform.GetChild(0).Rotate(0, 0, 2);
                yield return pause;
            }


            for (int i = 0; i < 5; i++)
            {
                gameObject.transform.GetChild(0).Rotate(0, 0, -2);
            }

            gameObject.transform.GetChild(0).Rotate(0, 0, 2f);
            yield return pause;

            isAnimating = false;
        }





    }
}
