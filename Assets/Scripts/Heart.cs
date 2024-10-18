using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
private PlayerController playerScript;
    
    // Update is called once per frame

    
  [SerializeField] private int _health = 1;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            playerScript = collider.gameObject.GetComponent<PlayerController>();
            
            if(playerScript._currentHealth < playerScript._maxHealth)
            {
                 playerScript.Health(1);
            Destroy(gameObject);
            }
           
        }
    }

}

