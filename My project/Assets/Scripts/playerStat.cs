using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class playerStat : MonoBehaviour
{
    public int tmpItemNumber = 0;
    public Sprite tmpItemSprite;
    public GameObject nearObj;
    public GameObject player;

    public bool isItemNear;
    public bool isItemGot = false;
    public int item1Number = 0;
    public int item2Number = 0;
    public int item3Number = 0;
    public Image item1;
    public Image item2;
    public Image item3;
    public Image PlayerHpBar;
    public float maxHp;
    public float nowHp;
    public float damage;
    public float cure = 0;
    public int dmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    public bool playerDie = false;



    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100;
        nowHp = maxHp / 2;
        dmg = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        nowHp = nowHp + cure;
        nowHp = nowHp - damage;

        if (nowHp <= 0)
        {
            playerDie = true;
            player.GetComponent<Animator>().SetBool("isDie", true);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }

        cure = 0;
        damage = 0;

        if(nowHp > maxHp)
        {
            nowHp = maxHp;
        }

        float hpRatio = nowHp / maxHp;
        PlayerHpBar.fillAmount = Mathf.Lerp(PlayerHpBar.fillAmount, hpRatio, Time.deltaTime * 10);
        //Debug.Log(hpRatio);


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isItemNear)
            {
                Debug.Log("pressed E");
                if (item1Number == 0)
                {
                    Debug.Log("Item1 is got");
                    item1Number = tmpItemNumber;
                    item1.sprite = tmpItemSprite;
                    item1.gameObject.SetActive(true);
                    isItemGot = true;
                }

                else if (item2Number == 0)
                {
                    item2Number = tmpItemNumber;
                    item2.sprite = tmpItemSprite;
                    item2.gameObject.SetActive(true);
                    isItemGot = true;
                }

                else if (item3Number == 0)
                {
                    item3Number = tmpItemNumber;
                    item3.sprite = tmpItemSprite;
                    item3.gameObject.SetActive(true);
                    isItemGot = true;
                }

                else
                { }

                if (isItemGot == true)
                {
                    Destroy(nearObj);
                    isItemGot = false;
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        nearObj = col.gameObject;
        if (col.CompareTag("getableItems"))
        {
            Debug.Log("An item is near");
            isItemNear = true;
            tmpItemNumber = col.GetComponent<getableItems>().itemNumber;
            tmpItemSprite = col.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("getableItems"))
        {
            Debug.Log("An item is not near");
            isItemNear = false;


        }
    }


}
