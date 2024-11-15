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

    [SerializeField] private NewTankController[] tanks = new NewTankController[2];
    int currentTankSelecting = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(SpawnCards());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnCards()
    {
        int up = 0;
        for(int i = 0; i + (up * cardsPerRow) < allCards.Length; i++)
        {
            if (i == cardsPerRow)
            {
                up++;
                i = 0;
            }

            GameObject card = Instantiate(cardPrefab, new Vector3
                (spawnStart.position.x + 
                (i * sizeBetweenCardsHor)
                , spawnStart.position.y - 
                (up * sizeBetweenCardsVer)
                ),spawnStart.rotation,spawnStart.parent);
            card.GetComponent<CardReferences>().GiveCardValues(allCards[i + (up * cardsPerRow)],card);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator MoveCard(CardScriptable cardValues,GameObject actualCard)
    {
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

        GameObject card = Instantiate(cardPrefab, selectedSpots[spot].position, selectedSpots[spot].rotation, selectedSpots[spot]);
        card.GetComponent<CardReferences>().GiveCardValues(cardValues,actualCard);
        hasSelectedSpot[spot] = true;
        selectedCards[spot] = cardValues;
        actualCard.GetComponent<Image>().color = Color.gray;
        CheckAllCards();
        yield return new WaitForEndOfFrame();
    }

    public void RemoveCard(CardScriptable card, GameObject actualCard)
    {
        int index = -1;
        for (int i = 0; i < selectedCards.Length; i++)
        {
            if (card == selectedCards[i])
            {
                index = i;
                break;
            }
        }
        hasSelectedSpot[index] = false;
        selectedCards[index] = null;

        actualCard.GetComponent<Image>().color = Color.white;

        Destroy(selectedSpots[index].GetChild(0).gameObject);
        startButton.SetActive(false);
    }

    public void CheckAllCards()
    {
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
        for(int i = 0; i < tanks[currentTankSelecting].selectedBullets.Length; i++)
        {
            tanks[currentTankSelecting].selectedBullets[i] = selectedCards[i].bullet;
        }
        if(currentTankSelecting == 0)
        {
            ResetSelected();
            currentTankSelecting = 1;
        }
        else
        {
            //Turn stuff off
        }
    }

    private void ResetSelected()
    {
        for (int i = 0; i < selectedCards.Length; i++)
        {
            RemoveCard(selectedCards[i], selectedSpots[i].GetChild(0).GetComponent<CardReferences>().actualCard);

        }
    }
}
