using UnityEngine;

public class TetherRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer prefab;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private LineRenderer lineRenderer;


    private void Start()
    {
        lineRenderer = Instantiate(prefab, transform);
    }

    private void Update()
    {
        if (startPoint && endPoint)
        {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }
}