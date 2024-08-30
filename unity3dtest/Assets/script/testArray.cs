using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class testArray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int [] intArray;  // 声明，未初始化，不能使用
        int [] intArray2 = new int[5];  // 声明+初始化，数组是引用类型所以用new创建，初始化时默认用0创建
        intArray2[0] = 1;
        intArray2[1] = 2;

        Array.Sort(intArray2);  // 升序排序

        int[] intArray3 = {1,2,3};
        int[] intArray4 = new int[]{1,2,3};
        int[] intArray5 = new int[3]{1,2,3};   // 数组长度只能写3，后面{}有几个，长度就是几。
        int[] intArray6 = intArray5;           // intArray5 和 intArray6 指向同一份内存地址。

        foreach(int a in intArray2){
            Debug.Log("intArray:" + a);
        }

        // 多维数组
        int[,] mArray1 = new int[2,3];  // 申明 + 初始化一个2维数组，有2行，一行有3列。
        int[,] mArray2 = new int[2,3]{
            {0, 1, 2},
            {3, 4, 5}
        };

        for(int i = 0; i < 2 ; i++){
            for(int j = 0; j < 3; j++){
                Debug.LogFormat("xx-----TestArray:{0},{1},{2}", i, j, mArray2[i, j]);
            }
        }

        Debug.Log("###------mArray2.IsFixedSize:" + mArray2.IsFixedSize);  // 是否是固定大小
        Debug.Log("###------mArray2.IsReadOnly:" + mArray2.IsReadOnly);  // 是否是只读
        Debug.Log("###------mArray2.IsSynchronized:" + mArray2.IsSynchronized);  // 是否同步对数组的访问(线程安全)
        Debug.Log("###------mArray2.Length:" + mArray2.Length);  // 获取所有维中的总个数 = 行数*列数 ，返回6
        Debug.Log("###------mArray2.LongLength:" + mArray2.LongLength);  //返64位整数， 获取所有维中的总个数 = 行数*列数 ，返回6
        Debug.Log("###------mArray2.Rank:" + mArray2.Rank);  // 获取维度, 返回 2
        Debug.Log("###------mArray2.SyncRoot:" + mArray2.SyncRoot);  // 获取一个对象，该对象可用于同步对数组的访问
        
        Debug.Log("###------mArray2.GetLength(0):" + mArray2.GetLength(0)); // 获取行数，返回 2
        Debug.Log("###------mArray2.GetLength(1):" + mArray2.GetLength(1)); // 获取列数，返回 3
        Debug.Log("###------mArray2.GetUpperBound(0):" + mArray2.GetUpperBound(0)); // 获取第一维的最大索引，返回1， +1则是行数
        Debug.Log("###------mArray2.GetLowerBound(0):" + mArray2.GetLowerBound(0)); // 获取第一维的最小索引，返回0
        Debug.Log("###------mArray2.GetUpperBound(1):" + mArray2.GetUpperBound(1)); // 获取第二维的最大索引，返回2， +1则是列数
        //Debug.Log("###------mArray2.GetValue(0):" + mArray2.GetValue(0));    // 返回1维数组中指定位置的值 [多维数组不能用]
        intArray2.SetValue(66, 2); // 设置1维数组中索引为2的值为66 [多维数组不能用]
        Debug.Log("###------Array.IndexOf(intArray2, 0):" + Array.IndexOf(intArray2, 66));  //返2， 返回1维数组中指定值的位置 [多维数组不能用]

        Array.Reverse(intArray2);  // 将一个一维数组倒序

        Debug.Log("###------mArray2.ToString():" + mArray2.ToString()); // 获取数组对象的字符串

        // 交错数组
        int[][] tArray2 = new int[2][]; // 声明一个长度为3【声明时必须要有长度】，数组元素为1维数组的交错数组
        tArray2[0] = new int[5];        // 默认使用0初始化
        tArray2[1] = new int[3]{1,2,3}; // 给出了初始化

        int[][] tArray3 = new int[2][]{ // 声明+初始化
            new int[2]{1, 2},
            new int[3]{11, 12, 13}
        };

        int[][] tArray4 = { // 简化写法
            new int[2]{1, 2},
            new int[3]{11, 12, 13}
        };

        int[][,] tArray5 = new int[2][,]{  // 声明一个长度为2，数组元素是2维数组的交错数组
            new int[,]{
                {1, 1},
                {2, 2}
            },
            new int[,]{
                {3, 3},
                {4, 4},
                {5, 5},
            }
        };

        int[][,] tArray6 = {  // 简化写法
            new int[,]{
                {1, 1},
                {2, 2}
            },
            new int[,]{
                {3, 3},
                {4, 4},
                {5, 5},
            },
        };

        Debug.LogFormat("xx----Length:{0},{1},{2}, {3}, {4}", tArray6.Length, tArray6[0].GetLength(0), tArray6[0].GetLength(1) ,
             tArray6[1].GetLength(0), tArray6[1].GetLength(1) );
        for (int k = 0;k < tArray6.Length; k++){
            for(int i = 0;i < tArray6[k].GetLength(0); ++i){       // GetLength(0) 用来获取多维数组的行数
                for(int j = 0;j < tArray6[k].GetLength(1); ++j){   // GetLength(1) 用来获取多维数组的列数
                    Debug.LogFormat("tArray6[{0}][{1},{2}] = {3}", k, i, j, tArray6[k][i,j]);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
