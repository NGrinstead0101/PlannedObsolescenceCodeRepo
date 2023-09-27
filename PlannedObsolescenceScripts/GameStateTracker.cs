/*****************************************************************************
// File Name :         GameStateTracker.cs
// Author :            Nick Grinstead, Thomas Revord
//
// Brief Description :  Tracks and updates the current game state. Has functions
                        for revealing relevant information and changing between
                        states.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateTracker : MonoBehaviour
{
    public Queue<Robot> robotQueue = new Queue<Robot>();
    public RobotGenerator robotGenerator;

    public Image corpLogoImage;
    public List<Sprite> corpLogoSprites = new List<Sprite>(4);

    public GameObject confirmButton;
    public TextMeshProUGUI dialogueBox;
    public GameObject storeInterface;
    public Image robotSprite;
    public GameObject currentPart;
    public MoneyTracker mt;
    public MoneyFeedback moneyFeedback;
    public GameObject speechBubble;
    public HandTracker handTracker;

    public static State revealingRobots;
    public static State repairingRobot;
    public static State buyingCards;

    public State currentState;

    /// <summary>
    /// Initializing States
    /// </summary>
    private void Awake()
    {
        revealingRobots = new RevealingRobots(this);
        repairingRobot = new RepairingRobot(this);
        buyingCards = new BuyingCards(this);

        BuyingCards.dayCount = 0;

        robotGenerator.GenerateRobots();

        currentState = revealingRobots;

        ShowInfo();
    }

    /// <summary>
    /// Calls ShowInfo function on current state
    /// </summary>
    public void ShowInfo()
    {
        currentState.ShowInfo();
    }

    /// <summary>
    /// Called by button to change state
    /// </summary>
    public void GetContinueInput()
    {
        ChangeState();
    }
    
    /// <summary>
    /// Calls ChangeState function on current state
    /// </summary>
    private void ChangeState()
    {
        currentState.ChangeState();
    }

    /// <summary>
    /// Called by RobotGenerator to add new robots to queue
    /// </summary>
    /// <param name="robotList"></param>
    public void AddRobots(List<Robot> robotList)
    {
        robotQueue.Clear();

        foreach (Robot robot in robotList)
        {
            robotQueue.Enqueue(robot);
        }
    }
}
