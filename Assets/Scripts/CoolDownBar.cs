using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownBar : MonoBehaviour
{
    RectTransform rec;
    float initialWid, initialY;
    public float timer = 0;
    const float cooldownTime = 0.2f;

    public bool isActive = false;

    void Awake()
    {
        rec = GetComponent<RectTransform>();
        initialWid = rec.sizeDelta.x;
        initialY = rec.sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && timer <= cooldownTime)
            CoolDown();
        else
        {
            timer = 0;
            rec.sizeDelta = Vector2.zero;
            isActive = false;
        }
    }

    public void CoolDown()
    {
        timer += Time.deltaTime;
        float scale = timer / cooldownTime;
        rec.sizeDelta = new Vector2(initialWid * (1-scale), initialY);
    }

}
