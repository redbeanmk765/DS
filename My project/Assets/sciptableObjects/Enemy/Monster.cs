using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]



public class Monster : ScriptableObject
{
    public string name;
    public int maxHp;
    public int level;
    public int damage;
    public Race race;
    public float moveSpeed;
    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;

}
public enum Race
{
    slime
}