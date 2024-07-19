using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDelegate : MonoBehaviour
{
    // 委托声明，(代理可以理解成 函数指针) 只要函数满足申明委托时的 返回类型 和 参数类型和数量 就可以用这个函数来创建这种委托
    public delegate void numChanger(int n);

    private int num = 0;

    void addNum(int n){
        num += n;
    }

    void mulNum(int n){
        num *= n;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 生成委托
        numChanger changer1 = new numChanger(addNum);
        numChanger changer2 = new numChanger(mulNum);

        changer1(5);
        changer2(2);

        // 委托的多播
        numChanger changer3 = changer1;
        changer3 += changer2;
        changer3(2);  // + 2 再 * 5

        Debug.LogFormat("num:{0, 2}", num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
