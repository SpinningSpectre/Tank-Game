using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletSelectMenu;
    public CardScriptable[,] playerBullets = new CardScriptable[2,3];
    [SerializeField] private NewTankController[] tanks = new NewTankController[2];
    [SerializeField] private Transform[] cardPosition;
    [SerializeField] private GameObject bulletCard;
    private int currentPlayer = 0;

    public void StartRound()
    {
        for(int i = 0; i < tanks.Length; i++)
        {
            tanks[i].turnManager = this;
            tanks[i].playerNumber = i;
        }
        ShowCards();
    }

    List<GameObject> cards = new List<GameObject>();
    private void ShowCards()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards = new List<GameObject>();
        bulletSelectMenu.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(bulletCard, cardPosition[i].position, cardPosition[i].rotation, bulletSelectMenu.transform);
            card.GetComponent<CardReferences>().GiveCardValues(playerBullets[currentPlayer, i], card);
            card.GetComponent<CardReferences>().turnManager = this;
            card.GetComponent<CardReferences>().bulletNumber = i;
            cards.Add(card);
        }
    }

    public void Swap()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].canShoot = false;
        }
        if (currentPlayer == 0)
        {
            currentPlayer = 1;
        }
        else { currentPlayer = 0; }
        ShowCards();
    }

    public void SelectBullet(int bullet)
    {
        tanks[currentPlayer].canShoot = true;
        bulletSelectMenu.SetActive(false);
        tanks[currentPlayer].currentBullet = playerBullets[currentPlayer,bullet].bullet;
    }
}
