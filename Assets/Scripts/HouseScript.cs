using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    int spawned, rng;
    float smog;
    public GameObject agent;

    // Start is called before the first frame update
    void Start()
    {
        spawned = 0;
        GameObject.Find("EnvironmentManager").GetComponent<values>().money -= 100;
        transform.SetParent(GameObject.Find("Houses").GetComponent<Transform>());
    }

    // Update is called once per frame
    void Update()
    {
        rng = Random.Range(0, 10);
        if (rng == 1 && spawned < 4)
        {
            GameObject a = Instantiate(agent);
            a.transform.localPosition = transform.position;
            a.GetComponent<AgentScript>().setHome(transform);
            spawned++;
        }
    }
}
