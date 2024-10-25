using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoidDeath : MonoBehaviour
{
   
    // Update is called once per frame
    
  
  
    

    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Player")
         {
            BGMManager.instance.PlayBGM(BGMManager.instance.GameOver);
            SceneManager.LoadScene("Game Over");
         }
    }
}
