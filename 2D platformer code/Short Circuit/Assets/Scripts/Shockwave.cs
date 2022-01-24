using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float freezeTime;
    private EnemyPatrol enemyWalk;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyWalk = other.GetComponent<EnemyPatrol>();
            freezeEnemy();
        }
    }
    public IEnumerator freezeEnemy()
    {
        enemyWalk.frozen = true;
        yield return new WaitForSeconds(freezeTime);
        enemyWalk.frozen = false;
    }
}

