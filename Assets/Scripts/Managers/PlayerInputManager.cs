using UnityEngine;
using System.Collections;
using System;

public class PlayerInputManager : MonoBehaviour
{
    public bool isActive;
    public bool isTetrisTurn = true;
    private bool hasClicked = false;

    private Vector3 playerTargetPos;

    void Awake()
    {

    }

    void Update()
    {
        if (isActive)
        {
            PlayerInput();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    } 
    
    void PlayerInput()
    { 
        if (isTetrisTurn)
        {
            playerTargetPos = Managers.Spawner.thePlayer.GetComponent<Animator>().transform.position;
            Debug.Log("Block Turn");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Managers.Game.currentShape.movementController.RotateClockWise(false); 
                // Managers.Game.currentShape.movementController.RotateClockWise(true); //alternate way
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Managers.Game.currentShape.movementController.MoveHorizontal(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (Managers.Game.currentShape != null)
                {
                    isActive = false;
                    Managers.Game.currentShape.movementController.InstantFall();
                }
            }
        }
        else
        {
            Debug.Log("Player Turn");
            if (Input.GetKeyDown(KeyCode.Mouse0) && !hasClicked)
            {
                // Set the position for the player. Round to nearast whole number so they snap into the grid perfectly.
                playerTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                playerTargetPos.x = (float)(Math.Round((double)playerTargetPos.x, 0));
                playerTargetPos.y = (float)(Math.Round((double)playerTargetPos.y, 0));
                playerTargetPos.z = 0;

                if (Managers.Grid.ValidClick(playerTargetPos.x, playerTargetPos.y))
                {
                    hasClicked = true;

                    if (playerTargetPos.x > Managers.Spawner.thePlayer.transform.position.x)
                    {
                        Managers.Spawner.thePlayer.transform.eulerAngles = new Vector3(0, 0, 0); // Normal
                    }
                    else if (playerTargetPos.x < Managers.Spawner.thePlayer.transform.position.x)
                    {
                        Managers.Spawner.thePlayer.transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
                    }
                    Managers.Spawner.thePlayer.GetComponent<Animator>().enabled = true;
                    Managers.Audio.PlayMoveSound();
                    Managers.Spawner.thePlayer.GetComponent<PlayerController>().PostRoundStatUpdate();
                }
            }
            if (hasClicked)
            {
                Managers.Spawner.thePlayer.transform.position = Vector3.MoveTowards(Managers.Spawner.thePlayer.transform.position, playerTargetPos, Time.deltaTime * 1);
                if (playerTargetPos == Managers.Spawner.thePlayer.transform.position)
                {
                    Managers.Spawner.thePlayer.GetComponent<Animator>().enabled = false;
                    Managers.UI.popUps.ActivateGameDialogPopup();
                }
            } 
        }
    }

    public void PostPopupChores()
    {
        // Consume aby item
        Managers.Items.ConsumeItem();
        // Restore blocks to blue if any were green.
        Managers.Grid.RevertGridColor();
        Managers.Input.isTetrisTurn = true;
        Managers.Spawner.SpawnShape(); 
        isActive = true;
        hasClicked = false;

    }

}
