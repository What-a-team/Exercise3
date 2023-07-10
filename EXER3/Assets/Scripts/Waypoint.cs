using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Waypoint : MonoBehaviour
{
    //The number of hits that waypoint can take before it is destroyed
    public int hitPoints = 4;
    private bool visible = true;
    //The original position of the Waypoints
    private Vector3 WaypointA = new Vector3(-70, 70, 0);
    private Vector3 WaypointB = new Vector3(70, -70, 0);
    private Vector3 WaypointC = new Vector3(30, 0, 0);
    private Vector3 WaypointD = new Vector3(-70, -70, 0);
    private Vector3 WaypointE = new Vector3(70, 70, 0);
    private Vector3 WaypointF = new Vector3(-30, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        GameObject greenArrow = GameObject.Find("GreenUp");
    }

    // Update is called once per frame
    void Update()
    {
        //if enter "H" button make all waypoints invisible
        if (Input.GetKeyDown(KeyCode.H))
        {
            visible = !visible;
        }
        //if waypoints are visible, make them visible
        if (visible) GetComponent<SpriteRenderer>().enabled = true;
        //if waypoints are invisible, make them invisible
        else if(!visible)GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Egg")
        {
            hitPoints--;
            if(hitPoints == 0)
            {
                WayPointResprawn();//resprawn the waypoint
            }
            UpdateColor();
        }
    }
    private void WayPointResprawn()//re-position itself in a new point that is randomly located at + or - 15 units in both X and Y from the initial position of the waypoint.
    {
        GameObject newWaypoint = null;
        //based on different name of the waypoint to load a different waypoint
        switch(transform.name)
        {
            //if the name of the waypoint is WaypointA or WaypointA(Clone), load the WaypointA prefab
            case "WaypointA"or"WaypointA(Clone)":
                newWaypoint = Instantiate(Resources.Load("Prefabs/WaypointA", typeof(GameObject))) as GameObject;
                newWaypoint.transform.localPosition = new Vector3(newWaypoint.transform.position.x + Random.Range(-15, 15), newWaypoint.transform.position.y + Random.Range(-15, 15), newWaypoint.transform.position.z);
                break;
            case "WaypointB"or"WaypointB(Clone)":
                newWaypoint = Instantiate(Resources.Load("Prefabs/WaypointB", typeof(GameObject))) as GameObject;
                newWaypoint.transform.localPosition = new Vector3(newWaypoint.transform.position.x + Random.Range(-15, 15), newWaypoint.transform.position.y + Random.Range(-15, 15), newWaypoint.transform.position.z);
                break;
            case "WaypointC"or"WaypointC(Clone)":
                newWaypoint = Instantiate(Resources.Load("Prefabs/WaypointC", typeof(GameObject))) as GameObject;
                newWaypoint.transform.localPosition = new Vector3(newWaypoint.transform.position.x + Random.Range(-15, 15), newWaypoint.transform.position.y + Random.Range(-15, 15), newWaypoint.transform.position.z);
                break;
            case "WaypointD"or"WaypointD(Clone)":
                newWaypoint = Instantiate(Resources.Load("Prefabs/WaypointD", typeof(GameObject))) as GameObject;
                newWaypoint.transform.localPosition = new Vector3(newWaypoint.transform.position.x + Random.Range(-15, 15), newWaypoint.transform.position.y + Random.Range(-15, 15), newWaypoint.transform.position.z);
                break;
            case "WaypointE"or"WaypointE(Clone)":
                newWaypoint = Instantiate(Resources.Load("Prefabs/WaypointE", typeof(GameObject))) as GameObject;
                newWaypoint.transform.localPosition = new Vector3(newWaypoint.transform.position.x + Random.Range(-15, 15), newWaypoint.transform.position.y + Random.Range(-15, 15), newWaypoint.transform.position.z);
                break;
            case "WaypointF"or"WaypointF(Clone)":
                newWaypoint = Instantiate(Resources.Load("Prefabs/WaypointF", typeof(GameObject))) as GameObject;
                newWaypoint.transform.localPosition = new Vector3(newWaypoint.transform.position.x + Random.Range(-15, 15), newWaypoint.transform.position.y + Random.Range(-15, 15), newWaypoint.transform.position.z);                break;
        }
        //Destory itself then create a new one in a new point that is randomly located at + or - 15 units in both X and Y from the initial position of the waypoint.
        //Debug.Log("Waypoint: " + transform.name + " was destroyed");
        Destroy(transform.gameObject);
        
    }
    private void UpdateColor()//loses 25% of its opacity
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color c = s.color;
        c.a *=0.75f;
        s.color = c;
    }
}
