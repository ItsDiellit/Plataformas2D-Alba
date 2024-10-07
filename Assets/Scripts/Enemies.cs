using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

     [SerializeField]private int EnemyhealthPoints = 3;
    // Start is called before the first frame update
    void Start()
    {

        
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
            SoundManager.instance.PlaySFX(SoundManager.instance.EnemyAudio);
            Destroy(gameObject, 0.45f);

        }
}
