using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int coins = 0;

    private bool isPaused;



    void Awake()
    
   
    {
         BGMManager.instance.PlayBGM(BGMManager.instance.BGMAudio);
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Pause()
    {
        if(!isPaused)
        {
            Time.timeScale = 0; 
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void AddCoin()
    {
        coins++;
        // voins += 1; las dos cosas hacen lo mismo
    
    }
   
}
