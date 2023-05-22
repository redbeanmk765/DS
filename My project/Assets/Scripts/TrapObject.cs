using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapObject : MonoBehaviour
{
    public string objectName;
    public float dmg;
    public GameObject onObj;

    public float timer;
    public int waitingTime;

    public bool timeCheck = true;
    public bool onTrigger = false;

    private void setObjectStat(string objectNameSet)
    {
        objectName = objectNameSet;

    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        waitingTime = 2;
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onTrigger = true;
            col.gameObject.GetComponent<playerStat>().damage = this.dmg;
            onObj = col.gameObject;
            StartCoroutine(WaitForDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {   
            onTrigger = false;
        }
    }

    IEnumerator WaitForDamage()
    {
        while (onTrigger) 
        {
            yield return new WaitForSeconds(1.0f);

            if (onTrigger == false)
            {
                yield break;
            }
            onObj.gameObject.GetComponent<playerStat>().damage = this.dmg;
        }
        if(onTrigger == false)
        {
            yield break;
        }
    }
}
