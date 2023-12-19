using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();       
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point;
        }

        if (Input.GetButton("Fire1") & transform.childCount == 1 & GameObject.Find("EnvironmentManager").GetComponent<values>().money > 0 & hit.collider.tag == "Respawn")
        {
            Transform g = transform.GetChild(0);
            g.position += new Vector3(0, transform.GetChild(0).localScale.y / 2, 0);
            if (g.transform.tag == "Untagged")
            {
                g.SetParent(GameObject.Find("Environment").GetComponent<Transform>());
                g.GetComponent<Collider>().enabled = true;
                GameObject.Find("EnvironmentManager").GetComponent<values>().money -= 25;
            }

            if(transform.childCount != 0)
            {
                MonoBehaviour[] scripts = transform.GetChild(0).GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = true;
                }
            }
            g.GetComponent<Collider>().enabled = true;

        }
    }
}
