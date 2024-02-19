using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public Transform prefab;
    public int gridResolution = 10;
    Transform[] grid;

    byte testByte = 0;
    char testStr = '0';

    // Start is called before the first frame update
    void Start()
    {
        grid = new Transform[gridResolution * gridResolution * gridResolution];
        for(int i = 0, z = 0; z < gridResolution; z++){
            for(int y= 0 ;y < gridResolution; y++){
                for(int x = 0; x < gridResolution ;x++, i++){
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }

        Debug.Log("size of byte:" + sizeof(byte));
        Debug.Log("size of char:" + sizeof(char));
    }

    Transform CreateGridPoint(int x, int y, int z){
        Transform point = Instantiate<Transform>(prefab);
        point.localPosition = GetCoordinates(x,y,z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );
        return point;
    }

    Vector3 GetCoordinates(int x, int y, int z){
        return new Vector3(
            x - (gridResolution - 1) * 0.5f,
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
