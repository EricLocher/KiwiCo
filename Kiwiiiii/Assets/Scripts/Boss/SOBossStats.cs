using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossStats", menuName = "Utilities/Stats/BossStats")]
public class SOBossStats : SOCharacterStats
{
    public bool shielded = false;
    public float phase1Health = 1f;
    public float phase2Health = 0.7f;
    public float phase3Health = 0.3f;
}
