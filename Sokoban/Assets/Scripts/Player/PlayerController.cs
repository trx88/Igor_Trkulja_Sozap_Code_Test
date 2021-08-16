using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles player input.
public class PlayerController : MonoBehaviour
{
    Command Idle, CommandRight, CommandLeft, CommandUp, CommandDown;

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

        Idle = new DoNothing();
        CommandRight = new MoveRight();
        CommandLeft = new MoveLeft();
        CommandUp = new MoveUp();
        CommandDown = new MoveDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiCommand == null)
        {
            Command command = HandleInput();

            if (command.GetType() != Idle.GetType())
            {
                commandExecutor.TryToExecute(command);
            }
        }
        else
        {
            Command command = GetUICommand();

            if (command.GetType() != Idle.GetType())
            {
                commandExecutor.TryToExecute(command);
                uiCommand = null;
            }
        }
    }


    Command HandleInput(GameObject gameObject = null)
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || gameObject?.name == UIButtonRight.gameObject.name)
        {
            return CommandRight;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || gameObject?.name == UIButtonLeft.gameObject.name)
        {
            return CommandLeft;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || gameObject?.name == UIButtonUp.gameObject.name)
        {
            return CommandUp;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || gameObject?.name == UIButtonDown.gameObject.name)
        {
            return CommandDown;
        }
        else
        {
            return Idle;
        }
    }

    public void HandleUIInput()
    {
        uiCommand = HandleInput(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject);
    }

    Command GetUICommand()
    {
        return uiCommand;
    }
}
