using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardReferences : MonoBehaviour
{
    public TMP_Text cardName;
    public Image cardImage;

    public TMP_Text cardDamage;
    public TMP_Text cardSplash;
    public TMP_Text cardSpecial1;
    public TMP_Text cardSpecial2;
    public TMP_Text cardSpecialValue1;
    public TMP_Text cardSpecialValue2;

    public CardScriptable scriptable;

    public GameObject actualCard = null;

    public PlayerTurnManager turnManager;
    public int bulletNumber;

    public void GiveCardValues(CardScriptable card,GameObject realCard)
    {
        cardName.text = card.cardName;
        cardImage.sprite = card.cardIcon;

        cardDamage.text = card.bullet.directDamage.ToString();
        cardSplash.text = card.bullet.explosionSize.ToString();

        cardSpecial1.text = card.special1Text;
        cardSpecialValue1.text = card.special1Value;

        cardSpecial2.text = card.special2Text;
        cardSpecialValue2.text = card.special2Value;

        scriptable = card;
        actualCard = realCard;
    }

    /// <summary>
    /// Moves the card to the next available spot
    /// </summary>
    public void MoveCard()
    {
        //Get all spots
        CardScriptable[] selecteds = BulletSelectionManager.instance.selectedCards;
        for (int i = 0; i < selecteds.Length; i++)
        {
            //if its already in a spot then remove the card
            if(scriptable == selecteds[i])
            {
                BulletSelectionManager.instance.RemoveCard(scriptable,actualCard);
                return;
            }
        }
        //Otherwise move the card into the next available spot
        StartCoroutine(BulletSelectionManager.instance.MoveCard(scriptable,actualCard));
        
    }

    public void SelectBullet()
    {
        turnManager.SelectBullet(bulletNumber);
    }
}
