using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3.0f;
    public float radius = 5.0f;
    public float upforce = 1.0f;
    public float force = 200.0f;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        // Show effect
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, upforce, ForceMode.Impulse);
            }

            EnemyTarget nearbyTarget = nearbyObject.transform.root.gameObject.GetComponent<EnemyTarget>();
            if (nearbyTarget != null)
            {
                float proximity = (transform.position - nearbyTarget.transform.position).magnitude;
                int explosiveEffect = 1 - (int)(proximity / radius);

                nearbyTarget.TakeDamage(explosiveEffect * 7);
            }
        }

        // Remove grenade
        Destroy(gameObject);
        Destroy(explosion, 2.5f);
    }
}
