using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Utilities/Stats/PlayerStats")]
public class SOPlayerStats : ScriptableObject
{
    public float health;

    public float moveSpeed;
    public float dashSpeed;
    public float jumpForce;

    public int amountOfJumps;
}
