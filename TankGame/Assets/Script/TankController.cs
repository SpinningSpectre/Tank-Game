using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    //Rotations
    [SerializeField]
    Transform barrelRotator;
    [SerializeField]
    Transform barrelRotator2;
    [SerializeField]
    Transform barrelRotator3;
    //Firepoints
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    Transform firePoint2;
    [SerializeField]
    Transform firePoint3;
    //Speeds
    [SerializeField]
    float movementSpeed = 2;
    [SerializeField]
    float bulletSpeed = 20;
    [SerializeField]
    float tripleSpeed = 15;
    [SerializeField]
    float snipeSpeed = 50;
    [SerializeField]
    float closeSpeed = 10;
    //Types of Bullets
    [SerializeField]
    GameObject bulletToFire;
    [SerializeField]
    GameObject bigBulletToFire;
    [SerializeField]
    GameObject biggerBulletToFire;
    [SerializeField]
    GameObject poisonBulletToFire;
    [SerializeField]
    GameObject FireworkToFire;
    [SerializeField]
    GameObject spreadBulletToFire;
    [SerializeField]
    GameObject damageBulletToFire;
    //Upgrades
    public int upgrade = 0;
    public int normal = 1;
    public int snipe = 2;
    public int spread = 3;
    public int close = 4;
    public int poison = 5;
    public int firework = 6;
    public int damage = 7;
    public int playerNumber = 1;
    public Text hpText;
    public Text moveTime;
    bool isActive = false;
    bool allowedToMove = false;
    bool allowedToSchoot = false;
    [SerializeField]
    int Boss = 0;
    bool inBossEvent = false;
    Animator animator;
    Animator animatorBackround;
    //HP and death
    public float health = 100;
    public float damageMultiplier = 1;
    [SerializeField]
    GameObject death;
    [SerializeField]
    Transform tankBody;
    [SerializeField]
    GameObject theEntireTank;
    public float timeToMove = 3;
    [SerializeField]
    Transform upgrades;
    public Slider hpBar;
    public Slider schootSliderSpeed;
    void Start()
    {
        animatorBackround = GameObject.Find("Backround").GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
        inBossEvent = false;
        if (Boss == 1)
        {
            inBossEvent = true; 
        }
    }
    void Update()
    {
        if (isActive == true || (inBossEvent == true))
        {
            //Rotation And Movement
            if (allowedToMove == true || (inBossEvent == true))
            {
                if (timeToMove >= 0 || (inBossEvent == true))
                {
                    if (playerNumber == 1)
                    {
                        transform.Translate(new Vector2(Input.GetAxis("Player1") * movementSpeed * Time.deltaTime, 0));
                        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                        {
                            animator.SetBool("Drive_Forward", true);
                            animator.SetBool("Drive_Not", false);
                            timeToMove -= Time.deltaTime;
                            moveTime.text = timeToMove.ToString("F0");
                        }
                        else
                        {
                            StopWheels();
                        }
                    } else if (playerNumber == 2)
                    {
                        transform.Translate(new Vector2(Input.GetAxis("Player2") * movementSpeed * Time.deltaTime, 0));
                        if (Input.GetKey(KeyCode.H) || Input.GetKey(KeyCode.K))
                        {
                            animator.SetBool("Drive_Forward", true);
                            animator.SetBool("Drive_Not", false);
                            timeToMove -= Time.deltaTime;
                            moveTime.text = timeToMove.ToString("F0");
                        }
                        else
                        {
                            StopWheels();
                        }
                    }
                }
                if (playerNumber == 1)
                {
                    barrelRotator.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * Time.deltaTime);
                    barrelRotator2.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * Time.deltaTime);
                    barrelRotator3.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * Time.deltaTime);
                } else if (playerNumber == 2)
                {
                    barrelRotator.RotateAround(Vector3.forward, Input.GetAxis("Vertical2") * Time.deltaTime);
                    barrelRotator2.RotateAround(Vector3.forward, Input.GetAxis("Vertical2") * Time.deltaTime);
                    barrelRotator3.RotateAround(Vector3.forward, Input.GetAxis("Vertical2") * Time.deltaTime);
                }
            }
            if (allowedToSchoot == true)
            {
                if (normal == upgrade)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        GameObject bullet = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        bullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                        StopWheels();
                    }
                }
                if (snipe == upgrade)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        GameObject bigbullet = Instantiate(bigBulletToFire, firePoint.position, firePoint.rotation);
                        bigbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * snipeSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                    }
                }
                if (spread == upgrade)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        GameObject bullet = Instantiate(spreadBulletToFire, firePoint.position, firePoint.rotation);
                        GameObject bullet2 = Instantiate(spreadBulletToFire, firePoint2.position, firePoint2.rotation);
                        GameObject bullet3 = Instantiate(spreadBulletToFire, firePoint3.position, firePoint3.rotation);
                        bullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * tripleSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                        bullet2.GetComponent<Rigidbody2D>().AddForce(barrelRotator2.up * tripleSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                        bullet3.GetComponent<Rigidbody2D>().AddForce(barrelRotator3.up * tripleSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                    }
                }
                if (close == upgrade)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        GameObject biggerbullet = Instantiate(biggerBulletToFire, firePoint.position, firePoint.rotation);
                        biggerbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * closeSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                    }
                }
                if (poison == upgrade)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        health += 10;
                        updateHP();
                        GameObject poisonbullet = Instantiate(poisonBulletToFire, firePoint.position, firePoint.rotation);
                        poisonbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                    }
                }
                if (firework == upgrade)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        GameObject Firework = Instantiate(FireworkToFire, firePoint.position, firePoint.rotation);
                        Firework.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                    }
                }
                if (damage == upgrade)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        health = health - 10;
                        updateHP();
                        GameObject damageBullet = Instantiate(damageBulletToFire, firePoint.position, firePoint.rotation);
                        damageBullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed * schootSliderSpeed.value, ForceMode2D.Impulse);
                    }
                }
            }
        }
        if (health >= 150)
        {
            health = 150;
            updateHP();
        }
        if (isActive == false)
        {
            allowedToSchoot = false;
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                toChoosingAmount();
            }
        }
        if (health <= 0)
        {
            Explode();
            health = 0;
            hpText.text = health.ToString();
            Instantiate(death, tankBody.position, tankBody.rotation);
            Debug.Log("tank"+playerNumber + "lost");
            Object.Destroy(theEntireTank);
        }
        //Change turn
        if (upgrade >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Invoke("ChangeTurn", 0.1f);
                StopWheels();
                MoveSlider();
            }
        }
        if (timeToMove <= 0)
        {
            StopWheels();
        }
        if (health <= 0 && (inBossEvent == false))
        {
            if (playerNumber == 1)
            {
                P1Lose();
            } else if (playerNumber == 2)
            {
                P2Lose();
            }
        } else if (health <= 0 && (inBossEvent == true))
        {
            BossLose();
        }
    }
    public void DefaultMoveTime()
    {
        timeToMove = 3;
        moveTime.text = timeToMove.ToString();
    }
    //Change Turns?
    void ChangeTurn()
    {
        GameObject.Find("GameManager").GetComponent<TurnManager>().ChangeTurn();
    }
    public void IsActive(bool a)
    {
        if (a == true)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
    //Button Bullets + Movement
    public void toChoosingAmount()
    {
        upgrade = 0;
        allowedToMove = false;
        StopWheels();
    }
    public void toNormalAmount()
    {
        upgrade = 1;
        allowedToMove = true;
        allowedToSchoot = true;
        DefaultMoveTime();
    }
    public void toSnipeAmount()
    {
        upgrade = 2;
        allowedToMove = true;
        allowedToSchoot = true;
        DefaultMoveTime();
    }
    public void toSpreadAmount()
    {
        upgrade = 3;
        allowedToMove = true;
        allowedToSchoot = true;
        DefaultMoveTime();
    }
    public void toBigAmount()
    {
        upgrade = 4;
        allowedToMove = true;
        allowedToSchoot = true;
        DefaultMoveTime();
    }
    public void toPoisonAmount()
    {
        upgrade = 5;
        allowedToMove = true;
        allowedToSchoot = true;
        DefaultMoveTime();
    }
    public void toFireworkAmount()
    {
        upgrade = 6;
        allowedToMove = true;
        allowedToSchoot = true;
        DefaultMoveTime();
    }
    public void toDamageAmount()
    {
        upgrade = 7;
        allowedToMove = true;
        allowedToSchoot = true;
        DefaultMoveTime();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        if (collision.gameObject.CompareTag("Lazer"))
        {
            health = 0;
        }
        updateHP();
    }
    public void updateHP()
    {
        hpText.text = health.ToString();
        hpBar.value = health;
    }
    public void StopWheels()
    {
        animator.SetBool("Drive_Forward", false);
        animator.SetBool("Drive_Not", true);
    }
    public void Explode()
    {
        animatorBackround.SetBool("Explode", true);
    }
    public void MoveSlider()
    {
        schootSliderSpeed.transform.position = new Vector2(upgrades.position.x, upgrades.position.y);
    }
    public void BossEventActive()
    {
        inBossEvent = true;
    }
    public void BossEventInActive()
    {
        inBossEvent = false;
    }
    public void BossLose()
    {
        GameObject.Find("GameManager").GetComponent<nextScene>().PlayerLose();
    }
    public void P1Lose()
    {
        GameObject.Find("GameManager").GetComponent<nextScene>().P1Dead();
    }
    public void P2Lose()
    {
        GameObject.Find("GameManager").GetComponent<nextScene>().P2Dead();
    }
}