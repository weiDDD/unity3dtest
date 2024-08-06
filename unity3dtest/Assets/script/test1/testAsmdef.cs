using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAsmdef : MonoBehaviour
{
    private string _name = "";
    private int _num = 0;

    private int Phone
    {
        get;set;
    }

    // 无参构造函数
    public testAsmdef()
    {

    }
    public testAsmdef(string name)
    {
        _name = name;
    }
    public testAsmdef(string name, int num)
    {
        _name = name;
        _num = num;
    }

    public void show(string moreTxt)
    {
        Debug.Log("xxx-------testAsmdef show:" + moreTxt);
    }

    // Start is called before the first frame update
    void Start()
    {
        var t = new testAsmdef2();
        t.output();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
