using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IDamageable
{
    void GetDamage(int damageAmmount, int damageType);
    int GetHealthInfo();
}

interface IWeapon
{

}
