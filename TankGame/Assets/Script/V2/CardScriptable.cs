using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
[System.Serializable]
public class CardScriptable : ScriptableObject
{
    [Header("Card")]
    public Sprite cardIcon;
    public string cardName;

    [Header("Bullet stats")]
    public BulletScriptable bullet;

    public string special1Text;
    public string special1Value;

    public string special2Text;
    public string special2Value;
}
