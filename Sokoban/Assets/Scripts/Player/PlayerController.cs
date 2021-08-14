using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles player input.
public class PlayerController : MonoBehaviour
{
    Command Idle, ArrowRight, ArrowLeft, ArrowUp, ArrowDown, ButtonRight, ButtonLeft, ButtonUp, ButtonDown;

    private CommandExecutor commandExecutor;

    private Command uiCommand = null;

    public Button UIButtonRight;
    public Button UIButtonLeft;
    public Button UIButtonUp;
    public Button UIButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        commandExecutor = GetComponent<CommandExecutor>();

        Idle = new DoNothing();
        ArrowRight = new MoveRight();
        ArrowLeft = new MoveLeft();
        ArrowUp = new MoveUp();
        ArrowDown = new MoveDown();
        ButtonRight = new MoveRight();
        ButtonLeft = new MoveLeft();
        ButtonUp = new MoveUp();
        ButtonDown = new MoveDown();
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


    Command HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return ArrowRight;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return ArrowLeft;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return ArrowUp;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return ArrowDown;
        }
        else
        {
            return Idle;
        }
    }

    public void HandleUIInput()
    {
        if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == UIButtonRight.gameObject.name)
        {
            uiCommand = ButtonRight;
        }
        else if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == UIButtonLeft.gameObject.name)
        {
            uiCommand = ButtonLeft;
        }
        else if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == UIButtonUp.gameObject.name)
        {
            uiCommand = ButtonUp;
        }
        else if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name == UIButtonDown.gameObject.name)
        {
            uiCommand = ButtonDown;
        }
        else
        {
            uiCommand = Idle;
        }
    }

    Command GetUICommand()
    {
        return uiCommand;
    }
}
