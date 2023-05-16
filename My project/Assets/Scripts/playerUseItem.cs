using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUseItem : MonoBehaviour
{
    public GameObject player;
    public int pressedItemNumber = 0;
    public bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pressedItemNumber = player.GetComponent<playerStat>().item1Number;
            pressed = true;
            player.GetComponent<playerStat>().item1Number = 0;
            player.GetComponent<playerStat>().item1.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pressedItemNumber = player.GetComponent<playerStat>().item2Number;
            pressed = true;
            player.GetComponent<playerStat>().item2Number = 0;
            player.GetComponent<playerStat>().item2.gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            pressedItemNumber = player.GetComponent<playerStat>().item3Number;
            pressed = true;
            player.GetComponent<playerStat>().item3Number = 0;
            player.GetComponent<playerStat>().item3.gameObject.SetActive(false);
        }

        if(pressed == true)
        {
            switch (pressedItemNumber)
            {
                case 0: 
                    break;
                case 301:
                    Debug.Log("use potion");
                    player.GetComponent<playerStat>().cure = 30;
                    break;

            }

            pressed = false;
        }
    }
}
