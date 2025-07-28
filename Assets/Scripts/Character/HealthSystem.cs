using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Player, Enemy, Nature, None
}

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Faction faction;
    public Faction Faction => faction;

    [SerializeField] private int maxHealth = 100;
    private int health;

    public event Action OnDamage;
    public event Action OnDie;

    public bool IsDead
    {
        get
        {
            if (gameObject.activeSelf)
                return health == 0;
            else 
                return true;
        }
    }

    void Start()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        health = (int)MathF.Max(health - damage, 0);

        if(health == 0)
        {
            OnDie?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }

        Debug.Log($"{name} HP : {health} / {maxHealth}");
    }
}
