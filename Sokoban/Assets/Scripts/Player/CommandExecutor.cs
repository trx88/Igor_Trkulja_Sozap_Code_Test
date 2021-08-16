using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Executes command sent by PlayerController.
/// </summary>
public class CommandExecutor : MonoBehaviour
{
    private Command commandToExecute = null;

    private MapController mapController;

    public MapController MapControllerReference
    {
        get => mapController;
        set
        {
            mapController = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        commandToExecute = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToExecute(Command command)
    {
        //Free the command "space" only when movement is completed.
        if (commandToExecute != null && commandToExecute.commandCompleted)
        {
            commandToExecute = null;
        }

        if (commandToExecute == null)
        {
            commandToExecute = command;
            commandToExecute.Execute(mapController);

            //Check if level is completed.
            if(mapController.AreBoxesInPlace())
            {
                Debug.Log("LEVEL COMPLETED!");
                mapController.CompleteLevel();
            }
        }
    }
}
