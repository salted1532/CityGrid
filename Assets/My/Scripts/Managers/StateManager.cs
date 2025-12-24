using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool BuildState = false;
    private bool IdleState = true;
    private bool UIModeState = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateState (string state, bool stateOnOff)
    {
        if (state == "BuildState")
        {
            BuildState = stateOnOff;
        }
        if (state == "IdleState")
        {
            IdleState = stateOnOff;
        }
        if (state == "UIModeState")
        {
            UIModeState = stateOnOff;
        }
    }
}
