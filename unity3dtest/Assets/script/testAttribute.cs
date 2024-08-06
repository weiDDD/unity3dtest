// 测试 Attribute 特性
//#define Debug_wss
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

// 声明一个自定义特性类
[AttributeUsage(
    AttributeTargets.Class|AttributeTargets.Constructor|AttributeTargets.Field|AttributeTargets.Method,
    AllowMultiple = true, // 是否可以多个特性修饰同一个对象
    Inherited = false
    )]
public class DebugInfo : System.Attribute {
    private int bugNo;
    private string developer;
    private string lastReview;
    public string message;

    public DebugInfo(int bg, string dev, string d){
        this.bugNo = bg;
        this.developer = dev;
        this.lastReview = d;
    }

    public int BugNo{
        get{
            return bugNo;
        }
    }
    public string Developer{
        get{
            return developer;
        }
    }
    public string LastReview{
        get{
            return lastReview;
        }
    }
    public string Message{
        get{
            return message;
        }
        set{
            message = value;
        }
    }
}

[DebugInfo(1, "wss", "12/4/2024", Message = "测试信息1")]
[DebugInfo(2, "ddd", "20/4/2024", Message = "测试信息2")]
public class testAttribute : MonoBehaviour
{

    [Conditional("Debug_wss")] // 条件执行，只有当定义了 Debug_wss 这个预编译标识符才会执行。
    public static void message(string msg){
        UnityEngine.Debug.Log("conditional attribute msg:" + msg);
    }

    [Obsolete("please use func2", true)] // 表示一个弃用程序，第一个参数为提示语,第二参数表示是否生成一个 error (false为生成一个警告)
    void func1(){
        UnityEngine.Debug.Log("xxxx--------func1");
    }

    [DebugInfo(3, "ddd", "20/5/2024", Message = "测试信息3")]
    void func2(){
        UnityEngine.Debug.Log("func2");
    }

    

    // Start is called before the first frame update
    void Start()
    {
        testAttribute.message("testAttribute start");

        //func1();
        func2();

        // 利用反射获取 DebugInfo 。typeof() 返回一个System.Type类型。 MemberInfo 继承自 System.Type. 表示获取一个类的元素
        System.Reflection.MemberInfo info = typeof(testAttribute);
        // GetCustomAttributes 获取自定义的 Attribute . false表示不获取继承链条上的 Attribute,只获取自己这一层
        object[] attributes = info.GetCustomAttributes(false);
        UnityEngine.Debug.LogFormat("xxx---- attributes length {0}", attributes.Length);

        for(int i = 0; i< attributes.Length; i++){
            // 强转成 DebugInfo
            DebugInfo dInfo = (DebugInfo)attributes[i];
            if(null != dInfo){
                UnityEngine.Debug.LogFormat("dInfo 编号 {0}", dInfo.BugNo);
                UnityEngine.Debug.LogFormat("dInfo 开发者 {0}", dInfo.Developer);
                UnityEngine.Debug.LogFormat("dInfo 上次检查时间 = {0}", dInfo.LastReview);
                UnityEngine.Debug.LogFormat("dInfo 消息 = {0}", dInfo.Message);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
