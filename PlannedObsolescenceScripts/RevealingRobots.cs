/*****************************************************************************
// File Name :         RevealingRobots.cs
// Author :            Nick Grinstead, Thomas Revord
//
// Brief Description :  State script for handling the phase where a new robot
                        arrives at the shop.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealingRobots : State
{
    GameStateTracker context;

    public RevealingRobots(GameStateTracker context)
    {
        this.context = context;
    }

    /// <summary>
    /// Reveals the next robot and their dialogue
    /// Continues to next state after 2 seconds
    /// </summary>
    public void ShowInfo()
    {
        Robot currentRobot = context.robotQueue.Dequeue();

        context.corpLogoImage.enabled = true;
        context.corpLogoImage.sprite = context.corpLogoSprites[currentRobot.Corp];

        context.speechBubble.SetActive(true);
        context.dialogueBox.text = currentRobot.Dialogue;

        //get part reference
        context.currentPart = currentRobot.Part;
        context.currentPart.GetComponent<Part>().UpdateSuit(currentRobot.Corp);

        // update robot sprite
        context.robotSprite.enabled = true;
        context.robotSprite.sprite = currentRobot.RobotSprite;

        context.Invoke("GetContinueInput", 2f);
    }

    /// <summary>
    /// Switches to repairing robots state
    /// </summary>
    public void ChangeState()
    {
        context.currentState = GameStateTracker.repairingRobot;

        context.ShowInfo();
    }
}
