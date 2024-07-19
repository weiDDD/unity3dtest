using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testNullValue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int? a = null;
        int? b = 1;
        int? c = a ?? 50;  // ?? 表示如果a为空则 c = 50
        int? d = new int?();

        Debug.LogFormat("testNullValues:{0},{1},{2},{3}", a , b, c, d);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
