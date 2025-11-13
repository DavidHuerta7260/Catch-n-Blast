using UnityEngine;

public class SimpleLine : MonoBehaviour
{
    public Transform hook;
    public Transform topAnchor;

    void Update()
    {
        Vector3 dir = hook.position - topAnchor.position;
        float distance = dir.magnitude;

        transform.position = topAnchor.position + dir / 2;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        transform.localScale = new Vector3(0.02f, distance / 2, 1);
    }
}

