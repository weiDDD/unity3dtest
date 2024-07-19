using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testFunc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int a = 10;
        int b = 20;

        Debug.LogFormat("before swap {0},{1}", a,b);
        swap(ref a, ref b);
        Debug.LogFormat("after swap {0},{1}", a,b);

        int outA;
        int outB;
        testOut(a,out outA,out outB);
        Debug.LogFormat("testOut {0},{1}", outA, outB);

        Debug.LogFormat("getSum1:{0}", getSum(1,2,3,4,5));
        int[] testArray = { 1,2,3,4,5 };
        Debug.LogFormat("getSum2:{0}", getSum(testArray) );

        Debug.LogFormat("getAvg:{0}", getAvg(testArray));
    }
    // ref 关键字表示引用参数
    public void swap(ref int a, ref int b){
        int tem = a;
        a = b;
        b = tem;
    }

    // out 关键字表示输出
    public void testOut(int inValue, out int a, out int b){
        a = inValue + 10;
        b = inValue + 20;
    }

    // params 传递不定参数
    public int getSum(params int[] arrayParm){
        int sum = 0;
        for(int i = 0;i < arrayParm.Length; ++i){
            sum += arrayParm[i];
        }
        return sum;
    }

    // 获取平均值
    public float getAvg(int[] nums){
        int sum = 0;
        for(int i = 0;i< nums.Length; ++i){
            sum += nums[i];
        }
        return sum / nums.Length;
    }

    [MenuItem("Test/funcs/testOutput")] // unity 编辑器上增加自定义功能按钮
    static void outPut(){
        Debug.LogFormat("total:{0}", 20);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
