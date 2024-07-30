using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject turret;
    public float bulletDmg;
    public float playerHP;
    public GameObject loseTxt;

    // Update is called once per frame
    void Update()
    {
        PlayerFire();

        DoPlayerDeath();
    }

    void PlayerFire()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            GameObject a = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation);
            a.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
        }
    }

    void DoPlayerDeath()
    {
        if (playerHP <= 0)
        {
            Destroy(this.gameObject);
            Time.timeScale = 0f;
            loseTxt.SetActive(true);
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Shell"))
        {
            playerHP -= bulletDmg;
        }
    }
}
