using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class breakableObject : MonoBehaviour
{
    public GameObject obj;
    public Slider hpBar;
    public float maxHp;
    public float nowHp;
    public GameObject spawnObj;

    public string objectName;
    public int dmg;
    Vector3 position;

    private void setObjectStat(int maxHpSet, string objectNameSet)
    {
        objectName = objectNameSet;
        maxHp = maxHpSet;
        nowHp = maxHp;


    }

    // Start is called before the first frame update
    void Start()
    {
        if(obj.CompareTag  ("Drum"))
            setObjectStat(10, "Drum");
         position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float hpRatio = nowHp / maxHp;
        hpBar.transform.position = new Vector3(position.x , position.y + 1, position.z);
        hpBar.value = Mathf.Lerp(hpBar.value, hpRatio, Time.deltaTime * 10);
        

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("attack"))
        {
            this.dmg = col.gameObject.GetComponent<weaponStat>().dmg;
            nowHp -= dmg;
          
            

           // Debug.Log(nowHp);
           // Debug.Log(hpBar.value);

            if (nowHp <= 0)
            {
                Destroy(gameObject);
               
                    Instantiate(spawnObj, new Vector3(position.x, position.y , position.z), Quaternion.identity);
                     
            }
            
        }
    }
}
