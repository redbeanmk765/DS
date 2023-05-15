using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class playerStat : MonoBehaviour
{
    public int tmpItemNumber = 0;
    public Sprite tmpItemSprite;

    public bool isItemNear;

    public int item1Number = 0;
    public int item2Number = 0;
    public int item3Number = 0;
    public Image item1;
    public Image item2;
    public Image item3;
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
                }

                else if (item2Number == 0)
                {
                    item2Number = tmpItemNumber;
                    item2.sprite = tmpItemSprite;
                    item2.gameObject.SetActive(true);
                }

                else if (item3Number == 0)
                {
                    item3Number = tmpItemNumber;
                    item3.sprite = tmpItemSprite;
                    item3.gameObject.SetActive(true);
                }

                else
                { }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {        
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
