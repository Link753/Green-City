using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class values : MonoBehaviour
{
    public float smog = 0f, extraSmog = 0f;
    public float money = 100f;
    float foodlevel, waterlevel, smoggy, time, hour, day, week, year;
    public Text moneyDisplay;
    public GameObject housePrefab, foodPrefab, waterPrefab, jobPrefab, tree, road, ground;
    [SerializeField] GameObject jobsite, equiped;
    GameObject fooddemand, waterdemand, smoglevel;
    Color colour, newColour;
    int totalagents, totaljobs, takenjobs;
    [Header("Timer Attributes")]
    [SerializeField] Text hours;
    [SerializeField] Text days;
    [SerializeField] Text weeks;
    [SerializeField] Text years;


    void Awake()
    {
        fooddemand = GameObject.Find("FoodDemand");
        waterdemand = GameObject.Find("waterDemand");
        smoglevel = GameObject.Find("Smog");
        colour = ground.GetComponent<Renderer>().material.color;
        totaljobs = 0;
        takenjobs = 0;
        hour = 0;
        day = 0;
        year = 0;
    }

    void Update()
    {
        hour += Time.deltaTime;
        runtimer();
        smoggy = smog / 20f;
        newColour = colour * (1f-smoggy);
        ground.GetComponent<Renderer>().material.color = newColour;
        smoglevel.GetComponent<Slider>().value = smoggy;
        smog = GameObject.Find("Environment").GetComponent<Transform>().childCount - GameObject.Find("jobSites").GetComponent<Transform>().childCount;
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
        hours.text = hour.ToString("0");
        if(hour > 23)
        {
            day++;
            hour = 0;
        }
        days.text = day.ToString();
        if(day > 7)
        {
            week++;
            day = 0;
        }
        weeks.text = week.ToString();
        if(week > 52)
        {
            year++;
            year = 0;
        }
        years.text = year.ToString();

    }

    #region GetandSet

    public float GetHour()
    {
        return hour;
    }

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
