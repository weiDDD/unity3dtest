using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent : MonoBehaviour
{
    private string value;
    public delegate void onStringChange(string s);

    public event onStringChange changeEvent;

    public void setString(string s){
        value = s;
        changeEvent(s);
    }

    // Start is called before the first frame update
    void Start()
    {
        setString("testEvent1");
        setString("testEvent2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
