using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;

public class AgentScript : MonoBehaviour
{
    [SerializeField] float energy, health;
    values value;
    GameObject job, leisureAreas, jobTargets;
    float speed, hour;
    int[] jobSchedule;
    AgentState state = new();
    NavMeshAgent nav;
    Transform Home;
    MeshRenderer mesh;

    IEnumerator DoAShift()
    {
        yield return new WaitForSeconds(jobSchedule[1] = jobSchedule[0]);
        energy -= 50;
        state = AgentState.RELAX;
    }

    enum AgentState
    {
        REST,
        WORK,
        RELAX
    }

    private void Awake()
    {
        jobTargets = GameObject.Find("jobSites");
        leisureAreas = GameObject.Find("Leisure");
        state = AgentState.REST;
        jobSchedule = new int[2];
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
        energy = 100f;
        health = 100f;
        nav = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshRenderer>();
        value = GameObject.Find("EnvironmentManager").GetComponent<values>();
        value.addAgent();
    }

    // Update is called once per frame
    void Update() 
    {
        hour = value.GetHour();
        nav.speed = speed - value.GetSmog();

        if (job != null)
        {
            FoundJob();
        }

        switch (state) 
        {
            case AgentState.REST:
                Rest();
                break;
            case AgentState.WORK:
                Work();
                break;
            case AgentState.RELAX:
                Relax();
                break;
        }

        if(energy > 20f)
        {
            if (hour == jobSchedule[0])
            {
                state = AgentState.WORK;
            }
            else if (hour == jobSchedule[1])
            {
                state = AgentState.RELAX;
            }
        }
        else
        {
            state = AgentState.REST;
        }

        if(health < 0)
        {
            Destroy(gameObject);
        }
        
    }

    void Rest()
    {
        nav.SetDestination(Home.transform.position);
    }

    void Work()
    {
        nav.SetDestination(job.transform.position);
    }

    void Relax()
    {
        nav.SetDestination(Home.transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Respawn")
        {
            transform.position = Home.position;
        }
        else if(col.gameObject.CompareTag("job"))
        {
            mesh.enabled = false;
            StartCoroutine(DoAShift());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "job")
        {
            mesh.enabled = true;
        }
    }
 

    public void setHome(Transform home)
    {
        Home = home;
    }

    public bool HasJob()
    {
        if (job != null) return true;
        return false;
    }
    
    GameObject FindNearest(GameObject transforms)
    {

        float closest = 1000f;
        GameObject returnObject = gameObject;
        for(int i = 0; i < transforms.GetComponent<Transform>().childCount; i++)
        {
            if(Vector3.Distance(transform.position,transforms.GetComponent<Transform>().GetChild(i).GetComponent<Transform>().position) < closest)
            {
                closest = Vector3.Distance(transform.position, transforms.GetComponent<Transform>().GetChild(i).GetComponent<Transform>().position);
                returnObject = transforms.GetComponent<Transform>().GetChild(i).gameObject;
            }
        }

        return returnObject;
    }

    void FoundJob()
    {
        for(int i = 0; i < jobTargets.transform.childCount; i++)
        {
            if (jobTargets.transform.GetChild(i).GetComponent<jobScript>().HasOpenSpots())
            {
                job = jobTargets.transform.GetChild(i).gameObject;
                job.GetComponent<jobScript>().addAgent(this);
                break;
            }
        }
    }

    public void SetShift(int[] shiftPattern)
    {
        jobSchedule = shiftPattern;
    }
}
