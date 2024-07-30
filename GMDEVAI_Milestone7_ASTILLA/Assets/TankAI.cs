using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject turret;
    public float currEnemyHP;
    public float maxEnemyHP;
    public float bulletDmg;

    public GameObject GetPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currEnemyHP = maxEnemyHP;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));

        DoEnemyDeath();

        if(currEnemyHP <= (maxEnemyHP * 0.2f))
        {
            anim.SetBool("isFlee", true);
        }
    }

    void Fire()
    {
        GameObject b = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    void DoEnemyDeath()
    {
        if(this.currEnemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Shell"))
        {
            currEnemyHP -= bulletDmg;
        }
    }
}
