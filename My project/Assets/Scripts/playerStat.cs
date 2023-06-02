using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class playerStat : MonoBehaviour
{
    public Material originalMaterial;
    public Material flashMaterial;
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
    public float damaged;
    public float cure = 0;
    public int dmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    public bool playerDie = false;
    public bool onFlash = false;
    public int itemType;



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
        if (this.damaged != 0)
        {
            if (!onFlash)
            {
                nowHp = nowHp - damaged;
                onFlash = true;
                StartCoroutine(FlashWhite());
            }
            this.damaged = 0;
            
            
        }
        nowHp = nowHp + cure;
        

        if (nowHp <= 0)
        {
            playerDie = true;
            player.GetComponent<Animator>().SetBool("isDie", true);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }

        cure = 0;
        damaged = 0;

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
                switch (itemType)
                {

                    case 1:
                    {

                            break;
                    }

                    case 3:
                    { 
                        if (item1Number == 0)
                        {
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
                         break;
                    }
                }
                
            }

        }

    }
    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            this.GetComponent<SpriteRenderer>().material = this.flashMaterial;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<SpriteRenderer>().material = this.originalMaterial;
            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i <= 4; i++)
            {
             
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSeconds(0.1f);
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.2f);
            }

            this.GetComponent<SpriteRenderer>().material = this.originalMaterial;




            if (onFlash == false)
            {
                yield break;
            }
            onFlash = false;


        }
        if (onFlash == false)
        {
            yield break;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        nearObj = col.gameObject;
        if (col.CompareTag("getableItems"))
        {
            //Debug.Log("An item is near");
            isItemNear = true;
            tmpItemNumber = col.GetComponent<getableItems>().itemNumber;
            tmpItemSprite = col.GetComponent<SpriteRenderer>().sprite;
            itemType = tmpItemNumber / 100;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("getableItems"))
        {
            //Debug.Log("An item is not near");
            isItemNear = false;


        }
    }


}
