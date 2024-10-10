using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private UserInput userInput;
    [SerializeField] private int interactableLayer;
    private Line currentLine;
    private Route currentRoute;
    private RaycastDetector raycastDetector = new();
    public UnityAction<Route, List<Vector3>> OnParkLinkedToLine;
    private void Start()
    {
        userInput.OnMouseDown += OnMouseDownHandler;
        userInput.OnMouseMove += OnMouseMoveHandler;
        userInput.OnMouseUp += OnMouseUpHandler;
    }

    private void OnMouseDownHandler()
    {
        ContactInfo contactInfo = raycastDetector.Raycast(interactableLayer);
        if (contactInfo.contacted)
        {
            bool isCar = contactInfo.collider.TryGetComponent(out Car _car);
            if (isCar && _car.route.isActive)
            {
                currentRoute = _car.route;
                currentLine = currentRoute.line;
                currentLine.Init();
            }
        }
    }
    private void OnMouseMoveHandler()
    {
        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.Raycast(interactableLayer);
            if (contactInfo.contacted)
            {
                Vector3 newPoint = contactInfo.point;
                currentLine.AddPoint(newPoint);
                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);
                if (isPark)
                {
                    Route parkRoute = _park.route;
                    if (parkRoute == currentRoute)
                    {
                        currentLine.AddPoint(contactInfo.transform.position);
                    }
                    else
                    {
                        currentLine.Clear();
                    }
                    OnMouseUpHandler();
                }
            }
        }
    }
    private void OnMouseUpHandler()
    {
        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.Raycast(interactableLayer);
            if(contactInfo.contacted) {
                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);
                if(currentLine.pointsCount<2 || !isPark)
                {
                    currentLine.Clear();
                }
                else
                {
                    OnParkLinkedToLine?.Invoke(currentRoute, currentLine.points);
                    currentRoute.Deactivate();
                }
            }
            else
            {
                currentLine.Clear();
            }
        }
        ResetDrawer();
    }

    private void ResetDrawer()
    {
        currentLine = null;             
        currentRoute = null;
    }
}
