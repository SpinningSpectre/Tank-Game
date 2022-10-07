using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider mySlider;
    public TMP_Text myText;
    public TMP_Text sliderText;
    [SerializeField]
    GameObject selectedAttack;

    // Start is called before the first frame update
    public void ChangeText()
    {
        myText.text = "Upgrade Active";
    }
    public void ChangeAttack()
    {
        
    }

    void Update()
    {
        sliderText.text = mySlider.value.ToString();
    }
}
