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
    public float defaultJumpForce;

    public int amountOfDashes;
    public int maxDashes;
    public int amountOfJumps;
    public int maxJumps;

    public int berriesInInventory;
}
