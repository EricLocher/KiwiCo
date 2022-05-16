using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStats", menuName = "Utilities/Stats/BossStats")]
public class SOBossStats : SOCharacterStats
{
    public float shield = 100;
    public float maxHealth = 1000;
    public float maxShield = 100;
}
