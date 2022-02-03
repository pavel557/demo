using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//обнаружение юнитов с помощью луча

public class Detection : MonoBehaviour
{
    [SerializeField] private Vector2 directionView;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Color color = new Color(1f, 1f, 1f);
    [SerializeField] private UnityEvent Action;
    [SerializeField] private GameObject unitObject;

    private Unit unit;
    private Health health;
    private RaycastHit2D raycastHitData;

    private void Start()
    {
        unit = unitObject.GetComponent<Unit>();
        health = unitObject.GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        Observe();
    }

    private void Observe()
    {
        raycastHitData = Physics2D.Raycast(transform.position, directionView, rayLength, mask);
        Ray2D ray = new Ray2D(transform.position, directionView);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, color);
        Action.Invoke();
    }

    

    public void EnemyDetection()
    {
        if ((raycastHitData.collider))
        {
            unit.DistanceEnemy = raycastHitData.distance;
            if (raycastHitData.collider.GetComponent<Health>().Team != health.Team)
            {
                unit.IsEnemyDetected = true;
            }
        }
        else
        {
            unit.IsEnemyDetected = false;
            unit.DistanceEnemy = 1000f;
        }
    }

    public void RearDetection()
    {
        if (raycastHitData.collider)
        {
            if (raycastHitData.collider.GetComponent<Health>().Team == health.Team)
            {
                unit.IsAllyRear = true;
            }
        }
        else
        {
            unit.IsAllyRear = false;
        }
    }

    public void FrontDetection()
    {
        if ((raycastHitData.collider))
        {
                unit.IsAllyFront = true;
        }
        else
        {
            unit.IsAllyFront = false;
        }
    }
}

