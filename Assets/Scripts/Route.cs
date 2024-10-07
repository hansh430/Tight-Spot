using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;
    public Line line;
    public Park park;
    public Car car;

    [Space]
    [Header("Colors :")]
    public Color carColor;
    [SerializeField] private Color lineColor;

    public void Deactivate()
    {
        isActive = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.Log("1");
        if (!Application.isPlaying && line!=null && car!=null && park!=null)
        {
            Debug.Log("2");
            line.lineRenderer.SetPosition(0,car.bottomTransform.position);
            line.lineRenderer.SetPosition(1,park.transform.position);

            car.SetColor(carColor);
            park.SetColor(carColor);
            line.SetColor(lineColor);
        }
    }
#endif
}
