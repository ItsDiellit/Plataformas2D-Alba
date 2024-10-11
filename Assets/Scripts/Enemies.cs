using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private AudioSource _audioSource;

     [SerializeField]private int EnemyhealthPoints = 3;
    // Start is called before the first frame update

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance.EnemyAudio);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyTakeDamage()
    {
        EnemyhealthPoints--;
       
        if(EnemyhealthPoints <= 0)
        {
            EnemyDie();
        }
        else
        {

        }
    }

    void EnemyDie()
        {
            SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance.EnemyAudio);
            Destroy(gameObject, 0.45f);

        }
}
