using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foreachTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 声明并赋值数组
        int[] intList = new int[] {1,2,3,4,5};
        foreach(int i in intList){
            Debug.Log("foreach i:" + i);
        }

        // 先声明后赋值数组
        int [] intList2 = new int[20];
        for(int i = 0; i < 20;i++){
            intList2[i] = i;
        }
        foreach(int i in intList2){
            Debug.Log("foreach22 i:" + i);
        }

        // 列表
        List<string> strList = new List<string>();
        strList.Add("hello");
        strList.Add("world");

        foreach(string str in strList){
            Debug.Log("foreach strList:" + str);
        }

        // 字典
        var dic = new Dictionary<int, string>();
        dic.Add(1, "hello");
        dic.Add(2, "world");

        foreach(var a in dic){
            Debug.LogFormat( "foreach dic:{0}, {1}, {2}"  ,a , a.Key , a.Value) ;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
