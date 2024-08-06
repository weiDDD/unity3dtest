using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

interface IWorker { };
class User {
    private int _num = 0;
    public string Phone = "123456";
    public string Name { get; set; }
    public string Address { get; set; }
    static int userNum;
    public User() {
        Debug.Log("User class ctor");
    }
    public User(string name)
    {
        Name = name;
        Debug.Log("User class ctor with name");
    }
    public int PublicMethod()
    {
        Debug.LogFormat("PublicMethod minValue:{0}", int.MinValue);
        return int.MinValue;
    }
    internal void InternalMethod()
    {
        Debug.LogFormat("InternalMethod MaxValue:{0}", int.MaxValue);
    }
    private void PrivateMethod()
    {
        Debug.LogFormat("PrivateMethod MaxValue:{0}", int.MaxValue);
    }

};

public class testReflection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ********************** 获取Type类型的方式 ↓
        User user1 = new User();
        Type type1 = typeof(User);      // typeof(className) 通过一个类名获取Type
        Type type2 = user1.GetType();   // obj.GetType()     通过一个变量获取Type
        Type type3 = Type.GetType("User");  // 使用 Type.GetType 获取。同命名空间的传 类型的 FullName ; 
        //Type type4 = Type.GetType(类型的AssemblyQualifiedName); // 不同命名空间需要用 AssemblyQualifiedName 获取类型. [不建议]

        // ********************** Type 的一些属性 ↓
        Debug.LogFormat("type.Name:{0}-{1}", type1.Name, type2.Name);               // Name 是类型名称
        Debug.LogFormat("type.FullName:{0}-{1}", type1.FullName, type2.FullName);   // FullName 是带命名空间的类型名称
        Debug.LogFormat("type.Namespace:{0}-{1}", type1.Namespace, type2.Namespace);   // Namespace 是命名空间
        Debug.LogFormat("type.AssemblyQualifiedName:{0}-{1}", type1.AssemblyQualifiedName, type2.AssemblyQualifiedName);   // AssemblyQualifiedName 是包含了程序集的限定名

        Debug.LogFormat("type.IsEnum:{0}-{1}", type1.IsEnum, type2.IsEnum);   // IsEnum 表示该类型是否是枚举
        Debug.LogFormat("type.IsGenericType:{0}-{1}", type1.IsGenericType, type2.IsGenericType);   // IsGenericType 表示该类型是否是泛型类型
        // 其他详见微软文档

        // ********************** 程序集信息 ↓
        Assembly assembly = type1.Assembly;  // 获取类型的程序集
        Debug.LogFormat("assembly.FullName:{0}", assembly.FullName);  // FullName 表示强名称.具有名称，版本，语音，公钥 .

        // ********************** Type 常用方法 ↓
        // GetConstructors 获取所有的 public 的构造函数
        ConstructorInfo[] constructorInfos = type1.GetConstructors();
        foreach(var item in constructorInfos)
        {
            // GetParameters() 获取指定方法或构造函数的参数
            ParameterInfo[] parameterInfos = item.GetParameters();
            foreach(var pi in parameterInfos)
            {
                Debug.LogFormat("item.Name:{0};pi.Name:{1};pi.ParameterType:{2}", item.Name, pi.Name, pi.ParameterType);
            }
        }
        // GetMethods() 获取该类型所有的 public 方法
        MethodInfo[] methodInfos = type1.GetMethods(); // 
        foreach(var method in methodInfos)
        {
            Debug.LogFormat("GetMethods:{0},{1},{2}" , type1.Name, method.Name, method.ReturnType);
        }
        // GetProperties 获取所有的属性。 BindingFlags 可以用来做筛选. BindingFlags.Instance 表示实例化的成员
        PropertyInfo[] propertyInfos = type1.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach(var property in propertyInfos)
        {
            Debug.LogFormat("xx---PropertyInfo:{0},{1},{2}",type1.Name, property.Name, property.PropertyType);
        }
        // GetFields 获取字段
        FieldInfo[] fieldInfos = type1.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach(FieldInfo field in fieldInfos)
        {
            Debug.LogFormat("xx---FieldInfo:{0},{1},{2}", type1.Name, field.Name, field.FieldType);
        }
        // GetMembers 获取成员
        MemberInfo[] memberInfos = type1.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (MemberInfo member in memberInfos)
        {
            Debug.LogFormat("xx---MemberInfo:{0},{1},{2}", type1.Name, member.Name, member.MemberType);
        }

        // ********************** System.Activator类的应用 ↓
        // 使用 Activator.CreateInstance(TypeObject) 创建一个类型的实例对象。
        User user2 = (User)Activator.CreateInstance(type1);

        // ********************** Assembly 类的应用 ↓
        // ** 程序集的加载
        // Assembly.Load 传入一个程序集名称，不带后缀。会动态去查找。
        // 传入程序集的FullName:先去全局程序集缓存查找 -> 程序根目录 -> 应用程序私有路径
        // 传入简单程序集名字: 程序根目录 -> 应用程序私有路径
        Assembly assembly1 = Assembly.Load("test1");
        // Assembly.LoadFrom 传入一个全路径，需要后缀。会自动加载引用的程序集
        Assembly assembly2 = Assembly.LoadFrom( System.IO.Directory.GetCurrentDirectory() + @"\Library\ScriptAssemblies\test2.dll");
        // Assembly.LoadFile 传入一个全路径，需要后缀。不会自动加载引用的程序集
        Assembly assembly3 = Assembly.LoadFile(System.IO.Directory.GetCurrentDirectory() + @"\Library\ScriptAssemblies\testEditor3.dll");

        // ** 获取类型
        Type assType1 = assembly1.GetType("testAsmdef");
        
        // ** 动态创建类对象
        // 使用Activator.CreateInstance(TypeObj) 创建
        testAsmdef obj1 = (testAsmdef)Activator.CreateInstance(assType1);
        // 使用 assemblyObj.CreateInstance(TypeName, ignoreCase)
        testAsmdef obj2 = (testAsmdef)assembly1.CreateInstance("testAsmdef", true);
        // 使用 ConstructorInfo.Invoke 创建
        ConstructorInfo ctorInfo = assType1.GetConstructor(new Type[] { });  // 获取的是无参构造函数
        testAsmdef obj3 = (testAsmdef)ctorInfo.Invoke(new object[] { });
        // 获取一个参数的构造函数，并传入参数创建对象
        ConstructorInfo ctorInfo2 = assType1.GetConstructor(new Type[] { typeof(string) });  // 获取的是一个字符串的构造函数
        testAsmdef obj4 = (testAsmdef)ctorInfo2.Invoke(new object[] { "hello" });

        // ** 方法调用
        obj1.show("test");  // 用对象直接调方法
        // 使用TypeObj获取方法信息, 再通过Invoke 调用
        MethodInfo mInfo = assType1.GetMethod("show");
        mInfo.Invoke(obj1, new object[] { "test2" }); // 非static 方法第一个参数传入类对象; static 方法第一个参数传null

        // ** 设置属性
        PropertyInfo pInfo = assType1.GetProperty("Phone", BindingFlags.Instance| BindingFlags.NonPublic);
        pInfo.SetValue(obj1, 222333);

        // ** 设置字段
        FieldInfo fInfo = assType1.GetField("_num", BindingFlags.Instance | BindingFlags.NonPublic);
        fInfo.SetValue(obj1, 666);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
