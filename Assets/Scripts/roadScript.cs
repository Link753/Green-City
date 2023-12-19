using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class roadScript : MonoBehaviour
{
    Transform Roads;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Roads").GetComponent<Transform>());
        GetComponent<Collider>().enabled = true;
        GameObject.Find("EnvironmentManager").GetComponent<values>().money -= 5;
        GameObject.Find("NavMesh Surface").GetComponent<NavMeshSurface>().RemoveData();
        GameObject.Find("NavMesh Surface").GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
