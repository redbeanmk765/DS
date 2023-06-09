using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomManager : MonoBehaviour
{
    public GameObject Bar;
    public Image BossHpBar;
    public GameObject boss;
    public float bossHpRatio;
    public bool isBoss;
    public int isClose;


    // Start is called before the first frame update
    void Start()
    {
        isBoss = false;
        isClose = 0;
        bossHpRatio = 1;
        BossHpBar = GameObject.Find("BossHpBar").GetComponent<Image>();
        Bar = GameObject.Find("BossHpBackground");
    }

    // Update is called once per frame
    void Update()
    {
        if (isBoss)
        {
            BossHpBar.fillAmount = bossHpRatio;
        }
        if (this.transform.Find("enemies").gameObject.transform.childCount == 0 && this.transform.Find("Boss").gameObject.transform.childCount == 0)
        {
            if (isClose < 13)
            {
                Transform[] allChildren = this.transform.Find("doors").gameObject.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    child.GetComponent<Animator>().SetBool("isClose", true);
                }
                isClose++;
            }
            if (isBoss)
            {
                isBoss = false;
                Bar.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            this.transform.Find("enemies").gameObject.SetActive(true);
            this.transform.Find("objects").gameObject.SetActive(true);
            this.transform.Find("doors").gameObject.SetActive(true);
            this.transform.Find("Boss").gameObject.SetActive(true);

            if (this.transform.Find("enemies").gameObject.transform.childCount == 0 && this.transform.Find("Boss").gameObject.transform.childCount == 0)
            {
                this.transform.Find("doors").gameObject.SetActive(false);
            }

                if (this.transform.Find("Boss").gameObject.transform.childCount != 0)   
            {
                Bar.SetActive(true);
                isBoss = true;
            }
        }

        
        
            
    }
}
