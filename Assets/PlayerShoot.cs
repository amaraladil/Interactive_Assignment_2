using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform camera = null;
    float range = 100f; // for now
    public GameObject BulletHoldDecal;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }

    }

    private void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;

        LayerMask wall = LayerMask.GetMask("Structure");
        LayerMask enemy = LayerMask.GetMask("Enemy");

        if (Physics.Raycast(camera.position, camera.forward, out hit, range, enemy))
        {
            Debug.Log("Shot the following enemy: " + hit.collider.name);
            Health enemyHealth = hit.collider.GetComponent<Health>();
            enemyHealth.TakeDamage(10);
            if (enemyHealth.isDead)
            {
                hit.collider.gameObject.SetActive(false);
                UImanager.instance.killCount++;
                UImanager.instance.UpdateKillCounterUI();
            }
        }
        else if (Physics.Raycast(camera.position, camera.forward, out hit, range, wall))
        {
            Debug.Log("Shot the following wall: " + hit.collider.name);

            // display a bullet hole decal
            GameObject impactHole = Instantiate(BulletHoldDecal,
                                                hit.point + (0.01f * hit.normal),
                                                Quaternion.LookRotation(-1 * hit.normal, hit.transform.up));
            Destroy(impactHole, 7f);
        }

        // animate the gun
        //gunAnimator.SetTrigger("Fire");
    }
}
