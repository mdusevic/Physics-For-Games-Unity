using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MovementType
{
    MoveRightToLeft,
    MoveLeftToRight,
    MoveForwardToBack,
    MoveBackToForward,
    Idle
};

public class EnemyTarget : MonoBehaviour
{
    public MovementType type = MovementType.Idle;

    Rigidbody rb;
    CharacterController controller;

    public int maxHealth = 100;
    public int health;
    public float timeToColor = 0.5f;
    public float speed = 10.0f;

    public Transform startPos;
    public Transform endPos;
    private Transform target;

    public HealthBar healthBar;
    public GameObject healthCanvas;
    public Material jointsMaterial;
    public Material bodyMaterial;
    public Material damageMaterial;

    private bool healthBarCreated = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        health = maxHealth;
        healthCanvas.SetActive(true);
        target = endPos;
    }

    void Update()
    {
        if (healthBar != null && !healthBarCreated)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBarCreated = true;
        }

        if (!isDead)
        {
            switch (type)
            {
                case MovementType.MoveLeftToRight:
                    if (transform.position.z >= endPos.position.z)
                    {
                        target = startPos;
                    }
                    else if (transform.position.z <= startPos.position.z)
                    {
                        target = endPos;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    transform.LookAt(target.position);
                    break;

                case MovementType.MoveRightToLeft:
                    if (transform.position.z <= endPos.position.z)
                    {
                        target = startPos;
                    }
                    else if (transform.position.z >= startPos.position.z)
                    {
                        target = endPos;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    transform.LookAt(target.position);
                    break;

                case MovementType.MoveForwardToBack:
                    if (transform.position.x <= endPos.position.x)
                    {
                        target = startPos;
                    }
                    else if (transform.position.x >= startPos.position.x)
                    {
                        target = endPos;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    transform.LookAt(target.position);
                    break;

                case MovementType.MoveBackToForward:
                    if (transform.position.x >= endPos.position.x)
                    {
                        target = startPos;
                    }
                    else if (transform.position.x <= startPos.position.x)
                    {
                        target = endPos;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    transform.LookAt(target.position);
                    break;

                default:
                    break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;

            healthBar.SetHealth(health);

            StartCoroutine("ChangeColor");

            if (health <= 0)
            {
                health = 0;
                isDead = true;
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(healthCanvas.gameObject);
        GetComponent<Animator>().enabled = false;
        GetComponent<Ragdoll>().RagdollOn = true;
        Destroy(GetComponent<EnemyTarget>());
        Destroy(gameObject, 4.0f);
    }

    IEnumerator ChangeColor()
    {
        foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            smr.material = damageMaterial;
        }

        yield return new WaitForSeconds(timeToColor);

        foreach (SkinnedMeshRenderer smr in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (smr.gameObject.name == "Beta_HighJointsGeo")
            {
                smr.material = jointsMaterial;
            }

            else
            {
                smr.material = bodyMaterial;
            }
        }
    }
}
