using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private SOEnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SOEnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
