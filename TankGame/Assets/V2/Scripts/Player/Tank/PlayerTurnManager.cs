using System.Collections;
using System.Collections.Generic;
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
        bulletSelectMenu.SetActive(true);
        for(int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(bulletCard, cardPosition[i].position, cardPosition[i].rotation,bulletSelectMenu.transform);
            card.GetComponent<CardReferences>().GiveCardValues(playerBullets[currentPlayer,i], card);
            card.GetComponent<CardReferences>().turnManager = this;
            card.GetComponent<CardReferences>().bulletNumber = i;
        }
    }

    public void SelectBullet(int bullet)
    {
        bulletSelectMenu.SetActive(false);
        tanks[currentPlayer].currentBullet = playerBullets[bullet,currentPlayer].bullet;
    }
}
