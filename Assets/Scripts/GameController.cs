using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [HideInInspector] public List<Route> ReadyRoutes = new();
    public UnityAction<Route> OnCarEntersPark;
    public UnityAction OnCarCollision;
    private int totalRoutes;
    private int totalCarParked;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
        OnCarEntersPark += OnCarEntersParkHandler;
        OnCarCollision += OnCarCollisionHandler;
        totalCarParked = 0;
    }

    private void OnCarCollisionHandler()
    {
        Debug.Log("Game Over");
        DOVirtual.DelayedCall(2f, () =>
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevel);
        });
    }

    private void OnDestroy()
    {
        OnCarEntersPark -= OnCarEntersParkHandler;
        OnCarCollision -= OnCarCollisionHandler;
    }
    private void OnCarEntersParkHandler(Route route)
    {
        route.car.StopCarAnimations();
        totalCarParked++;
        if (totalCarParked == totalRoutes)
        {
            Debug.Log("You Win");
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(nextLevel);
                else
                    Debug.LogWarning("No Next Level To Load");
            });
        }
    }

    public void RegisterRoutes(Route route)
    {
        ReadyRoutes.Add(route);
        if (ReadyRoutes.Count == totalRoutes)
        {
            MoveAllCars();
        }
    }

    private void MoveAllCars()
    {
        foreach (var route in ReadyRoutes)
        {
            route.car.MoveCar(route.linePoints);
        }
    }
}
