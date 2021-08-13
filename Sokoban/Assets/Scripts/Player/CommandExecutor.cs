using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    private Command CommandToExecute = null;

    //For testing only
    public MapController mapController;

    // Start is called before the first frame update
    void Start()
    {
        CommandToExecute = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToExecute(Command command)
    {
        if (CommandToExecute != null && CommandToExecute.CommandCompleted)
        {
            CommandToExecute = null;
        }

        if (CommandToExecute == null)
        {
            CommandToExecute = command;
            CommandToExecute.Execute(mapController);
        }
    }
}
