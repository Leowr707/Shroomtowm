using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hudHP : MonoBehaviour
{
   public int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
