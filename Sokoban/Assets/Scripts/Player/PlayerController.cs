using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Command Idle, ArrowRight, ArrowLeft, ArrowUp, ArrowDown, ButtonRight, ButtonLeft, ButtonUp, ButtonDown;

    private CommandExecutor commandExecutor;

    // Start is called before the first frame update
    void Start()
    {
        commandExecutor = GetComponent<CommandExecutor>();

        Idle = new DoNothing();
        ArrowRight = new MoveRight();
        ArrowLeft = new MoveLeft();
        ArrowUp = new MoveUp();
        ArrowDown = new MoveDown();
    }

    // Update is called once per frame
    void Update()
    {
        if(commandExecutor.TryToExecute(HandleInput()))
        {

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
}
