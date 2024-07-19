using System;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Unity.Editor.Example {
   static class AddPackageExample
   {
       static AddRequest Request;
      
       [MenuItem("Test/Add Package Example")]
       static void Add()
       {
           // Add a package to the project
           Request = Client.Add("com.unity.textmeshpro");
           EditorApplication.update += Progress;
       }

       static void Progress()
       {
           if (Request.IsCompleted)
           {
               if (Request.Status == StatusCode.Success)
                   Debug.Log("Installed: " + Request.Result.packageId);
               else if (Request.Status >= StatusCode.Failure)
                   Debug.Log(Request.Error.message);

               EditorApplication.update -= Progress;
           }
       }

        static ListRequest listRequest;
        [MenuItem("Test/List Package Example")]
        static void List()
        {
           listRequest = Client.List();    // List packages installed for the project
           EditorApplication.update += Progress2;
        }

       static void Progress2()
       {
           if (listRequest.IsCompleted)
           {
               if (listRequest.Status == StatusCode.Success)
                   foreach (var package in listRequest.Result)
                       Debug.Log("Package name: " + package.name);
               else if (listRequest.Status >= StatusCode.Failure)
                   Debug.Log(listRequest.Error.message);

               EditorApplication.update -= Progress2;
           }
       }
   }
}