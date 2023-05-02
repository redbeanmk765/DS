using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerStat : MonoBehaviour
{
    public int maxHp;
    public int nowHp;
    public int dmg;
    public float atkSpeed = 1;
    public bool attacked = false;
   


    // Start is called before the first frame update
    void Start()
    {
        maxHp = 10;
        nowHp = 10;
        dmg = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
