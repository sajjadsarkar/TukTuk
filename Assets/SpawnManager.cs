using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> Roads;
    public float offset = 60f;
    // Start is called before the first frame update
    void Start()
    {
        if(Roads !=  null && Roads.Count > 0)
        {
            Roads = Roads.OrderBy(r => r.transform.position).ToList();
            }
    }
    public void MoveRoad()
    {
        GameObject moveRoad = Roads[0];
        Roads.Remove(moveRoad);
        float newz = Roads[Roads.Count - 1].transform.position.z + offset;
        moveRoad.transform.position = new Vector3(0,0,newz);
        Roads.Add(moveRoad);
    }

    public void SpwanTriggerEnter()
    {
        MoveRoad();
    }
   
}
