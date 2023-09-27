/*****************************************************************************
// File Name :         CardConstruction.cs
// Author :            Nick Grinstead
//
// Brief Description :  Script that generates cards using certain parameters.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConstruction : MonoBehaviour
{
    [SerializeField] Sprite[] corpSprites;
    [SerializeField] Sprite[] typeSprites;

    [SerializeField] GameObject baseCardPrefab;
    Card currentCard;

    List<GameObject> newCards = new List<GameObject>();

    /// <summary>
    /// Generates the player's starting selection of cards
    /// </summary>
    /// <returns>List of card GameObjects</returns>
    public List<GameObject> GenerateBaseCards()
    {
        newCards.Clear();

        GameObject temp;

        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                temp = Instantiate(baseCardPrefab);
                currentCard = temp.GetComponent<Card>();
                currentCard.CreateCard(i, j, BonusGenerator(), corpSprites[i], typeSprites[j]);
                newCards.Add(temp);
            }
        }

        return newCards;
    }

    /// <summary>
    /// Generates a card with a specified suit
    /// </summary>
    /// <param name="chosenSuit">int corresponding to a suit</param>
    /// <returns>A card GameObject</returns>
    public GameObject CreateCardWithSuit(int chosenSuit)
    {
        GameObject temp = Instantiate(baseCardPrefab);
        
        currentCard = temp.GetComponent<Card>();
        int randType = Random.Range(0, 4);
        currentCard.CreateCard(chosenSuit, randType, BonusGenerator(), corpSprites[chosenSuit], typeSprites[randType]);

        return temp;
    }

    /// <summary>
    /// Generates a card with a specified suit and type
    /// </summary>
    /// <param name="chosenSuit">int corresponding to a suit</param>
    /// <param name="chosenType">int corresponding to a type</param>
    /// <returns>A card GameObject</returns>
    public GameObject CreateCardWithPart(int chosenSuit, int chosenType)
    {
        GameObject temp = Instantiate(baseCardPrefab);

        currentCard = temp.GetComponent<Card>();
        currentCard.CreateCard(chosenSuit, chosenType, BonusGenerator(), corpSprites[chosenSuit], typeSprites[chosenType]);

        return temp;
    }

    /// <summary>
    /// Helper function to decide if a card has a bonus
    /// </summary>
    /// <returns>null for no bonus or a CardBonus script</returns>
    private CardBonus BonusGenerator()
    {
        int randomNum = Random.Range(0, 100);

        if (randomNum <= 2)
        {
            // card has x2 bonus
            int bonusType = Random.Range(0, 4);
            return new CardBonus(bonusType, true);
        }
        else if (randomNum < 10)
        {
            // card has +2 bonus
            int bonusType = Random.Range(0, 4);
            return new CardBonus(bonusType, false);
        }

        // card has no bonus
        return null;
    }
}
