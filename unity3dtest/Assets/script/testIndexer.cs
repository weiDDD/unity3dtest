// 测试 索引器

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testIndexer : MonoBehaviour
{
    private List<string> names = new List<string>();

    private int size = 10;

    // 定义索引器
    public string this[int idx]{
        get{ // get访问器
            if(idx >= 0 && idx < names.Count){
                return names[idx];
            }
            return "";
        }
        set{ // set 访问器
            if(idx >= 0 && idx < names.Count){
                names[idx] = value;
            }
        }
    }
    
    //
    public override string ToString(){
        string tem = "";

        foreach(var s in names){
            tem += s;
            tem +=",";
        }
        return tem;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 先给list 加元素
        for(int i = 0;i <= size ;i++){
            names.Add("1");
        }

        this[0] = "ni";
        this[1] = "hao";
        this[2] = "world";

        Debug.LogFormat("xxx--------test Indexer:{0},{1}", this[0] , this );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
