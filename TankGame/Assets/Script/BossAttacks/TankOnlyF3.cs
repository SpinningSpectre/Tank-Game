using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankOnlyF3 : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 2;
    Animator animator;
    Animator animatorBackround;
    //HP and death
    public float health = 1;
    public float damageMultiplier = 1;
    [SerializeField]
    GameObject death;
    [SerializeField]
    Transform tankBody;
    [SerializeField]
    GameObject theEntireTank;
    void Start()
    {
        animatorBackround = GameObject.Find("Backround").GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        transform.Translate(new Vector2(Input.GetAxis("Player1") * movementSpeed * Time.deltaTime, 0));
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Drive_Forward", true);
            animator.SetBool("Drive_Not", false);
        }
        if (health <= -0)
        {
            health = 0;
            Instantiate(death, tankBody.position, tankBody.rotation);
            BossLose();
            Object.Destroy(theEntireTank);
        }
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
        if (collision.gameObject.CompareTag("Bomb"))
        {
            health = health - 20;
        }
    }
    public void StopWheels()
    {
        animator.SetBool("Drive_Forward", false);
        animator.SetBool("Drive_Not", true);
    }
    public void BossLose()
    {
        GameObject.Find("GameManager").GetComponent<nextScene>().PlayerLose();
    }
}
