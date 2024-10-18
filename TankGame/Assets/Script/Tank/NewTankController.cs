using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTankController : MonoBehaviour
{
    [SerializeField] private BulletScriptable[] selectedBullets = new BulletScriptable[4];
    [SerializeField] private Transform firePoint;

    [Header("Health")]
    private float currentHealth = 100;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float maxHealth = 150;

    [Header("Turns")]
    [SerializeField] private bool currentTurn = false;
    [SerializeField] private bool canShoot = true;

    public int numb = 0;
    private void Start()
    {
        currentHealth = startHealth;
        if(firePoint == null)
        {
            firePoint = transform.Find("Firepoint");
        }
        for(int i = 0; i < 10; i++)
        {
            print(firePoint.rotation.eulerAngles.z + (i * 5));

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            FireBullet();
        }
        if (Input.GetKey(KeyCode.V))
        {
            FireBullet();
        }
        if (Input.GetKey(KeyCode.B))
        {
            FireBullet(numb);
            numb++;
            if(numb > 3)
            {
                numb = 0;
            }
        }
    }

    public void FireBullet(int type = 0)
    {
        BulletScriptable scrip = selectedBullets[type];
        GameObject bullet = Instantiate(
            selectedBullets[type].bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        NewBulletController controller = bullet.GetComponent<NewBulletController>();
        controller.stats = selectedBullets[type];
        Rigidbody2D rigidB = bullet.GetComponent<Rigidbody2D>();
        rigidB.AddForce(firePoint.up * scrip.speed, ForceMode2D.Impulse);
        bullet.transform.Find("ParticleTrail").gameObject.SetActive(true);
    }

    public void DoDamage(float amount)
    {
        currentHealth -= amount;
        //print(currentHealth);
        if (currentHealth < 0)
        {
            print("You ded");
        }else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
