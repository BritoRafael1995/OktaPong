using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Sight : MonoBehaviour
{
    [SerializeField] private int maxReflections = 2, defaultRayDistance = 3, Infinity = 3;

    private int currentReflections = 0;
    private Vector2 startPoint, direction;
    private List<Vector3> Points;
    private LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        Points = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        GenerateRay();
    }

    private void GenerateRay()
    {
        startPoint = transform.position;
        direction = transform.up;
        var hitData = Physics2D.Raycast(startPoint, direction.normalized, defaultRayDistance);

        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData)
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + direction.normalized * Infinity);
        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);
        if (newHitData)
        {
            ReflectFurther(hitData.point, newHitData);
        }
        else
        {
            Points.Add(hitData.point + newDirection * defaultRayDistance);
        }
    }
}
