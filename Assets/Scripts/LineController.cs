using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;
    private List<Vector3> points;
    public float minDistance = 0.1f;
    EdgeCollider2D edgeCollider;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        points = new List<Vector3>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
            {
                points.Add(currentPosition);
                UpdateLineRenderer(points);
                previousPosition = currentPosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetEdgeCollider();
            RestartManager.instance.ShowPanel();
        }
    }

    void UpdateLineRenderer(List<Vector3> pointList)
    {
        line.positionCount = pointList.Count;
        line.SetPositions(pointList.ToArray());
    }

    void SetEdgeCollider()
    {
        Vector2[] edgePoints = points.ConvertAll(p => new Vector2(p.x, p.y)).ToArray();
        edgeCollider.points = edgePoints;
    }
}