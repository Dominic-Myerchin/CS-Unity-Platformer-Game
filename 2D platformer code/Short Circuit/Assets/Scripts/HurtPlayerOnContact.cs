using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HurtPlayerOnContact : MonoBehaviour
{
    public int damageToGive;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            HealthManager.HurtPlayer(damageToGive);
        }
    }
}
