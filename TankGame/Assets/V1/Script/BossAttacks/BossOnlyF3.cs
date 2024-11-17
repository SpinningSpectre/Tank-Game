using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOnlyF3 : MonoBehaviour
{
    bool hasUsedAttack3 = false;
    bool attack3Active = false;
    [SerializeField]
    Transform leftBlock;
    [SerializeField]
    Transform rightBlock;
    [SerializeField]
    Transform middleBlock;
    [SerializeField]
    GameObject midBlock;
    [SerializeField]
    Transform currentlyMovingTo;
    [SerializeField]
    float timeTillNextBomb;
    bool hasANumber = false;
    [SerializeField]
    GameObject bombToDrop;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    int bossNumber;
    // Start is called before the first frame update
    void Start()
    {
        leftBlock = GameObject.Find("BossMoveTo1").GetComponent<Transform>();
        rightBlock = GameObject.Find("BossMoveTo2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack3Active == true)
        {
            timeTillNextBomb -= Time.deltaTime * 10;
            if (hasANumber == false)
            {
                timeTillNextBomb = Random.Range(05, 10);
                hasANumber = true;
            }
            if (timeTillNextBomb <= 0)
            {
                GameObject bullet = Instantiate(bombToDrop, firePoint.position, firePoint.rotation);
                hasANumber = false;
            }
        }
        if (attack3Active == false)
        {
            ToMiddle();
        }
        attack3Active = true;
        hasUsedAttack3 = true;
        if (hasUsedAttack3 == true)
        {
            Vector2 bossMoving = currentlyMovingTo.position - transform.position;
            transform.Translate(new Vector2(bossMoving.normalized.x * Time.deltaTime * 4, bossMoving.normalized.y * Time.deltaTime * 4));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Attack 3
        if (collision.gameObject.CompareTag("LeftTrigger"))
        {
            Debug.Log("LeftTrig");
            ToRight();
        }
        if (collision.gameObject.CompareTag("RightTrigger"))
        {
            Debug.Log("RightTrig");
            ToLeft();
        }
        if (collision.gameObject.CompareTag("MiddleTrigger"))
        {
            Debug.Log("MiddleTrig");
            if (bossNumber == 1)
            {
                ToRight();
            }
            if (bossNumber == 2)
            {
                ToLeft();
            }
            Object.Destroy(midBlock);
        }
    }
    public void ToLeft()
    {
        Debug.Log("1");
        currentlyMovingTo = leftBlock;
    }
    public void ToRight()
    {
        Debug.Log("2");
        attack3Active = true;
        currentlyMovingTo = rightBlock;
    }
    public void ToMiddle()
    {
        Debug.Log("3");
        currentlyMovingTo = middleBlock;
    }
}
