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
    public string objectName;
    public int dmg;
    Vector3 position;
    private void setObjectStat(int maxHpSet, string objectNameSet)
    {
        objectName = objectNameSet;
        maxHp = maxHpSet;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(objectName == "Drum")
            setObjectStat(5, "Drum");
         position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.transform.position = new Vector3(position.x + 0.5f, position.y + 10, position.z);
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            this.dmg = col.gameObject.GetComponent<weaponStat>().dmg;
            nowHp -= dmg;

            hpBar.value = nowHp / maxHp;

            Debug.Log(nowHp);
            Debug.Log(hpBar.value);

            if (nowHp <= 0)
                {
                //nowHp = maxHp;
                Destroy(gameObject);
               
                    Instantiate(obj, new Vector3(position.x, position.y + 1, position.z), Quaternion.identity);
                     
                }
            
        }
    }
}
