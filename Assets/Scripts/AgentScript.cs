using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;

public class AgentScript : MonoBehaviour
{
    [SerializeField] float food, water, health, fatigue;
    values Smogvalue;
    GameObject job, foodtarget, watertarget, jobTargets;
    float speed;
    AgentState state = new();
    NavMeshAgent nav;
    Transform Home;
    MeshRenderer mesh;

    enum AgentState
    {
        REST,
        WORK,
        RELAX
    }

    private void Awake()
    {
        foodtarget = GameObject.Find("foodpoints");
        watertarget = GameObject.Find("waterpoints");
        jobTargets = GameObject.Find("jobSites");
        state = AgentState.REST;
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
        food = 25f;
        water = 25f;
        health = 100f;
        fatigue = 0.0f;
        nav = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshRenderer>();
        Smogvalue = GameObject.Find("EnvironmentManager").GetComponent<values>();
        Smogvalue.addAgent();
    }

    // Update is called once per frame
    void Update() 
    {
        nav.speed = speed - Smogvalue.GetSmog();
        food -= Time.deltaTime;
        water -= Time.deltaTime;

        if (job != null)
        {
            FoundJob();
        }

        switch (state) 
        {
            case AgentState.REST:
                break;
            case AgentState.WORK:
                break;
            case AgentState.RELAX:
                break;
        }


        if (food < 10f)
        {
            nav.SetDestination(FindNearest(foodtarget).transform.position);
            fatigue -= 0.5f * Time.deltaTime;
        }

        if(water < 10f)
        {
            nav.SetDestination(FindNearest(watertarget).transform.position);
            fatigue -= 0.2f * Time.deltaTime;
        }

        if(food > 10f && water > 10f)
        {
            nav.SetDestination(job.transform.position);
        }

        if(food <= 0 && water <= 0)
        {
            health -= Time.deltaTime;
        }

        if(health < 0)
        {
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "food")
        {
            food += 35;
            Smogvalue.SetSmog(Smogvalue.GetSmog() + 0.5f);
        }
        else if(col.gameObject.tag == "water")
        {
            water += 30;
            Smogvalue.SetSmog(Smogvalue.GetSmog() + 0.5f);
        }
        else if(col.gameObject.tag == "Respawn")
        {
            transform.position = Home.position;
        }
        else if(col.gameObject.tag == "job")
        {
            mesh.enabled = false;
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
}
