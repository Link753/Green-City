using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class jobScript : MonoBehaviour
{
    values money;
    Transform jobsites;
    List<AgentScript> agents;
    int maxEmployees, curEmployees;
    bool hasNightShifts;

    #region GameLoop

    IEnumerator work()
    {
        yield return new WaitForSeconds(5);
        money.SetMoney(money.GetMoney() + 50);
    }

    private void Awake()
    {
        jobsites = GameObject.Find("jobSites").transform;
        money = GameObject.Find("EnvironmentManager").GetComponent<values>();
        if(Random.Range(0,4) % 2 == 0)
        {
            hasNightShifts = true;
        }
        else
        {
            hasNightShifts = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(jobsites);
        money.SetMoney(money.GetMoney() - 50);
        maxEmployees = 4;
        curEmployees = 0;
        agents = new List<AgentScript>();
        money.addjobs(maxEmployees);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[] AssignShift()
    {
        int[] decidedShift = new int[2];
        if (!hasNightShifts)
        {
            decidedShift[0] = Random.Range(7, 9);
            decidedShift[1] = Random.Range(5, 7);
        }

        return decidedShift;
    }
    #endregion

    #region PublicMethods

    public void addAgent(AgentScript agent)
    {
        agents.Add(agent);
        money.addtakenjobs();
        agent.SetShift(AssignShift());
    }

    public void ClockIn(AgentScript agent)
    {
        if (agents.Contains(agent))
        {
            StartCoroutine(work());
        }
    }

    public bool HasOpenSpots()
    {
        if(maxEmployees - curEmployees != 0)
        {
            return true;
        }
        return false;
    }

    public int GetOpenSpots()
    {
        return maxEmployees - curEmployees;
    }

    #endregion
}
