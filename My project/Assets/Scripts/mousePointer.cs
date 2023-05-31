using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePointer : MonoBehaviour
{
    float angle;
    float angleForWeapon;
    Vector2 target, mouse;
    Animator animator;
    Animator weaponAnimator;
    int dir;
    [SerializeField] Transform player;
    [SerializeField] Transform weapon;
    public bool isAttackMontion;
    private void Start()
    {
        animator = GetComponent<Animator>();
        weaponAnimator = weapon.GetComponent<Animator>();
        isAttackMontion = false;
    }
    private void Update()
    {
        if (isAttackMontion == false)
        {
            target = player.transform.position;
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
            //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            angleForWeapon = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) + 1.5708f;

            if (angle >= -45 && angle < 45)
            {
                dir = 0;
                animator.SetFloat("angle", 0);

                if (weapon.transform.localScale.y < 0)
                {
                    weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y * -1, weapon.transform.localScale.z);
                }

            }
            else if (angle >= 45 && angle < 135)
            {
                dir = 90;
                animator.SetFloat("angle", 0.33f);

                if (weapon.transform.localScale.y < 0)
                {
                    weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y * -1, weapon.transform.localScale.z);
                }
            }
            else if (angle >= 135 || angle < -135)
            {
                dir = 180;
                animator.SetFloat("angle", 0.66f);

                if (weapon.transform.localScale.y > 0)
                {
                    weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y * -1, weapon.transform.localScale.z);
                }
            }
            else if (angle >= -135 && angle < -45)
            {
                dir = -90;
                animator.SetFloat("angle", 1);

                if (weapon.transform.localScale.y < 0)
                {
                    weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, weapon.transform.localScale.y * -1, weapon.transform.localScale.z);
                }
            }
            //this.transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);

            var x = 0.6f * Mathf.Sin(angleForWeapon);
            var y = 0.6f * Mathf.Cos(angleForWeapon);

            weapon.transform.position = transform.position + new Vector3(x, -y);
            weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        if (Input.GetMouseButtonDown(0))
        {
            weaponAnimator.SetBool("onClick", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            weaponAnimator.SetBool("onClick", false);
        }
        

    }
    


}
