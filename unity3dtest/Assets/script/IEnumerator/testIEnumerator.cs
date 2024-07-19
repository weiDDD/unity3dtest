using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testIEnumerator : MonoBehaviour
{
    ArrayList aList = new ArrayList();
    
    // Start is called before the first frame update
    void Start()
    {
        aList.Add(1);
        aList.Add("a");
        aList.Add(false);
        // 普通遍历
        foreach(object a in aList){
            Debug.LogFormat("xxxx-------aList {0} ", a);
        }
        // 使用迭代器遍历
        IEnumerator itor = aList.GetEnumerator();
        while(itor.MoveNext()){
            object item = itor.Current;
            Debug.LogFormat("xxxx------aList item:{0}", item);
        }
        itor.Reset();  // 重置

        // 
        object obj = GetEnumerator();
        Debug.LogFormat("xxxx------aList 3 {0}", obj);

        // foreach 里面会调用 GetEnumerator 获取迭代器, moveNext, Current 等
        foreach( object obj2 in this ){
            Debug.LogFormat("xxxx------aList 4 {0}", obj2);
        }
    }   

    // 给自己类加一个 GetEnumerator 接口，让其可以被 foreach 使用。
    public IEnumerator GetEnumerator(){
        for(int i = 0;i < aList.Count; i++){
            yield return aList[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
