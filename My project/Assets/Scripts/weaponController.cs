using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    public weapon weapon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }   

    public void AttackMotion1()
    {
        if (weapon.type == weapon.Type.stick)
        {
            this.transform.Find("HitBox").gameObject.SetActive(true);
            this.transform.parent.GetComponent<mousePointer>().isAttackMontion = true;
            this.GetComponent<Animator>().SetBool("isAttackMotion", false);
        }
    }
    public void AttackMotion2()
    { 
        if (weapon.type == weapon.Type.stick) 
        { 
            this.transform.Find("HitBox").gameObject.SetActive(false);
            this.transform.parent.GetComponent<mousePointer>().isAttackMontion = false;
            this.GetComponent<Animator>().SetBool("isAttackMotion", true);
        }

    }
}
