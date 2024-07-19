using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 声明 + 初始化List
        List<string> strList = new List<string>(){
            "hello", "world", "wss"
        };

        strList.Add("wfw");
        strList.Add("mylove");

        // 遍历
        foreach(string str in strList){
            Debug.Log("strList:" + str);
        }
        for(int i = 0;i < strList.Count ; i++){
             Debug.Log("strList2:" + strList[i]);
        }

        // List 转 Array
        string[] strArray = strList.ToArray();
        for(int i=0; i< strArray.Length; ++i){
            Debug.Log("strArray:" + strArray[i]);
        }

        // Array 转 List
        string[] strArray2 = {"ni", "hao", "boy"};
        List<string> strList2 = strArray2.ToList() ;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
