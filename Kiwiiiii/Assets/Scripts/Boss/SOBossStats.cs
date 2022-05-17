using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStats", menuName = "Utilities/Stats/BossStats")]
public class SOBossStats : SOCharacterStats
{
    public bool shielded = false;
    public float maxHealth = 1000;
}
