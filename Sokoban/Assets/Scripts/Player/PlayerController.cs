using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles player input. Uses command pattern.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Command idle, commandRight, commandLeft, commandUp, commandDown;

    private CommandExecutor commandExecutor;

    private Command uiCommand = null;

    public MapController mapController;
    public Button UIButtonRight;
    public Button UIButtonLeft;
    public Button UIButtonUp;
    public Button UIButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        commandExecutor = GetComponent<CommandExecutor>();
        commandExecutor.MapControllerReference = mapController;

        idle = new DoNothing();
        commandRight = new MoveRight();
        commandLeft = new MoveLeft();
        commandUp = new MoveUp();
        commandDown = new MoveDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiCommand == null)
        {
            Command command = HandleInput();

            if (command.GetType() != idle.GetType())
            {
                commandExecutor.TryToExecute(command);
            }
        }
        else
        {
            Command command = GetUICommand();

            if (command.GetType() != idle.GetType())
            {
                commandExecutor.TryToExecute(command);
                uiCommand = null;
            }
        }
    }

    /// <summary>
    /// Handles user input. Can handle keyboard and UI input.
    /// </summary>
    /// <param name="gameObject">Optional parameter to enable handling input from UI.</param>
    /// <returns></returns>
    Command HandleInput(GameObject gameObject = null)
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || gameObject?.name == UIButtonRight.gameObject.name)
        {
            return commandRight;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || gameObject?.name == UIButtonLeft.gameObject.name)
        {
            return commandLeft;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || gameObject?.name == UIButtonUp.gameObject.name)
        {
            return commandUp;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || gameObject?.name == UIButtonDown.gameObject.name)
        {
            return commandDown;
        }
        else
        {
            return idle;
        }
    }

    /// <summary>
    /// UI movement controls binding. Calls regular HandleInput, but passes the last active button.
    /// </summary>
    public void HandleUIInput()
    {
        uiCommand = HandleInput(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject);
    }

    Command GetUICommand()
    {
        return uiCommand;
    }
}
