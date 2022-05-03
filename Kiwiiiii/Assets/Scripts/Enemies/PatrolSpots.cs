using System.Collections.Generic;
using UnityEngine;

public class PatrolSpots : MonoBehaviour
{
    [HideInInspector]
    public List<Vector3> spots = new List<Vector3>();
    [HideInInspector]
    public bool hasSpots { get { return (spots.Count > 0); } }
    public Vector3 this[int i] { get { return spots[i]; } }

    [HideInInspector]
    public bool circle;
    [HideInInspector]
    public float circleRadius;
    [HideInInspector]
    public Vector3 circlePos;

    int? lastSelectedSpot = null;

    private void Start()
    {
        circlePos = transform.position;
    }

    public void AddSpot(Vector3 point)
    {
        spots.Add(transform.position + point);
    }

    public void RemoveSpot(int index)
    {
        spots.RemoveAt(index);
    }

    public void MoveSpot(int index, Vector3 newPos)
    {
        spots[index] = newPos;
    }

    public void RayTraceToGround(int index)
    {
        RaycastHit hit;
        if(Physics.Raycast(spots[index], Vector3.down, out hit, Mathf.Infinity)) {
            spots[index] = hit.point;
        }
    }

    public Vector3 GetNewSpot()
    {
        if (circle)
        {
            Vector2 newPos = Random.insideUnitCircle * circleRadius;
            return circlePos + new Vector3(newPos.x, 0, newPos.y);
        }

        int newSpot = Random.Range(0, spots.Count);

        //Select a new random point, avoid the one in use.
        while (lastSelectedSpot == newSpot) {
            newSpot = Random.Range(0, spots.Count);
        }

        lastSelectedSpot = newSpot;
        return spots[newSpot];
    }


}
