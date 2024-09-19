using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform target; //Player transform
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int maxHealth = 5;

    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
        agent.speed = moveSpeed;   

        Debug.Log("Current starting health = " + currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if(currentHealth <= 0){
            Destroy(gameObject);
        }
    }
}
