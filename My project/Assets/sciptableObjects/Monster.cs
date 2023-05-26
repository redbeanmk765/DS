using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]



public class Monster : ScriptableObject
{
    public string name;
    public int health;
    public int level;
    public int damage;
    public Race race;
   
}
public enum Race
{
    slime
}