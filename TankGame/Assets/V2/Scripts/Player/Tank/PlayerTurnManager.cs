using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletSelectMenu;
    public CardScriptable[,] playerBullets = new CardScriptable[2,3];
    [SerializeField] private Transform[] cardPosition;
    [SerializeField] private GameObject bulletCard;
    private int currentPlayer = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRound()
    {
        bulletSelectMenu.SetActive(true);
        for(int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(bulletCard, cardPosition[i].position, cardPosition[i].rotation,bulletSelectMenu.transform);
            card.GetComponent<CardReferences>().GiveCardValues(playerBullets[currentPlayer,i], card);
        }
    }
}
