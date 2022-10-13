using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    Transform upgrades;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void buttonPressed()
    {
        upgrades.transform.position = new Vector2(387, 539);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
