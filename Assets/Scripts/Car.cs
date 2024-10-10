using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Route route;
    public Transform bottomTransform;
    public Transform bodyTransform;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float danceValue;
    [SerializeField] private float durationMultiplier;
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
}
