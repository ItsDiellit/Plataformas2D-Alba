using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int coins = 0;
    private int stars = 0;
    [SerializeField] Text _starText;

    [SerializeField] Text _coinText;
    private bool isPaused;

    [SerializeField] GameObject _pauseCanvas;



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
            _pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            _pauseCanvas.SetActive(false);
        }
    }

    public void AddCoin()
    {
        coins++;
        _coinText.text = coins.ToString();
        // voins += 1; las dos cosas hacen lo mismo
    
    }

    public void AddStar()
    {
        stars++;
        _starText.text = stars.ToString();
        // voins += 1; las dos cosas hacen lo mismo
    
    }
   
}
