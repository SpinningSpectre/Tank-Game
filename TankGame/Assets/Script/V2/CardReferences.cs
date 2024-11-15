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

    public void GiveCardValues(CardScriptable card,GameObject realCard)
    {
        cardName.text = card.cardName;
        cardImage.sprite = card.cardIcon;

        cardDamage.text = card.bullet.damage.ToString();
        cardSplash.text = card.bullet.explosionSize.ToString();

        cardSpecial1.text = card.special1Text;
        cardSpecialValue1.text = card.special1Value;

        cardSpecial2.text = card.special2Text;
        cardSpecialValue2.text = card.special2Value;

        scriptable = card;
        actualCard = realCard;
    }

    public void MoveCard()
    {
        CardScriptable[] selecteds = BulletSelectionManager.instance.selectedCards;
        for (int i = 0; i < selecteds.Length; i++)
        {
            if(scriptable == selecteds[i])
            {
                BulletSelectionManager.instance.RemoveCard(scriptable,actualCard);
                return;
            }
        }
        StartCoroutine(BulletSelectionManager.instance.MoveCard(scriptable,actualCard));
        
    }
}
