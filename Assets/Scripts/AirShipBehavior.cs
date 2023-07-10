using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipBehavior : MonoBehaviour
{
    private GameObject mTarget = null;
    private const float kTurnRate = 0.03f;
    private const float kMySpeed = 20f;
    private const float kVeryClose = 25f;
    private int currentTarget = 0;

    static private GreenArrowBehavior sGreenArrow = null;
    static public void SetGreenArrow(GreenArrowBehavior g) { sGreenArrow = g; }


    private GameObject[] targets = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        targets[0] = GameObject.Find("A");
        targets[1] = GameObject.Find("B");
        targets[2] = GameObject.Find("C");
        targets[3] = GameObject.Find("D");
        targets[4] = GameObject.Find("E");
        targets[5] = GameObject.Find("F");
        mTarget = targets[Random.Range(0, 6)];
    }

    // Update is called once per frame
    void Update()
    {
        PointAtPosition(mTarget.transform.localPosition, kTurnRate * Time.smoothDeltaTime);
        transform.localPosition += kMySpeed * Time.smoothDeltaTime * transform.up;

        CheckTargetPosition();
    }

    private void ComputeNewTargetPosition()
    {
        mTarget = targets[currentTarget];
    }

    private void CheckTargetPosition()
    {
        // Access the GameManager
        float dist = Vector3.Distance(mTarget.transform.localPosition, transform.localPosition);
        if (dist <= kVeryClose)
        {
            UpdateCurrentTarget();
            ComputeNewTargetPosition();
        }
    }

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.localPosition;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    private void UpdateCurrentTarget()
    {
        if (sGreenArrow.sequencing == true)
        {
            if (currentTarget < 5)
            {
                currentTarget++;
            }
            else
            {
                currentTarget = 0;
            }
        }
        else
        {
            int randomNumber = Random.Range(0, 6);
            currentTarget=randomNumber;
        }

    }

}
