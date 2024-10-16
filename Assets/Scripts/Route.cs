using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;
    [HideInInspector] public Vector3[] linePoints;
    [SerializeField] private LineDrawer lineDrawer;

    [Space]
    public Line line;
    public Park park;
    public Car car;

    [Space]
    [Header("Colors :")]
    public Color carColor;
    [SerializeField] private Color lineColor;
    private void Start()
    {
        lineDrawer.OnParkLinkedToLine += OnParkLinkedToLineHandler;
    }
    private void OnDestroy()
    {
        lineDrawer.OnParkLinkedToLine -= OnParkLinkedToLineHandler;
    }
    private void OnParkLinkedToLineHandler(Route route, List<Vector3> points)
    {
        if (route == this)
        {
            linePoints = points.ToArray();
            GameController.Instance.RegisterRoutes(this);
        }
    }

    public void Deactivate()
    {
        isActive = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && line != null && car != null && park != null)
        {
            line.lineRenderer.SetPosition(0, car.bottomTransform.position);
            line.lineRenderer.SetPosition(1, park.transform.position);

            car.SetColor(carColor);
            park.SetColor(carColor);
            line.SetColor(lineColor);
        }
    }
#endif
}
