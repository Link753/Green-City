using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class values : MonoBehaviour
{
    public float smog = 0f, extraSmog = 0f;
    public float money = 100f;
    float foodlevel, waterlevel, smoggy, time, minute, hour, day;
    public Text moneyDisplay;
    public GameObject housePrefab, foodPrefab, waterPrefab, jobPrefab, tree, road, ground;
    [SerializeField] GameObject jobsite, equiped;
    GameObject fooddemand, waterdemand, smoglevel;
    Color colour, newColour;
    int totalagents, totaljobs, takenjobs;
    [Header("Timer Attributes")]
    [SerializeField] Text seconds;
    [SerializeField] Text minutes;
    [SerializeField] Text hours;
    [SerializeField] Text days;


    void Awake()
    {
        fooddemand = GameObject.Find("FoodDemand");
        waterdemand = GameObject.Find("waterDemand");
        smoglevel = GameObject.Find("Smog");
        colour = ground.GetComponent<Renderer>().material.color;
        totaljobs = 0;
        takenjobs = 0;
        time = 0;
        minute = 0;
        day = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        runtimer();
        smoggy = smog / 20f;
        newColour = colour * (1f-smoggy);
        ground.GetComponent<Renderer>().material.color = newColour;
        smoglevel.GetComponent<Slider>().value = smoggy;
        if(GameObject.Find("Houses").GetComponent<Transform>().childCount != 0)
        {
            waterlevel = GameObject.Find("waterpoints").GetComponent<Transform>().childCount / GameObject.Find("Houses").GetComponent<Transform>().childCount;
            foodlevel = GameObject.Find("foodpoints").GetComponent<Transform>().childCount / GameObject.Find("Houses").GetComponent<Transform>().childCount;
            //jobsites = (float)GameObject.Find("jobSites").GetComponent<Transform>().childCount / (float)GameObject.Find("Houses").GetComponent<Transform>().childCount;
            fooddemand.GetComponent<Slider>().value = foodlevel;
            waterdemand.GetComponent<Slider>().value = waterlevel;
            //jobavail.GetComponent<Slider>().value = jobsites;
        }
        smog = GameObject.Find("Environment").GetComponent<Transform>().childCount - (GameObject.Find("jobSites").GetComponent<Transform>().childCount + GameObject.Find("foodpoints").GetComponent<Transform>().childCount
            + GameObject.Find("waterpoints").GetComponent<Transform>().childCount);
        smog = -smog;
        moneyDisplay.text = "£" + money.ToString("0.00");

        
    }

    public void ChooseItem(GameObject prefab)
    {
        if (equiped.transform.childCount == 0)
        {
            GameObject g = Instantiate(prefab, equiped.transform);
            g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y + g.transform.localScale.y, g.transform.position.z);
            MonoBehaviour[] scripts = g.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
            g.GetComponent<Collider>().enabled = false;
            g.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void runtimer()
    {
        seconds.text = time.ToString("0");
        if(time > 60)
        {
            minute++;
            time = 0;
        }
        minutes.text = minute.ToString();
        if(minute > 60)
        {
            hour++;
            minute = 0;
        }
        hours.text = hour.ToString();
        if(hour > 24)
        {
            day++;
            hour = 0;
        }
        days.text = day.ToString();

    }

    #region GetandSet

    public float GetMoney()
    {
        return money;
    }

    public void SetMoney(float m)
    {
        money += m;
    }

    public float GetSmog()
    {
        return smog;
    }

    public void SetSmog(float s)
    {
        smog = s;
    }

    public void addAgent()
    {
        totalagents++;
    }

    public int GetAgentTotal()
    {
        return totalagents;
    }

    public void addjobs(int a)
    {
        totaljobs += a;
    }

    public int Getjobs()
    {
        return totaljobs;
    }

    public void addtakenjobs()
    {
        takenjobs++;
    }

    public int Gettakenjobs()
    {
        return totaljobs - takenjobs;
    }

    #endregion
}
