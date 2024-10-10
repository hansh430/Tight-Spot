using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [HideInInspector] public List<Route> ReadyRoutes = new();
    private int totalRoutes;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
    }
    public void RegisterRoutes(Route route)
    {
        ReadyRoutes.Add(route);
        if(ReadyRoutes.Count==totalRoutes)
        {
            MoveAllCars();
        }
    }

    private void MoveAllCars()
    {
      foreach(var route in ReadyRoutes)
        {
            route.car.MoveCar(route.linePoints);
        }
    }
}
