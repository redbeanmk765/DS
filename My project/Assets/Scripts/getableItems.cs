using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class getableItems : MonoBehaviour
{
    public GameObject obj;
    public TMP_Text nameText;
    bool isNear;

    public string objectName;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = objectName;
        nameText.gameObject.SetActive(false);
        position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        nameText.transform.position = new Vector3(position.x, position.y + 3, position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
                nameText.gameObject.SetActive(true);
                isNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            nameText.gameObject.SetActive(false);
            isNear = false;
        }
    }

}
