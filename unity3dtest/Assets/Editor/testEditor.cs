using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

public class testEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [MenuItem("Test/PrintAssemblyNames")]
    public static void PrintAssemblyNames(){
        Debug.Log("xx------PrintAssemblyNames");
        Assembly[] playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);

        foreach(var assembly in playerAssemblies){
            Debug.Log(assembly.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
