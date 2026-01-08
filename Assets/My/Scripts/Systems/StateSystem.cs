using UnityEngine;

public class StateSystem : MonoBehaviour
{
    bool IdleState;
    bool BuildState;
    bool UiState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IdleState = true;
        BuildState = false;
        UiState = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StateChange(string str)
    {
        switch (str)
        {
            case "idle":
                IdleState = true;
                BuildState = false;
                UiState = false;
                break;
            case "build":
                IdleState = false;
                BuildState = true;
                UiState = false;
                break;
            case "ui":
                IdleState = false;
                BuildState = false;
                UiState = true;
                break;
            default:
                IdleState = true;
                BuildState = false;
                UiState = false;
                break;
        } 
    }
}
