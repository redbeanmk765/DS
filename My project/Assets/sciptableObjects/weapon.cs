using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]

public class weapon : ScriptableObject
{
    public int dmg;
    [SerializeField] public GameObject playerWeapon;
    public Type type;

    public enum Type
    {
        stick
    }


   
}
