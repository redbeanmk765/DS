using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomManager : MonoBehaviour
{
    public GameObject Bar;
    public Image BossHpBar;
    public GameObject boss;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BossHpBar.fillAmount = Mathf.Lerp(BossHpBar.fillAmount, hpRatio, Time.deltaTime * 10);

        if (this.transform.Find("enemies").gameObject.transform.childCount == 0 && this.transform.Find("Boss").gameObject.transform.childCount == 0)
        {
            this.transform.Find("doors").gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            this.transform.Find("enemies").gameObject.SetActive(true);
            this.transform.Find("doors").gameObject.SetActive(true);

            if (this.transform.Find("Boss").gameObject.transform.childCount != 0)
            {

                Bar.SetActive(true);
               // BossHpBar = GameObject.Find("BossHpBar").GetComponent<Image>();
            }
        }

        
        
            
    }
}
