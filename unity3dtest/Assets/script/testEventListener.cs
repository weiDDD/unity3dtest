using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEventListener : MonoBehaviour
{

    public void printString(string s){
        Debug.LogFormat("testEventListener:{0}", s);
    }

    // 事件对象
    testEvent eventObj = new testEvent();

    // Start is called before the first frame update
    void Start()
    {
        // 事件 += 委托
        eventObj.changeEvent += new testEvent.onStringChange(printString);

        eventObj.setString("event Listener");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
