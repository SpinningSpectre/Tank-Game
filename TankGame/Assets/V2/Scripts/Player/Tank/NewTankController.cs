using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTankController : MonoBehaviour
{
    public BulletScriptable currentBullet;
    [SerializeField] private Transform firePoint;

    [Header("Health")]
    public float currentHealth = 100;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float maxHealth = 150;

    [Header("Turns")]
    [SerializeField] private bool canShoot = true;

    public int playerNumber = 0;
    private void Start()
    {
        currentHealth = startHealth;
        if(firePoint == null)
        {
            firePoint = transform.Find("Firepoint");
        }
        for(int i = 0; i < 10; i++)
        {
            //print(firePoint.rotation.eulerAngles.z + (i * 5));

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    public void FireBullet()
    {
        BulletScriptable scrip = currentBullet;
        GameObject bullet = Instantiate(
            currentBullet.bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        NewBulletController controller = bullet.GetComponent<NewBulletController>();
        controller.stats = currentBullet;
        Rigidbody2D rigidB = bullet.GetComponent<Rigidbody2D>();
        rigidB.AddForce(firePoint.up * scrip.speed, ForceMode2D.Impulse);
        bullet.transform.Find("ParticleTrail").gameObject.SetActive(true);
        DoDamage(-scrip.selfHealing);
    }

    public void DoDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            print("You ded");
        }else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
