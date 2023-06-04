using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.Find("enemies").gameObject.transform.childCount == 0)
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
        }
    }
}
