using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    int Tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Tmp != 100)
        {
            if (Tmp == 99)
            {
                GameObject.Find("BossHpBackground").SetActive(false);
                this.gameObject.SetActive(false);
            }
            Tmp++;
        }
    }
}
