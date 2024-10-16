using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Route route;
    public Transform bottomTransform;
    public Transform bodyTransform;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float danceValue;
    [SerializeField] private float durationMultiplier;
    float angle = 10f;

    private void Start()
    {
        bodyTransform.DOLocalMoveY(danceValue, 0.1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    public void MoveCar(Vector3[] path)
    {
        rb.DOLocalPath(path,2f*durationMultiplier*path.Length)
            .SetLookAt(0.01f,false)
            .SetEase(Ease.Linear);
    }
    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;
    }

    public void StopCarAnimations()
    {
        bodyTransform.DOKill(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent(out Car otherCar))
        {
            StopAllCoroutines();
            rb.DOKill(false);
            Vector3 hitPoint = collision.contacts[0].point;
            AddExplosionForce(hitPoint);
            smokeEffect.Play();
            GameController.Instance.OnCarCollision?.Invoke();
        }
    }

    private void AddExplosionForce(Vector3 hitPoint)
    {
        rb.AddExplosionForce(200f, hitPoint, 3f);
        rb.AddForceAtPosition(Vector3.up * 2f, hitPoint, ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
    }
    private float GetRandomAngle()
    {
        float randomAngle = UnityEngine.Random.value;
        return randomAngle > 0.5f ? angle : -angle;
    }
}
