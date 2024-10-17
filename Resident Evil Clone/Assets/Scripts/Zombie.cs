using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;


public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform target; //Player transform
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float maxHealth = 5;

    private float currentHealth;
    private int score = 10;

    public static event Action<int> OnZombieDie;

    [SerializeField] public UnityEvent<int> OnZombieDieEvent;

    // public Action<int> zombieDeath;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
        agent.speed = moveSpeed;   

        // zombieDeath += UIManager.instance.UpdateScore(score);
        // OnZombieDie += UIManager.instance.UpdateScore;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
        AudioManager.instance.PlayOneShot(AudioManager.instance.zombieDamage);
        if(currentHealth <= 0){
            // UIManager.zombieDeath?.Invoke(10);
            // zombieDeath?.Invoke(10); //works too, this is part of how events are helpful because the UIManager doesn't need to exist
            OnZombieDieEvent?.Invoke(score); //This is correct but commented to use UnityEvents
            Destroy(gameObject);
        }
    }

    private void OnDestroy(){
        // OnZombieDieEvent -= UIManager.instance.UpdateScore;
    }
    
}
