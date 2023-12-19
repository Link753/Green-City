using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterScript : MonoBehaviour
{
    public float smogeffect = 5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("waterpoints").GetComponent<Transform>());
        GameObject.Find("EnvironmentManager").GetComponent<values>().money -= 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float RemoveSmog()
    {
        float total = 0;
        GameObject g = GameObject.Find("Environment");
        for (int i = 0; i < g.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, g.transform.GetChild(i).GetComponent<Transform>().position) < 20f)
            {
                total++;
            }
        }

        float end = smogeffect / total;
        Debug.Log(end);
        return end;
    }
}
