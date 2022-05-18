using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaseStat", menuName = "Utilities/Boss/PhaseStat")]
public class SOPhaseStats : ScriptableObject
{
    public List<BossAttack> attackList;
}
