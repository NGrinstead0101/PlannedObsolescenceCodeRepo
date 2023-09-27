/*****************************************************************************
// File Name :         RepairingRobot.cs
// Author :            Nick Grinstead, Thomas Revord
//
// Brief Description :  State script for handling revealing the robot part
                        for repair.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairingRobot : State
{
    GameStateTracker context;
    bool changeStateCalled = false;

    public RepairingRobot(GameStateTracker context)
    {
        this.context = context;
    }

    /// <summary>
    /// Reveals robot part that needs repairs
    /// </summary>
    public void ShowInfo()
    {
        context.confirmButton.SetActive(true);

        context.currentPart.SetActive(true);

        context.moneyFeedback.AddPart(context.currentPart.GetComponent<Part>());

        changeStateCalled = false;
    }

    /// <summary>
    /// Checks for success and provides feedback based on repair job
    /// Calls itself on short delay to show feedback briefly before
    /// changing states
    /// </summary>
    public void ChangeState()
    {
        if (!changeStateCalled)
        {
            changeStateCalled = true;

            context.confirmButton.SetActive(false);

            context.moneyFeedback.ResetFeedback();

            //check matches and sleeves
            int corpMatches = context.currentPart.GetComponent<Part>().checkCorp();
            int typeMatches = context.currentPart.GetComponent<Part>().checkType();
            int sleeves = context.currentPart.GetComponent<Part>().returnSleeves();
            int bonusMoney = context.currentPart.GetComponent<Part>().checkBonuses();
            //set success value
            int success = Mathf.CeilToInt(sleeves / 2f);
            //check for success
            if (corpMatches >= success)
            {
                //add to money tracker
                context.mt.GainMoney(corpMatches * 30);
            }
            //add to money tracker
            context.mt.GainMoney(typeMatches * 20);

            //add money for bonuses
            context.mt.GainMoney(bonusMoney);

            int numEmpty = context.currentPart.GetComponent<Part>().checkIsEmpty();
            context.mt.SpendMoney(numEmpty * 40);

            context.Invoke("ChangeState", 0.5f);
        }
        else
        {
            context.speechBubble.SetActive(false);
            context.dialogueBox.text = "";
            context.corpLogoImage.enabled = false;
            context.robotSprite.enabled = false;
            GameObject.Destroy(context.currentPart);

            if (context.robotQueue.Count > 0)
            {
                context.currentState = GameStateTracker.revealingRobots;
            }
            else
            {
                context.currentState = GameStateTracker.buyingCards;
            }

            context.ShowInfo();
        }
    }
}


