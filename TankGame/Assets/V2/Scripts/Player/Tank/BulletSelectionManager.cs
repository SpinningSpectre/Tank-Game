using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletSelectionManager : MonoBehaviour
{
    public static BulletSelectionManager instance;
    [SerializeField] private CardScriptable[] allCards;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Transform spawnStart;
    [SerializeField] private int cardsPerRow = 4;
    [SerializeField] private int sizeBetweenCardsHor = 50;
    [SerializeField] private int sizeBetweenCardsVer = 50;

    [SerializeField] private Transform[] selectedSpots;
    public CardScriptable[] selectedCards = new CardScriptable[3];
    [SerializeField] private bool[] hasSelectedSpot = {false,false,false};

    [SerializeField] private GameObject startButton;

    //[SerializeField] private NewTankController[] tanks = new NewTankController[2];
    [SerializeField] private PlayerTurnManager turnManager;

    [SerializeField] private GameObject selectingUI;
    int currentTankSelecting = 0;
    void Start()
    {
        instance = this;

        //This is here for testing!!
        StartCoroutine(SpawnCards());
    }

    public IEnumerator SpawnCards()
    {
        int currentRow = 0;
        for(int i = 0; i + (currentRow * cardsPerRow) < allCards.Length; i++)
        {
            //if it reaches the amount of cards on a row, go to next row
            if (i == cardsPerRow)
            {
                currentRow++;
                i = 0;
            }

            //Spawn the card at the current position using i for horizontal and currentRow for vertical
            GameObject card = Instantiate(cardPrefab, new Vector3
                (spawnStart.position.x + 
                (i * sizeBetweenCardsHor)
                , spawnStart.position.y - 
                (currentRow * sizeBetweenCardsVer)
                ),spawnStart.rotation,spawnStart.parent);
            card.GetComponent<CardReferences>().GiveCardValues(allCards[i + (currentRow * cardsPerRow)],card);

            //wait between cards, could also be used for animations
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator MoveCard(CardScriptable cardValues,GameObject actualCard)
    {
        //Check for an available spot
        int spot = 0;
        if (hasSelectedSpot[0])
        {
            if (hasSelectedSpot[1])
            {
                if (hasSelectedSpot[2])
                {
                    yield break;
                }
                else { spot = 2; }
            }
            else { spot = 1; }
        }

        //Spawns a duplicate card and gives it a reference to the real card
        GameObject card = Instantiate(cardPrefab, selectedSpots[spot].position, selectedSpots[spot].rotation, selectedSpots[spot]);
        card.GetComponent<CardReferences>().GiveCardValues(cardValues,actualCard);

        //Tells this script what cards exist and where they are
        hasSelectedSpot[spot] = true;
        selectedCards[spot] = cardValues;

        //Sets the original card to gray
        actualCard.GetComponent<Image>().color = Color.gray;

        //Check if you have all cards selected
        CheckAllCards();

        //Here for when I want to add animation stuff later
        yield return new WaitForEndOfFrame();
    }

    public void RemoveCard(CardScriptable card, GameObject actualCard)
    {
        //Look for what card to remove
        int index = 0;
        for (int i = 0; i < selectedCards.Length; i++)
        {
            if (card == selectedCards[i])
            {
                index = i;
                break;
            }
        }

        //Removes it from this script
        hasSelectedSpot[index] = false;
        selectedCards[index] = null;

        //Sets the original card back
        actualCard.GetComponent<Image>().color = Color.white;

        //Destroys card and turns start button off, since if you remove a card you should never be able to start the game.
        Destroy(selectedSpots[index].GetChild(0).gameObject);
        startButton.SetActive(false);
    }

    public void CheckAllCards()
    {
        //Check if you have the max amount of cards selected and then turns on or off the start button
        for(int i = 0;i < hasSelectedSpot.Length;i++)
        {
            if (!hasSelectedSpot[i]) 
            {
                startButton.SetActive(false);
                return; 
            }
        }

        startButton.SetActive(true);
    }

    public void StartGame()
    {
        //Puts the bullets into the tanks
        for(int i = 0; i < turnManager.playerBullets.Length / 2; i++)
        {
            turnManager.playerBullets[currentTankSelecting, i] = selectedCards[i];
        }

        //If tank 0, go over to tank 1
        if(currentTankSelecting == 0)
        {
            ResetSelected();
            currentTankSelecting = 1;
        }
        else
        {
            selectingUI.SetActive(false);
            turnManager.StartRound();
        }
    }

    private void ResetSelected()
    {
        //Removes all cards you have, to reset the selecting.
        for (int i = 0; i < selectedCards.Length; i++)
        {
            RemoveCard(selectedCards[i], selectedSpots[i].GetChild(0).GetComponent<CardReferences>().actualCard);

        }
    }
}
