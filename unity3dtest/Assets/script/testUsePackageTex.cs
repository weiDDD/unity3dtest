using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testUsePackageTex : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        // 加载自己工程的纹理资源
        GameObject cube = GameObject.Find("TestCube");
        if (cube){
            Material mat = cube.GetComponent<Renderer>().material;
            mat.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/tex/bing_01.png", typeof(Texture2D)) ;
        }
        // 加载其他包的纹理资源
        GameObject cube2 = GameObject.Find("TestCube2");
        if (cube2){
            Material mat = cube2.GetComponent<Renderer>().material;
            // 访问包中的资源需要加上前缀： Packages/<包名>/
            mat.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Packages/com.mytest.test1/Assets/Texture/holder_qizi_01.png", typeof(Texture2D)) ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
