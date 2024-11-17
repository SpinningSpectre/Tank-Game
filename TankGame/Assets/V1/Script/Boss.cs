using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Text hpText;
    bool hasHadInvincible = false;
    bool bossHP100 = false;
    bool hasUsedAttack1 = false;
    bool attack1IsInactive = true;
    bool hasUsedAttack2 = false;
    bool hasUsedAttack3 = false;
    bool attack3Active = false;
    public float health = 100;
    public float damageMultiplier = 1;
    [SerializeField]
    GameObject death;
    [SerializeField]
    GameObject Shield;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    SpriteRenderer spriteRenderer2;
    [SerializeField]
    SpriteRenderer spriteRenderer3;
    [SerializeField]
    SpriteRenderer spriteRenderer4;
    [SerializeField]
    Transform tankBody;
    [SerializeField]
    GameObject theEntireTank;
    public Slider hpBar;
    Animator animator;
    Animator animatorBackround;
    [SerializeField]
    Transform leftBlock;
    [SerializeField]
    Transform rightBlock;
    [SerializeField]
    Transform middleBlock;
    [SerializeField]
    Transform normalBlock;
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
    // Start is called before the first frame update
    void Start()
    {
        leftBlock = GameObject.Find("BossMoveTo1").GetComponent<Transform>();
        rightBlock = GameObject.Find("BossMoveTo2").GetComponent<Transform>();
        normalBlock = GameObject.Find("BossStand").GetComponent<Transform>();
        animatorBackround = GameObject.Find("Backround").GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GameObject.Find("Shield").GetComponent<SpriteRenderer>();
        spriteRenderer2 = GameObject.Find("BossEye").GetComponent<SpriteRenderer>();
        spriteRenderer3 = GameObject.Find("BossEye2").GetComponent<SpriteRenderer>();
        spriteRenderer4 = GameObject.Find("Halo").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack3Active == true)
        {
            timeTillNextBomb -= Time.deltaTime * 10;
            if (hasANumber == false)
            {
                timeTillNextBomb = Random.Range(05, 15);
                hasANumber = true;
            }
            if (timeTillNextBomb <= 0)
            {
                GameObject bullet = Instantiate(bombToDrop, firePoint.position, firePoint.rotation);
                hasANumber = false;
            }
        }
        if (hasHadInvincible == false)
        {
            if (health <= 100)
            {
                damageMultiplier = 0.3f;
                spriteRenderer.color = new Color(1f, 0f, 1f, 1f);
                if (bossHP100 == false)
                {
                    health = 100;
                    updateHP();
                    bossHP100 = true;
                }
            }
        }
        if (health <= 400 && (hasUsedAttack1 == false))
        {
            spriteRenderer2.color = new Color(1f, 0f, 0f, 1f);
            spriteRenderer3.color = new Color(1f, 0f, 0f, 1f);
            ActiveAttackIs1();
        }
        else if (health <= 400 && (hasUsedAttack1 == true) && (attack1IsInactive == true))
        {
            InActiveAttackIs1();
            attack1IsInactive = false;
        }
        if (health <= 300 && hasUsedAttack2 == false)
        {
            ActiveAttackIs2();
            hasUsedAttack2 = true;
        }
        if (health <= 200 & hasUsedAttack3 == false)
        {
            spriteRenderer4.color = new Color(1f, 1f, 1f, 1f);
            ToMiddle();
            attack3Active = true;
            hasUsedAttack3 = true;
        }
        if (health <= 0)
        {
            health = 0;
            hpText.text = health.ToString();
            BossDied();
            Instantiate(death, tankBody.position, tankBody.rotation);
            Debug.Log("Boss" + "lost");
            Object.Destroy(theEntireTank);
            Explode();
        }
        if (hasUsedAttack3 == true)
        {
            Vector2 bossMoving = currentlyMovingTo.position - transform.position;
            transform.Translate(new Vector2(bossMoving.normalized.x * Time.deltaTime * 4, bossMoving.normalized.y * Time.deltaTime * 4));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Taking damage
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health = health - 25 * damageMultiplier;
        }
        if (collision.gameObject.CompareTag("SnipeBullet"))
        {
            health = health - 50 * damageMultiplier;
        }
        if (collision.gameObject.CompareTag("PoisonBullet"))
        {
            health = health - 15 * damageMultiplier;
        }
        if (collision.gameObject.CompareTag("CloseBullet"))
        {
            health = health - 40 * damageMultiplier;
        }
        if (collision.gameObject.CompareTag("SpreadBullet"))
        {
            health = health - 20 * damageMultiplier;
        }
        if (collision.gameObject.CompareTag("DamageBullet"))
        {
            health = health - 60 * damageMultiplier;
        }
        updateHP();
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
            ToRight();
            Object.Destroy(midBlock);
        }
    }

    public void updateHP()
    {
        hpText.text = health.ToString();
        hpBar.value = health;
    }
    public void Explode()
    {
        //Backround Explodes
        animatorBackround.SetBool("Explode", true);
    }
    void ActiveAttackIs1()
    {
        GetComponentInChildren<LazerSpawnAttack>().ActivateAttack();
    }
    void ActiveAttackIs2()
    {
        GameObject.Find("Attack2").GetComponent<FloorActive>().ActivateAtk2();
    }
    public void InActiveAttackIs1()
    {
        hasUsedAttack1 = true;
    }
    void BossDied()
    {
        GameObject.Find("GameManager").GetComponent<nextScene>().BossDead();
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