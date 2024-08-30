using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class testCShipBase : MonoBehaviour
{

    static string str1 = " test str ";
    string str2 = @" test\nstr ";    // 前面加 @ 可以将\转义字符当成普通字符，且可以保留多行字符串
    string str3 = "test\nstr";
    string str3_1 = "test\u0027str";  // \u + 4位16进制值 = \ + 对应转移符 . \u0027 = \' 
    string str4 = $"test {str1}";  // 前面加 $ 可以使用表达式来拼接字符串
    string str5 = string.Format("{0:D3}", 15);      // :D 表示十进制 :D3表示3位数字 不够的用0补 . output:015
    string str6 = string.Format("{0:X}", 15);       // :X 表示十六进制   output:F
    string str7 = string.Format("{0:C}", 15);       // :C 表示货币 output:￥15.00
    string str8 = string.Format("{0:N1}", 15000);   // :N 表示货币 表示有千分符的数字 N后的数字表示小数点个数 output:15,000.0
    string str9 = string.Format("{0:P}", 0.2456);   // :P 表示百分比  output:24.56%
    string str10 = string.Format("{0:##.##}", 1.2456);  // :# 表示一个数字的占位 output:1.25

    char char1 = str1[1];  // char1 = e; str1 的第二个字符
    char[] charArray = str1.ToCharArray();  // str1 转成 char 数组
    int strLen = str1.Length; // .Length 获取长度
    string upperStr = str1.ToUpper();  // ToUpper 将字符串转成大写的
    string lowerStr = str1.ToLower();  // ToLower 将字符串转成小写的
    string trimStr = str1.Trim();  // Trim() 去掉前后的空格
    static char[] filtter = { ' ', 'e' , 't' };
    string trimStr2 = str1.Trim(filtter);  // Trim() 去掉前后的空格或e或t
    string trimStr3 = str1.TrimStart(); // TrimStart 去掉前面的空格
    string trimStr4 = str1.TrimEnd(); // TrimEnd 去掉后面的空格
    string padStr1 = str1.PadLeft(12);  // PadLeft 在左边加空格直到12个字符
    string padStr2 = str1.PadRight(12); // PadLeft 在右边加空格直到12个字符

    List<string> dateList = new List<string>();

    byte bNum = 10;      // 一个字节，有符号，范围: -128~127
    sbyte bNum2 = 20;    // 一个字节，无符号，范围: 0~255
    short sNum = 30;     // 两个字节,有符号，范围：-2^15 ~ 2^15 - 1
    ushort sNum2 = 40;   // 两个字节，无符号 范围: 0~ ... 2^16-1
    int iNum = 50;       // 4个字节，有符号
    uint iNum2 = 60;     // 4个字节，无符号
    long lNum = 70;      // 8个字节，有符号
    ulong lNum2 = 80;    // 8个自己，无符号

    int[] numArray = { 1, 2, 4, 8 };
    int[] numArray2 = { 0b1, 0b100, 0b1_0110 };  // 0b表示2进制, 0b1 = 1, 0b100 = 4 , 0b1_0110 = 22  . _只是为了方便读的分隔符
    int[] numArray3 = { 0x1, 0x10, 0xa1 }; // 0x1 = 1, 0x10 = 16, 0xa1 = 161
       
    string str99 = (5 < 9) ? "test1" : "test2" ; // 3元运算符

    // 枚举类型 , 存储类型默认为int, 存储值默认从0开始
    enum Dir 
    {
        north, south, east, west
    }
    Dir dir =Dir.north;
    // 
    enum Dir2 : byte
    {
        north = 1,
        south , // 2
        east = 5,
        west , // 6
    }
    Dir2 dir2 = Dir2.north;
    // 结构体
    struct UserInfo
    {
        public string name;
        byte age;
        public byte Age{ // 属性
            get { return age; } set { age = value; }
        }
        long phone;
    }
    
    void printInfo(string msg) => Debug.Log($"xxxx---------------!!! test:{msg}");  // => 一行的函数可以用=> 来简写

    static int sum(params int[] nums)  // 函数最后一个可以定义一个同类型的数组参数
    {
        int total = 0;
        for(int i = 0;i< nums.Length; ++i)
        {
            total += nums[i];
        }
        return total;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"xxx--------total:{sum(1, 2, 3, 4, 5, 6)}");// 调用时直接传n个对应类型的参数 

        printInfo("1");
        // 日期相关的输出
        dateList.Add(string.Format("{0:d}", System.DateTime.Now));  // output:2024/8/7
        dateList.Add(string.Format("{0:D}", System.DateTime.Now));  // output:2024年8月7日
        dateList.Add(string.Format("{0:f}", System.DateTime.Now));  // output:2024年8月7日 19:41
        dateList.Add(string.Format("{0:F}", System.DateTime.Now));  // output:2024年8月7日 19:41:38
        dateList.Add(string.Format("{0:g}", System.DateTime.Now));  // output:2024/8/7 19:43
        dateList.Add(string.Format("{0:G}", System.DateTime.Now));  // output:2024/8/7 19:43:31
        dateList.Add(string.Format("{0:m}", System.DateTime.Now));  // output:8月7日
        dateList.Add(string.Format("{0:t}", System.DateTime.Now));  // output:19:43
        dateList.Add(string.Format("{0:T}", System.DateTime.Now));  // output:19:43:31

        Debug.LogFormat("xxx------testStr1:{0}", str1);
        Debug.LogFormat("xxx------testStr2:{0}", str2);
        Debug.Log($"xxx------testStr3:{str3}");
        Debug.Log($"xxx------testStr3_1:{str3_1}");
        Debug.Log($"xxx------testStr4:{str4}");
        Debug.Log($"xxx------testStr5:{str5}");
        Debug.Log($"xxx------testStr6:{str6}");
        Debug.Log($"xxx------testStr7:{str7}");
        Debug.Log($"xxx------testStr8:{str8}");
        Debug.Log($"xxx------testStr9:{str9}");
        Debug.Log($"xxx------testStr10:{str10}");

        // 基础类型转换
        int convertNum = Convert.ToInt32("15");
        string convertStr = Convert.ToString(15);
        bool convertChar = Convert.ToBoolean(12);
        float convertFloat = Convert.ToSingle(16);
        short srcNum = 7;
        short srcNum2 = 290;
        byte destNum = (byte)srcNum;
        byte destNum2 = Convert.ToByte(srcNum);
        //byte destNum3 = checked((byte)srcNum2);  // checked 函数会检查转换是否溢出。如果溢出会报错

        string dirStr = Convert.ToString(dir2);
        // 字符串转成枚举变量
        Dir2 dir3 = (Dir2)(Enum.Parse(typeof(Dir2), "north"));

        Dir dir4 = (Dir)Enum.Parse(typeof(Dir), "north");

        UserInfo myInfo = new UserInfo();
        myInfo.name = "wei";
        myInfo.Age = 30;


        //
        int idx = 10;
        foreach (string str in dateList)
        {
            idx++;
            Debug.LogFormat($"xxx-----testStr{idx}:{str}");
        }

        for (int i = 0; i < numArray2.Length; i++)
        {
            Debug.Log($"xxxx-------numArray:k = {i}, v = {numArray2[i]}");
        }
        // switch 模式匹配
        string[] friendNames = { "wei", "yao", "chen", null, "" };
        foreach (string str in friendNames)
        {
            switch (str)
            {
                case string t when t.StartsWith("y"):  // 以y开始的字符串
                    Debug.LogFormat("xx----case 1:{0}", t);
                    break;
                case string e when e.Length == 0:  // 长度为0的
                    Debug.LogFormat("xx----case 2:{0}", e);
                    break;
                case null:  // null 空
                    Debug.Log("xx----case 3:");
                    break;
                case var x: // 普通值
                    Debug.LogFormat("xx----case 4:{0}", x);
                    break;
                default:
                    break;
            }
        }

        // 元组
        var numbers = (1, 2, 3);  // 元组, 默认成员 Item1 = 1, Item2 = 2, Item3 = 3
        var first = numbers.Item1;

        (int one, int two, int three) numbers2 = (4, 5, 6); // 元组, 自定义成员 one = 4, two = 5, three = 6
        var first2 = numbers2.one;

        IEnumerable<int> numbers3 = new int[] { 1, 2, 3, 4, 5, 6 };
        var result = getMaxMinAvg(numbers3);
        Debug.Log($"xxx----resule:max:{result.max}, min:{result.min}, avg:{result.avg}");

        // 作用域
        int iddx;
        for (iddx = 0; iddx < 10; ++iddx)
        {
            string text = $"Line { Convert.ToString( iddx) }";
            printInfo( text );
        }


    }
    // 定义一个返回值是元组的函数
    static (int max, int min, double avg) getMaxMinAvg(IEnumerable<int> numbers)
    {
        return (Enumerable.Max(numbers), Enumerable.Min(numbers), Enumerable.Average(numbers));  // 返回元组
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
