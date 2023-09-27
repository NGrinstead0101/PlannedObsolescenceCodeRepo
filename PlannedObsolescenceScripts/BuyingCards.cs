/*****************************************************************************
// File Name :         BuyingCards.cs
// Author :            Nick Grinstead
//
// Brief Description :  State script for handling the shoping phase.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyingCards : State
{
    GameStateTracker context;
    public static int dayCount = 0;

    public BuyingCards(GameStateTracker context)
    {
        this.context = context;
    }

    /// <summary>
    /// Checks loss condition, opens store, and then generates new robots
    /// </summary>
    public void ShowInfo()
    {
        // checking loss condition
        if (context.handTracker.cardsInHand.Count < 10 && context.mt.moneyTotal < 20)
        {
            SceneManager.LoadScene("LoseScene");
        }

        context.confirmButton.SetActive(true);

        context.storeInterface.SetActive(true);

        ++dayCount;

        if (dayCount % 2 == 0)
        {
            context.robotGenerator.UpdateTargetCount();
        }

        context.robotGenerator.GenerateRobots();
    }

    /// <summary>
    /// Closes shop and switches to revealing robots
    /// </summary>
    public void ChangeState()
    {
        context.confirmButton.SetActive(false);

        context.storeInterface.SetActive(false);

        context.currentState = GameStateTracker.revealingRobots;

        context.ShowInfo();
    }
}
