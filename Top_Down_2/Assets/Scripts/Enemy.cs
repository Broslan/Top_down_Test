using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private int currentHealth;
    [SerializeField] int maximumHealth = 10;
    [SerializeField] float baseMovmentSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WhenCreated()
    {
        currentHealth = maximumHealth;
    }

    public void GetDamage(int damageAmmount, int damageType)
    {
        currentHealth -= damageAmmount;
        if(currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public int GetHealthInfo()
    {
        return currentHealth;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
