using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    public weapon weapon;
    public GameObject hitBox;

    // Start is called before the first frame update
    void Start()
    {
        hitBox = Instantiate(weapon.playerHitBox); 
        hitBox.name = "HitBox";
        hitBox.transform.SetParent(this.transform, false);

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Animator>().runtimeAnimatorController = weapon.weaponAnimator;
    }   

    public void AttackMotion1()
    {
        if (weapon.type == weapon.Type.stick)
        {
            this.transform.Find("HitBox").gameObject.SetActive(true);
            this.transform.parent.GetComponent<mousePointer>().isAttackMontion = true;
            this.GetComponent<Animator>().SetBool("isAttackMotion", true);

        }

        else if (weapon.type == weapon.Type.bow)
        {
            this.transform.parent.GetComponent<mousePointer>().isAttackMontion = true;
            this.GetComponent<Animator>().SetBool("isAttackMotion", true);
        }
    }
    public void AttackMotion2()
    { 
        if (weapon.type == weapon.Type.stick) 
        { 
            this.transform.Find("HitBox").gameObject.SetActive(false);
            this.transform.parent.GetComponent<mousePointer>().isAttackMontion = false;
            this.GetComponent<Animator>().SetBool("isAttackMotion", false);
            this.GetComponent<Animator>().SetBool("isAttackCooltime", true);
            StartCoroutine(AttackCooltime());
        }

        else if (weapon.type == weapon.Type.bow)
        {
            this.transform.parent.GetComponent<mousePointer>().isAttackMontion = false;
            this.GetComponent<Animator>().SetBool("isAttackMotion", false);
            this.GetComponent<Animator>().SetBool("isAttackCooltime", true);
            hitBox = Instantiate(weapon.playerHitBox);
            hitBox.name = "Arrow";
            hitBox.transform.SetParent(this.transform, false);
            StartCoroutine(AttackCooltime());
        }

    }

    IEnumerator AttackCooltime()
    {       
        yield return new WaitForSeconds(weapon.attackSpeed);
        this.GetComponent<Animator>().SetBool("isAttackCooltime", false);
        yield break;

    }
}
