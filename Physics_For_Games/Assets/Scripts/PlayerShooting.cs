using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damage = 10;
    public float range = 100.0f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            EnemyTarget target = hit.transform.root.GetComponent<EnemyTarget>();

            if (target != null)
            {
                switch(hit.transform.name)
                {
                    case "Head":
                        target.TakeDamage(damage * 2);
                        break;

                    case "Spine2":
                        target.TakeDamage(damage * 2);
                        break;

                    case "LeftUpLeg":
                        target.TakeDamage(damage + 2);
                        break;
                    
                    case "LeftLeg":
                        target.TakeDamage(damage + 2);
                        break;

                    case "RightUpLeg":
                        target.TakeDamage(damage + 2);
                        break;

                    case "RightLeg":
                        target.TakeDamage(damage + 2);
                        break;

                    case "LeftArm":
                        target.TakeDamage(damage + 2);
                        break;

                    case "LeftForeArm":
                        target.TakeDamage(damage - 4);
                        break;

                    case "RightArm":
                        target.TakeDamage(damage + 2);
                        break;

                    case "RightForeArm":
                        target.TakeDamage(damage - 4);
                        break;

                    default:
                        target.TakeDamage(damage);
                        break;
                }
            }
        }
    }
}
