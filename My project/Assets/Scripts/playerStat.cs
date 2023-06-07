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
    public weapon tmpWeapon;
    public GameObject weaponController;
    public float dashCooltime;



    // Start is called before the first frame update
    void Start()
    {
        weaponController = this.gameObject.transform.Find("weaponController").GetComponent<weaponController>().gameObject;
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
                            
                            nearObj.GetComponent<getableItems>().itemNumber = weaponController.GetComponent<weaponController>().weapon.weaponNumber;
                            nearObj.GetComponent<SpriteRenderer>().sprite = weaponController.GetComponent<weaponController>().weapon.objectSprite;
                            nearObj.GetComponent<getableItems>().weapon = weaponController.GetComponent<weaponController>().weapon;
                            nearObj.GetComponent<getableItems>().objectName = weaponController.GetComponent<weaponController>().weapon.objectName;
                            nearObj.gameObject.name = nearObj.GetComponent<getableItems>().objectName;
                            nearObj.GetComponent<getableItems>().nameText.text = nearObj.GetComponent<getableItems>().objectName;
                            weaponController.GetComponent<weaponController>().weapon = tmpWeapon;
                            weaponController.GetComponent<weaponController>().weaponChanged = true;
                            GetNearObject();
                            this.gameObject.GetComponent<mousePointer>().isAttackMontion = false;

                            var dir = this.gameObject.GetComponent<mousePointer>().dir;

                            switch (dir)
                            {
                                case 0:
                                    nearObj.transform.position = this.transform.position + new Vector3(0.5f, 0, 0);
                                    break;
                                case 1:
                                    nearObj.transform.position = this.transform.position + new Vector3( 0, 0.5f, 0);
                                    break;
                                case 2:
                                    nearObj.transform.position = this.transform.position + new Vector3(-0.5f, 0, 0);
                                    break;
                                case 3:
                                    nearObj.transform.position = this.transform.position + new Vector3(0, -0.5f, 0);
                                    break;
                            }


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
    public void GetNearObject()
    {     
        tmpItemNumber = nearObj.GetComponent<getableItems>().itemNumber;
        tmpItemSprite = nearObj.GetComponent<SpriteRenderer>().sprite;
        itemType = tmpItemNumber / 100;
        this.tmpWeapon = nearObj.GetComponent<getableItems>().weapon;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("getableItems"))
        {
            nearObj = col.gameObject;
            isItemNear = true;
            GetNearObject();
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("getableItems"))
        {
            //Debug.Log("An item is not near");
            isItemNear = false;
            tmpItemNumber = 0;
            tmpItemSprite = default;
            itemType = 0;
            this.tmpWeapon = default;
            nearObj = default;


        }
       
    }


}
