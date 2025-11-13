using UnityEngine;

public class FishingLineRenderer : MonoBehaviour
{
    public Transform hook;  // Drag your hook object here
    private LineRenderer lineRenderer;
    private Vector3 topPoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // The top point (adjust this to match your camera’s framing)
        topPoint = new Vector3(0f, Camera.main.orthographicSize, 0f);
    }

    void Update()
    {
        if (hook != null)
        {
            // Update both points each frame
            lineRenderer.SetPosition(0, topPoint);
            lineRenderer.SetPosition(1, hook.position);
        }
    }
}
