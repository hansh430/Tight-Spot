using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    public Route route;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem fx;
    private ParticleSystem.MainModule fxMainModulel;
    private void Start()
    {
        fxMainModulel = fx.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            if(car.route==route)
            {
                GameController.Instance.OnCarEntersPark?.Invoke(route);
                StarFx();
            }
        }
    }
    private void StarFx()
    {
        fxMainModulel.startColor = route.carColor;
        fx.Play();
    }
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
