using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpots : MonoBehaviour
{
    public List<Vector3> spots;

    public void AddSpot(Vector3 point)
    {
        spots.Add(point);
    }

    public void MoveSpot(int index, Vector3 newPos)
    {
        spots[index] = newPos;
    }
}
