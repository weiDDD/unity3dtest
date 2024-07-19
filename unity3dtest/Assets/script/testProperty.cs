using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testProperty : MonoBehaviour
{
    // 定义两个私有域
    private string code;
    private string name;
    //private int age;

    // 定义名为Code的属性
    public string Code{
        get{ // get 访问器
            return code;
        }
        set{ // set 访问器
            code = value;
        }
    }
    // 定义名为 Name 的属性
    public string Name{
        get{
            return name;
        }
        set{
            name = value;
        }
    }

    // 抽象属性
    //public abstract int Age{
    //    get;
    //    set;
    //}

    // 重写 ToString 方法
    public override string ToString(){
        return "编码:" + Code + ",姓名:" + Name;
    }

    // Start is called before the first frame update
    void Start()
    {
        Code = "test";
        Name = "wss";

        Debug.LogFormat("xx---testProperty:{0},{1},{2}",Code , "wss", this);  // this 用了 ToString
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
