using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
 private bool interactable;
    
    // Update is called once per frame
  
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            interactable = true;
            GameManager.instance.AddStar();
            SoundManager.instance.PlaySFX(SoundManager.instance._audioSource ,SoundManager.instance.coinAudio);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            interactable = false;
        }
    }
}

