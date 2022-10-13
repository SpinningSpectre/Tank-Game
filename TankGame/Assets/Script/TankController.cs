using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    //Mouses
    public int Amount = 0;
    public int Normal = 1;
    public int Snipe = 2;
    public int Spread = 3;
    public int Close = 4;
    public int Poison = 5;
    public int Firework = 6;
    public int Damage = 7;
    public int playerNumber = 1;
    public Text selecAttack;
    public Text HPText;
    public Text MoveTime;
    bool isActive = false;
    bool allowedToMove = false;
    Animator animator;
    //HP and death
    public int Health = 100;
    [SerializeField]
    GameObject Death;
    [SerializeField]
    Transform tankBody;
    [SerializeField]
    GameObject theEntireTank;
    public float timeToMove = 3;
    [SerializeField]
    Transform upgrades;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (isActive == true)
        {
            //Rotation And Movement
            if (allowedToMove == true)
            {
                barrelRotator.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * Time.deltaTime);
                barrelRotator2.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * Time.deltaTime);
                barrelRotator3.RotateAround(Vector3.forward, Input.GetAxis("Vertical") * Time.deltaTime);
                if (timeToMove >= 0)
                {
                    transform.Translate(new Vector2(Input.GetAxis("Player1") * movementSpeed * Time.deltaTime, 0));
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                    {
                        animator.SetBool("Drive_Forward", true);
                        animator.SetBool("Drive_Not", false);
                        timeToMove -= Time.deltaTime;
                        MoveTime.text =  "Move Time: " + timeToMove.ToString();
                    }
                    else
                    {
                        animator.SetBool("Drive_Forward", false);
                        animator.SetBool("Drive_Not", true);
                    }
                }
            }
            //Different types of bullets
            if (Normal == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    GameObject bullet = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed, ForceMode2D.Impulse);
                }
            }
            if (Snipe == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    GameObject bigbullet = Instantiate(bigBulletToFire, firePoint.position, firePoint.rotation);
                    bigbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * snipeSpeed, ForceMode2D.Impulse);
                }
            }
            if (Spread == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    GameObject bullet = Instantiate(spreadBulletToFire, firePoint.position, firePoint.rotation);
                    GameObject bullet2 = Instantiate(spreadBulletToFire, firePoint2.position, firePoint2.rotation);
                    GameObject bullet3 = Instantiate(spreadBulletToFire, firePoint3.position, firePoint3.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * tripleSpeed, ForceMode2D.Impulse);
                    bullet2.GetComponent<Rigidbody2D>().AddForce(barrelRotator2.up * tripleSpeed, ForceMode2D.Impulse);
                    bullet3.GetComponent<Rigidbody2D>().AddForce(barrelRotator3.up * tripleSpeed, ForceMode2D.Impulse);
                }
            }
            if (Close == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    GameObject biggerbullet = Instantiate(biggerBulletToFire, firePoint.position, firePoint.rotation);
                    biggerbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * closeSpeed, ForceMode2D.Impulse);
                }
            }
            if (Poison == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Health = Health + 10;
                    HPText.text = "HP" + playerNumber + ":" + Health.ToString();
                    GameObject poisonbullet = Instantiate(poisonBulletToFire, firePoint.position, firePoint.rotation);
                    poisonbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed, ForceMode2D.Impulse);
                }
            }
            if (Firework == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    GameObject Firework = Instantiate(FireworkToFire, firePoint.position, firePoint.rotation);
                    Firework.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed, ForceMode2D.Impulse);
                }
            }
            if (Damage == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Health = Health - 10;
                    HPText.text = "HP" + playerNumber + ":" + Health.ToString();
                    GameObject damageBullet = Instantiate(damageBulletToFire, firePoint.position, firePoint.rotation);
                    damageBullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * bulletSpeed, ForceMode2D.Impulse);
                }
            }
            if (Health >= 150)
            {
                Health = 150;
                updateHP();
            }

        }
        if (isActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                toChoosingAmount();
            }
        }
        if (Health <= 0)
        {
            Health = 0;
            HPText.text =Health + " Bitches".ToString();
            Instantiate(Death, tankBody.position, tankBody.rotation);
            Object.Destroy(theEntireTank);
        }
        //Change turn
        if (Amount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Invoke("ChangeTurn", 0.1f);
            }
        }
    }
    public void defaultMoveTime()
    {
        timeToMove = 3;
        MoveTime.text = "Seconds you can move: " + timeToMove.ToString();
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
        Amount = 0;
        allowedToMove = false;
        animator.SetBool("Drive_Forward", false);
        animator.SetBool("Drive_Not", true);
    }
    public void toNormalAmount()
    {
        Amount = 1;
        allowedToMove = true;
        defaultMoveTime();
    }
    public void toSnipeAmount()
    {
        Amount = 2;
        allowedToMove = true;
        defaultMoveTime();
    }
    public void toSpreadAmount()
    {
        Amount = 3;
        allowedToMove = true;
        defaultMoveTime();
    }
    public void toBigAmount()
    {
        Amount = 4;
        allowedToMove = true;
        defaultMoveTime();
    }
    public void toPoisonAmount()
    {
        Amount = 5;
        allowedToMove = true;
        defaultMoveTime();
    }
    public void toFireworkAmount()
    {
        Amount = 6;
        allowedToMove = true;
        defaultMoveTime();
    }
    public void toDamageAmount()
    {
        Amount = 7;
        allowedToMove = true;
        defaultMoveTime();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Health = Health - 25;
        }
        if (collision.gameObject.CompareTag("SnipeBullet"))
        {
            Health = Health - 50;
        }
        if (collision.gameObject.CompareTag("PoisonBullet"))
        {
            Health = Health - 15;
        }
        if (collision.gameObject.CompareTag("CloseBullet"))
        {
            Health = Health - 40;
        }
        if (collision.gameObject.CompareTag("SpreadBullet"))
        {
            Health = Health - 20;
        }
        if (collision.gameObject.CompareTag("DamageBullet"))
        {
            Health = Health - 60;
        }
        updateHP();
    }
    public void updateHP()
    {
        HPText.text = "HP" + playerNumber + ":" + Health.ToString();
    }

}