using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class breakableObject : MonoBehaviour
{   
    // public Slider hpBar;
    public GameObject obj;
    public ItemList itemList;
    public float maxHp;
    public float nowHp;
    public GameObject spawnObj;
    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;
    public bool onFlash = false;

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

        onFlash = false;
    }

    // Update is called once per frame
    void Update()
    {
      //  float hpRatio = nowHp / maxHp;
       // hpBar.transform.position = new Vector3(position.x , position.y + 1, position.z);
       // hpBar.value = Mathf.Lerp(hpBar.value, hpRatio, Time.deltaTime * 10);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.000001f, 0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.000001f, 0);

    }

    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            

            this.GetComponent<SpriteRenderer>().material = flashMaterial;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<SpriteRenderer>().material = originalMaterial;

         

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
        if (col.CompareTag("attack"))
        {
           
            this.dmg = col.gameObject.GetComponent<weaponStat>().dmg;
            nowHp -= dmg;
            onFlash = true;
            StartCoroutine(FlashWhite());
            


            // Debug.Log(nowHp);
            // Debug.Log(hpBar.value);

            if (nowHp <= 0)
            {
                Destroy(gameObject);
                if (this.gameObject.CompareTag("Drum"))
                {
                    spawnObj = itemList.ConsumableItems[Random.Range(0, itemList.ConsumableItems.Count)];
                    Debug.Log("test");
                }
                else if (this.gameObject.CompareTag("Chest"))
                {
                    spawnObj = itemList.Weapons[Random.Range(0, itemList.Weapons.Count)];
                    Debug.Log("test2");
                }

                Instantiate(spawnObj, new Vector3(position.x, position.y , position.z), Quaternion.identity);
                     
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
    }
}

