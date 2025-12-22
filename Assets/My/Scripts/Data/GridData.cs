using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition,
                            Vector2Int objectSize,
                            int ID,
                            int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                throw new Exception($"Dictionary already contains this cell positiojn {pos}");
            placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal;
    }
    public bool HasRoadNearby(Vector3Int position, int radius, Vector2Int objectSize)
    {
        // 건물이 차지하는 모든 셀 검사
        for (int ox = 0; ox < objectSize.x; ox++)
        {
            for (int oy = 0; oy < objectSize.y; oy++)
            {
                Vector3Int occupiedCell = position + new Vector3Int(ox, 0, oy);

                // 이 occupiedCell 기준으로 반경 검사
                bool cellHasRoad = false;

                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        Vector3Int checkPos = occupiedCell + new Vector3Int(x, 0, y);
                        if (placedObjects.ContainsKey(checkPos))
                        {
                            cellHasRoad = true;
                            break;
                        }
                    }
                    if (cellHasRoad) break;
                }

                // 건물의 한 셀이라도 도로 반경 밖이면 설치 불가
                if (!cellHasRoad)
                    return false;
            }
        }

        return true; // 전체 셀이 전부 도로 반경 4칸 안에 있음
    }
    public List<Vector3Int> CalculatePositionsPublic(Vector3Int gridPosition, Vector2Int objectSize)
    {
        return CalculatePositions(gridPosition, objectSize);
    }

    public bool CanPlaceObejctAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                return false;
        }
        return true;
    }

    internal int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (placedObjects.ContainsKey(gridPosition) == false)
            return -1;
        return placedObjects[gridPosition].PlacedObjectIndex;
    }

    internal void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex;
    }
}