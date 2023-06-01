using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(0, 0, 0); 
        pos.x = Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad);
        pos.y = Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad);

        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + (pos),  5f * Time.deltaTime);
    }
}
