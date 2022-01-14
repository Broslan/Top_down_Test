using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    protected float health;
    [SerializeField] protected float maxHealth;

    public void CreatureStart()
    {

    }
}
