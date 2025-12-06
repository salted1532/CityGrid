using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class GridLineGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;       // A
    public int height = 10;      // B
    public float cellSize = 1f;

    [Header("Line Renderer Settings")]
    public float lineWidth = 0.02f;
    public Color lineColor = Color.green;

    private List<LineRenderer> runtimeLines = new List<LineRenderer>();


    void OnEnable()
    {
        // 에디터 모드에서는 런타임 라인 제거
        if (!Application.isPlaying)
        {
            ClearRuntimeLines();
            return;
        }

        // 플레이 모드에서는 라인 생성
        GenerateRuntimeGrid();
    }

    void OnDisable()
    {
        ClearRuntimeLines();
    }

    void ClearRuntimeLines()
    {
        foreach (var line in runtimeLines)
        {
            if (line != null)
                DestroyImmediate(line.gameObject);
        }
        runtimeLines.Clear();
    }

    // -------------------------------
    // 1) 실제 Game View용 라인 생성
    // -------------------------------
    void GenerateRuntimeGrid()
    {
        ClearRuntimeLines();

        Vector3 origin = transform.position;
        float totalWidth = width * cellSize;
        float totalHeight = height * cellSize;

        // 세로선
        for (int x = 0; x <= width; x++)
        {
            CreateRuntimeLine(
                origin + new Vector3(x * cellSize, 0, 0),
                origin + new Vector3(x * cellSize, 0, totalHeight)
            );
        }

        // 가로선
        for (int y = 0; y <= height; y++)
        {
            CreateRuntimeLine(
                origin + new Vector3(0, 0, y * cellSize),
                origin + new Vector3(totalWidth, 0, y * cellSize)
            );
        }
    }

    void CreateRuntimeLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("GridLine_Runtime");
        lineObj.transform.parent = transform;

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lineColor;
        lr.endColor = lineColor;

        lr.useWorldSpace = true;

        runtimeLines.Add(lr);
    }

    // -------------------------------
    // 2) 에디터에서만 보이는 Gizmo 라인
    // -------------------------------
    void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        Gizmos.color = lineColor;

        Vector3 origin = transform.position;
        float totalWidth = width * cellSize;
        float totalHeight = height * cellSize;

        // 세로선
        for (int x = 0; x <= width; x++)
        {
            Gizmos.DrawLine(
                origin + new Vector3(x * cellSize, 0, 0),
                origin + new Vector3(x * cellSize, 0, totalHeight)
            );
        }

        // 가로선
        for (int y = 0; y <= height; y++)
        {
            Gizmos.DrawLine(
                origin + new Vector3(0, 0, y * cellSize),
                origin + new Vector3(totalWidth, 0, y * cellSize)
            );
        }
    }
}
