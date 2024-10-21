using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int coins = 0;
    private int stars = 0;
    [SerializeField] Text _starText;

    [SerializeField] Text _coinText;
    private bool isPaused;

    [SerializeField] GameObject _pauseCanvas;

    private Animator _pausePanelAnimator;

    private bool pauseAnimation;

    [SerializeField] GameObject _victoryCanvas;


    [SerializeField] private Slider _healthBar;

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

        _pausePanelAnimator = _pauseCanvas.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(LoadAsync("Main Menu"));
        }
    }
    public void Pause()
    {
        if(!isPaused)
        {
             isPaused = true;
            Time.timeScale = 0; 
           
            _pauseCanvas.SetActive(true);
        }
        else if(isPaused && !pauseAnimation)
        {
            pauseAnimation = true;
            StartCoroutine(ClosePauseAnimation());
        }
    }
    IEnumerator ClosePauseAnimation()
    {
        _pausePanelAnimator.SetBool("Close", true);

        yield return new WaitForSecondsRealtime(0.30f);
        Time.timeScale = 1;
        _pauseCanvas.SetActive(false);
       
        isPaused = false;

        pauseAnimation = false;
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

        if(stars == 5)
        {
            _victoryCanvas.SetActive(true);
        }    
    }

    public void SetHealthBar(int _maxHealth)
    {
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;
    }

    public void UpdateHealthBar(int health)
    {
        _healthBar.value = health;
    }
   
   public void SceneLoader(string sceneName)
   {
    SceneManager.LoadScene(sceneName);
   }

    float progresoDeCarga;

   IEnumerator LoadAsync(string sceneName)
   {
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

    while (!asyncLoad.isDone)
    {
        if(asyncLoad.progress <= 0.9f)
        {
            progresoDeCarga = asyncLoad.progress;
        }
        yield return null;
    }
   }
}
