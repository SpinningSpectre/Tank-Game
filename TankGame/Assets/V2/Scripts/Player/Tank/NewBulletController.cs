using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBulletController : MonoBehaviour
{
    public BulletScriptable stats;
    void Start()
    {
        //When the bullet spawns, kill it after stats.deathtime seconds
        StartCoroutine(KillBulletOverTime());
    }
    void Update()
    {
        //Makes the bullet look where its going
        Vector3 lookAtPoint = transform.GetComponent<Rigidbody2D>().velocity.normalized;
        lookAtPoint += transform.position;
        transform.LookAt(lookAtPoint, new Vector3(0, 0, -1));
        transform.rotation = new Quaternion(0,0,transform.rotation.z,transform.rotation.w);
    }

    private IEnumerator KillBulletOverTime()
    {
        yield return new WaitForSeconds(stats.deathTime);
        Explode(null);
    }

    protected virtual void Explode(Collision2D collider)
    {
        //Spawns a particle
        GameObject particle = Instantiate(stats.explosion,transform.position,new Quaternion(0,0,0,0));
        ParticleSystem system = particle.GetComponent<ParticleSystem>();

        //Gives it the stats that match with the scriptable
        ParticleSystem.MainModule main = system.main;
        main.startSpeed = stats.explosionSize;
        main.startColor = new ParticleSystem.MinMaxGradient(stats.explosionColor1,stats.explosionColor2);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        ParticleSystem.EmissionModule trail = transform.Find("ParticleTrail").GetComponent<ParticleSystem>().emission;
        trail.enabled = false;

        //Deals direct damage if it hit a tank directly
        if(collider.gameObject.GetComponent<NewTankController>() != null)
        {
            collider.gameObject.GetComponent<NewTankController>().DoDamage(stats.directDamage);
        }

        //Checks all colliders in the splash area
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 1.5f * stats.explosionSize);

        for(int i = 0; i < col.Length; i++)
        {
            //Deals knockback if it can
            if (col[i].gameObject.GetComponent<Rigidbody2D>() != null && stats.doKnockback)
            {
                bool doesntGetKnockback = false;
                NewBulletController contr;
                if(col[i].gameObject.TryGetComponent(out contr))
                {
                    for(int b = 0; b < stats.doesntKnockback.Count; b++)
                    {
                        if(contr.stats == stats.doesntKnockback[b])
                        {
                            doesntGetKnockback = true;
                        }
                    }
                }
                if (!doesntGetKnockback)
                {
                    col[i].gameObject.GetComponent<Rigidbody2D>().AddForce((col[i].transform.position - transform.position) * 300);
                }
            }

            //Deals damage if its a tank and isnt the main collider it hit
            if (col[i].gameObject.GetComponent<NewTankController>() != null && col[i].gameObject != collider.gameObject)
            {
                col[i].gameObject.GetComponent<NewTankController>().DoDamage(stats.splashDamage);
            }
        }

        //Keeps the object alive for 2 more seconds so it can finish its particle
        Destroy(gameObject,2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Temporary
        if(Attributes.HasAttribute(collision.gameObject,"BreakBullet"))
        {

            Explode(collision);
        }
    }
}
