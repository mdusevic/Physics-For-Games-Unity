using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MovementType
{
    MoveLefttoRight,
    Idle
};

public class EnemyTarget : MonoBehaviour
{
    public MovementType type;
    private Vector3 startPos;
    public Vector3 endPos;
    public float endPoint;

    public Transform endTarget;

    Rigidbody rb;
    CharacterController controller;

    public int maxHealth = 100;
    public int health;
    public float timeToColor = 0.5f;
    public float speed = 10.0f;

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
        startPos = transform.position;
    }

    void Update()
    {
        if (healthBar != null && !healthBarCreated)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBarCreated = true;
        }

        if (type == MovementType.MoveLefttoRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endTarget.position, speed * Time.deltaTime);

            //if (transform.position.z > endPos.z)
            //{
            //    controller.Move(Vector3.back * speed * Time.deltaTime);
            //}
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
