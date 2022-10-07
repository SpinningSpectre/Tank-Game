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
    float speed = 20;
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
    //Mouses
    public int Amount = 0;
    public int Normal = 1;
    public int Snipe = 2;
    public int Spread = 3;
    public int Close = 4;
    public int Poison = 5;
    public int Reset = 6;
    public int playerNumber = 1;
    public Text selecAttack;
    bool isActive = false;
    bool allowedToMove = false;
    void Start()
    {

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
                transform.Translate(new Vector2(Input.GetAxis("Player1") * 5 * Time.deltaTime, 0));
            }
            //Different types of bullets
            if (Normal == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    toChoosingAmount();
                    GameObject bullet = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * speed, ForceMode2D.Impulse);
                }
            }
            if (Snipe == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    toChoosingAmount();
                    GameObject bigbullet = Instantiate(bigBulletToFire, firePoint.position, firePoint.rotation);
                    bigbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * snipeSpeed, ForceMode2D.Impulse);
                }
            }
            if (Spread == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    toChoosingAmount();
                    GameObject bullet = Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    GameObject bullet2 = Instantiate(bulletToFire, firePoint2.position, firePoint2.rotation);
                    GameObject bullet3 = Instantiate(bulletToFire, firePoint3.position, firePoint3.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * tripleSpeed, ForceMode2D.Impulse);
                    bullet2.GetComponent<Rigidbody2D>().AddForce(barrelRotator2.up * tripleSpeed, ForceMode2D.Impulse);
                    bullet3.GetComponent<Rigidbody2D>().AddForce(barrelRotator3.up * tripleSpeed, ForceMode2D.Impulse);
                }
            }
            if (Close == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    toChoosingAmount();
                    GameObject biggerbullet = Instantiate(biggerBulletToFire, firePoint.position, firePoint.rotation);
                    biggerbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * closeSpeed, ForceMode2D.Impulse);
                }
            }
            if (Poison == Amount)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    toChoosingAmount();
                    GameObject poisonbullet = Instantiate(poisonBulletToFire, firePoint.position, firePoint.rotation);
                    poisonbullet.GetComponent<Rigidbody2D>().AddForce(barrelRotator.up * speed, ForceMode2D.Impulse);
                }
            }

        }
        //Change turn
        if (Amount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Invoke("ChangeTurn", 0.1f);
            }
        }

        //Test stuff
        if (Input.GetKeyDown(KeyCode.E))
        {
            Amount++;
        }
        if (Reset == Amount)
        {
            Amount = 0;
        }
        selecAttack.text = "Selected  Attack: " + Amount.ToString();
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
    }
    public void toNormalAmount()
    {
        Amount = 1;
        allowedToMove = true;
    }
    public void toSnipeAmount()
    {
        Amount = 2;
        allowedToMove = true;
    }
    public void toSpreadAmount()
    {
        Amount = 3;
        allowedToMove = true;
    }
    public void toBigAmount()
    {
        Amount = 4;
        allowedToMove = true;
    }
    public void toPoisonAmount()
    {
        Amount = 5;
        allowedToMove = true;
    }
}
