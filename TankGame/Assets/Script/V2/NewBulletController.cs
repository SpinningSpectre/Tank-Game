using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBulletController : MonoBehaviour
{
    public BulletScriptable stats;
    void Start()
    {
        StartCoroutine(KillBulletOverTime());
    }
    void Update()
    {
        Vector3 lookAtPoint = transform.GetComponent<Rigidbody2D>().velocity.normalized;
        lookAtPoint += transform.position;
        transform.LookAt(lookAtPoint, new Vector3(0, 0, -1));
        transform.rotation = new Quaternion(0,0,transform.rotation.z,transform.rotation.w);
    }

    private IEnumerator KillBulletOverTime()
    {
        yield return new WaitForSeconds(stats.deathTime);
        Explode();
    }

    protected virtual void Explode()
    {
        GameObject particle = Instantiate(stats.explosion,transform.position,new Quaternion(0,0,0,0));
        ParticleSystem system = particle.GetComponent<ParticleSystem>();


        ParticleSystem.MainModule main = system.main;
        main.startSpeed = stats.explosionSize;
        main.startColor = new ParticleSystem.MinMaxGradient(stats.explosionColor1,stats.explosionColor2);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        ParticleSystem.EmissionModule trail = transform.Find("ParticleTrail").GetComponent<ParticleSystem>().emission;
        trail.enabled = false;

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 1.5f * stats.explosionSize);

        for(int i = 0; i < col.Length; i++)
        {
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
            if (col[i].gameObject.GetComponent<NewTankController>() != null)
            {
                col[i].gameObject.GetComponent<NewTankController>().DoDamage(stats.damage / 2);
            }
        }
        Destroy(gameObject,2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Break")
        {
            Explode();
        }
    }
}
