using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RayAttack", menuName = "Utilities/BossAttacks/RayAttack")]
public class RayAttack : BossAttack
{
    [SerializeField] private int speed;
    [SerializeField] private GameObject ray;
    [SerializeField] private int amountOfRays;
    private List<GameObject> rays = new List<GameObject>();
    private float angle;

    public override void EnterState(BossPhase phase)
    {
        rays.Clear();
        base.EnterState(phase);
        Debug.Log(currentPhase);
        for (int i = 0; i < amountOfRays; i++)
        {
            GameObject _temp = Instantiate(ray);
            _temp.transform.parent = TempHolder.transform;
            rays.Add(_temp);
            //_temp.transform.position += new Vector3(_temp.GetComponentInChildren<BoxCollider>().bounds.size.x, 0, 0);

            float dX = 15 * Mathf.Cos(i * 2 * Mathf.PI / amountOfRays);
            float dY = 15 * Mathf.Sin(i * 2 * Mathf.PI / amountOfRays);
            _temp.transform.position = boss.transform.position + new Vector3(dX, 0, dY);
            _temp.transform.LookAt(boss.transform.position);
        }
    }
    public override void Update()
    {
        base.Update();
        angle = Time.deltaTime * speed;
        foreach (GameObject ray in rays)
        {
            ray.transform.RotateAround(boss.transform.position, Vector3.up, angle);
            ray.transform.LookAt(boss.transform.position, Vector3.right);
        }
    }
    public override void ExitState()
    {
        currentPhase.RemoveSubState(this);
        foreach (GameObject ray in rays)
        {
            ray.SetActive(false);
            Destroy(ray);
        }
        rays.Clear();
    }
}