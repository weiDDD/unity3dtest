using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCoroutine : MonoBehaviour
{
    private int updateIdx = 0;

    private IEnumerator testFunc(){
        Debug.LogFormat("xxx------------testFunc:{0}",updateIdx);
        yield return 5; // 下一帧后回来，从这里再继续执行  yield return null 或任何数字都是下一帧回来继续执行
        Debug.LogFormat("xxx-------------testFunc 2:{0}",updateIdx);
        yield return new WaitForSeconds(1);  // 1s后回来继续执行
        Debug.LogFormat("xxx-------------testFunc 3:{0}",updateIdx);
        yield return testFunc2();  // 调用testFunc2 , testFunc2 必须执行完之后才会回到这里继续执行。testFunc2中间的yield不会回到这里继续执行，不会让出控制权到testFunc。
        Debug.LogFormat("xxx-------------testFunc 4:{0}",updateIdx);
    }

    private IEnumerator testFunc2(){
        int total = 0;
        for(int i = 0;i < 10 ;i++){
            total += i;
        }
        Debug.LogFormat("total:{0}", total);
        
        Debug.LogFormat("xxx------------testFunc2 0:{0}",updateIdx);
        //yield break;
        
        yield return new WaitForSeconds(1); // 5帧后回来，从这里再继续执行
        Debug.LogFormat("xxx------------testFunc2 2:{0}",updateIdx);
    }

    private IEnumerator testFunc3(){
        Debug.LogFormat("xxxx-----testFunc3 0", updateIdx);
        yield return 1;
        Debug.LogFormat("xxxx-----testFunc3 1",updateIdx);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(testFunc());   // 启动一个testFunc 的协程
        StartCoroutine(testFunc3());  // 启动一个 testFunc3 的协程，会跟 testFunc 抢占资源
    }

    // Update is called once per frame
    void Update()
    {
        updateIdx++;
    }
}
