using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class testDelefate2 : MonoBehaviour
{
    // 声明一个委托
    public delegate void printString(string s);

    // 通过委托 打印字符串
    public void sendString(printString ps, string s){
        if(ps != null){
            ps(s);
        }
    }

    // 将字符串写到console中
    public void printScreen(string s){
        Debug.LogFormat("print:{0}", s);
    }

    // 将字符串写入文件
    public void printFile(string s){
        FileStream fs = new FileStream("./message.txt", FileMode.Append, FileAccess.Write);
        StreamWriter  sw = new StreamWriter(fs);

        sw.WriteLine(s);
        sw.Flush();
        sw.Close();
        fs.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 定义两个委托
        printString ps1 = new printString(printScreen);
        printString ps2 = new printString(printFile);
        // 委托的多播
        printString ps = ps1;
        ps += ps2;

        sendString(ps, "test1");
        sendString(ps, "test2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
