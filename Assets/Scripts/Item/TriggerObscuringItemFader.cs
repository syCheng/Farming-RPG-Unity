using UnityEngine;

public class TriggerObscuringItemFader : MonoBehaviour
{
    // When the player touches an object, we will fade out the objects with 'ObscuringItemFader'
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the gameobject we collided with, and then get all the obscuring item components on it and its children, and then trigger the fade out
        ObscuringItemFader[] obscuringItemFaders = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        for (int i = 0; i < obscuringItemFaders.Length; i++)
        {
            obscuringItemFaders[i].FadeOut();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Get the gameobject we collided with, and then get all of the obscuring item fader components and it and its children - and then trigger the fade in
        ObscuringItemFader[] obscuringItemFaders = collision.gameObject.GetComponentsInChildren<ObscuringItemFader>();

        for (int i = 0; i < obscuringItemFaders.Length; i++)
        {
            obscuringItemFaders[i].FadeIn();
        }
    }
}
