using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlaneBehaviors : MonoBehaviour
{
    private float energy = 1.0f;  // initialize energy to full
    private float threshold = 0.5f;  // set threshold for ener
    static private GreenArrowBehavior sGreenArrow = null;
    static public void SetGreenArrow(GreenArrowBehavior g) { sGreenArrow = g; }
    public int touchedEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Plane: Started");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateColor()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color c = s.color;
        const float delta = 0.2f;  // decrease energy by 20% in each collision
        energy *= 0.8f;  // decrease energy
        c.a = energy;
        s.color = c;
        Debug.Log("Plane: Energy = " + energy);

        if (energy <= threshold)  // check if energy is below threshold
        {
            Destroy(transform.gameObject);  // kills self
            sGreenArrow.OneMoreDestroyed();
            GameManager.sTheGlobalBehavior.mExistingPlaneCount--;
            Debug.Log("count to 9");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GreenUp")
        {
            Debug.Log("Plane hit by GreenUp");
            sGreenArrow.OneMoreTouch();
            sGreenArrow.OneMoreDestroyed();
            Destroy(transform.gameObject);  // kills self
            GameManager.sTheGlobalBehavior.mExistingPlaneCount--;
            Debug.Log("count to 9");

        }
        if (collision.gameObject.tag == "Egg")
        {
            Debug.Log("Plane hit by an Egg");
            UpdateColor();
        }


        //Instantiate(Resources.Load("Prefabs/plane") as GameObject, randomPosition, Quaternion.identity);

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Plane: OnTriggerStay2D");
        Destroy(transform.gameObject);  // kills self

        //Instantiate(Resources.Load("Prefabs/plane") as GameObject, randomPosition, Quaternion.identity);

    }





}

