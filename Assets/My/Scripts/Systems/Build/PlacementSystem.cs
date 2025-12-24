using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectDatabaseSO database;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData roadData, StructureData;

   //private Renderer previewRenderer;

    private List<GameObject> placedGameObject = new();

    [SerializeField]
    private PreviewSystem preview;
    private Vector3Int lastDectectedPosition = Vector3Int.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StopPlacement();
        roadData = new();
        StructureData = new();
        //previewRenderer = new();
        //previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found{ID}");
            return;
        }
        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(
            database.objectData[selectedObjectIndex].Prefab,
            database.objectData[selectedObjectIndex].Size);
        //cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
            //UI 위에 클릭시 코드 입력 부분
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        //건물 ID index 일시
        if (selectedObjectIndex > 0)
        {
            //도로 반경내 확인
            bool RoadValidity = CanPlaceStructure(gridPosition, selectedObjectIndex);
            //건물 겹침 확인
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            if (RoadValidity == false)
            {
                return;
            }
            if(placementValidity == false)
            {
                return;
            }
        }
        //도로 ID index 일시
        else if (selectedObjectIndex == 0)
        {
            //도로 겹침 확인
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            if (placementValidity == false)
            {
                return;
            }
        }

        //설치 코드들
        GameObject newObject = Instantiate(database.objectData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPosition);
        placedGameObject.Add(newObject);
        GridData selectedData = database.objectData[selectedObjectIndex].ID == 0 ? roadData : StructureData;
        selectedData.AddObjectAt(gridPosition, database.objectData[selectedObjectIndex].Size, database.objectData[selectedObjectIndex].ID, placedGameObject.Count - 1);
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
        Debug.Log(gridPosition);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.objectData[selectedObjectIndex].ID == 0 ? roadData : StructureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.objectData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPreview();
        //cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDectectedPosition = Vector3Int.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if(lastDectectedPosition != gridPosition)
        {
            //bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            if(selectedObjectIndex > 0)
            {
                bool RoadValidity = CanPlaceStructure(gridPosition, selectedObjectIndex);
                bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
                bool CheckValidity;
                if (RoadValidity && placementValidity)
                {
                    CheckValidity = true;
                }
                else
                {
                    CheckValidity = false;
                }
                preview.UpdatePosition(grid.CellToWorld(gridPosition), CheckValidity);
            }
            else if(selectedObjectIndex == 0)
            {
                bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
                preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            }
            mouseIndicator.transform.position = mousePosition;
            //previewRenderer.material.color = placementValidity ? Color.white : Color.red;
            //cellIndicator.transform.position = grid.CellToWorld(gridPosition);
            //preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            lastDectectedPosition = gridPosition;
        }
    }

    private bool CanPlaceStructure(Vector3Int gridPosition, int selectedObjectIndex)
    {
        // 건물이면 1) 충돌 검사 2) 도로 반경 검사 둘 다 필요
        bool noCollision = roadData.CanPlaceObejctAt(gridPosition, database.objectData[selectedObjectIndex].Size);

        if (!noCollision)
        {
            return false;
        }

        // 도로 반경 4칸 내에 도로가 있는지 검사
        bool nearRoad = roadData.HasRoadNearby(gridPosition, 4, database.objectData[selectedObjectIndex].Size);

        return nearRoad;
    }
}
