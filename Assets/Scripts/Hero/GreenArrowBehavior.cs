using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenArrowBehavior : MonoBehaviour
{
    public bool mFollowMousePosition = true;
    public float mHeroSpeed = 20f;
    public float mHeroRotateSpeed = 90f / 2f; // 90-degrees in 2 seconds
    public float shootInterval = 0.2f;
    public bool canShoot = false;
    public bool sequencing = true;


    public int mTotalEggCount = 0;
    public int touchedEnemyCount = 0;
    public int destroyedEnemyCount = 0;

    public CoolDownBar bar;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.localPosition;

        if (Input.GetKeyDown(KeyCode.M))
            mFollowMousePosition = !mFollowMousePosition;

        if (mFollowMousePosition)
        {
            p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            p.z = 0f;  // <-- this is VERY IMPORTANT!
            // Debug.Log("Screen Point:" + Input.mousePosition + "  World Point:" + p);
        }
        else
        {
            p += ((mHeroSpeed * Time.smoothDeltaTime) * transform.up);
            if (Input.GetKey(KeyCode.W))
                mHeroSpeed += 10 * Time.smoothDeltaTime;
            if (Input.GetKey(KeyCode.S))
                mHeroSpeed -= 10 * Time.smoothDeltaTime;

            if (Input.GetKey(KeyCode.A))
                transform.Rotate(transform.forward, mHeroRotateSpeed * Time.smoothDeltaTime);

            if (Input.GetKey(KeyCode.D))
                transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
            /*
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();  // Try to access the CameraSupport component on the MainCamera
            if (s != null)   // if main camera does not have the script, this will be null
            {
                Bounds myBound = GetComponent<Renderer>().bounds;  // this is the bound of the collider defined on GreenUp
                CameraSupport.WorldBoundStatus status = s.CollideWorldBound(myBound);

                if (status != CameraSupport.WorldBoundStatus.Inside)
                {
                    Debug.Log("Touching the world edge: " + status);
                    // now let's re-spawn ourself somewhere in the world
                    p.x = s.GetWorldBound().min.x + Random.value * s.GetWorldBound().size.x;
                    p.y = s.GetWorldBound().min.y + Random.value * s.GetWorldBound().size.y;
                }
            }
            */
        }

        transform.localPosition = p;

        // Now spawn an egg when space bar is hit
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canShoot = true;
            StartCoroutine("ShootBullet");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            canShoot = false;
            StopCoroutine("ShootBullet");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            sequencing = !sequencing;
        }
    }

    IEnumerator ShootBullet()
    {
        while (canShoot)
        {
            fire();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void fire(){
        bar.isActive = true;
        GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject, transform.position, transform.rotation); // Prefab MUST BE locaed in Resources/Prefab folder!
        mTotalEggCount++;
    }


    public void OneLessEgg() { mTotalEggCount--;  }
    public void OneMoreTouch() { touchedEnemyCount++; }
    public void OneMoreDestroyed() { destroyedEnemyCount++; }



    public string EggStatus() { return "EGG:on screen: " + mTotalEggCount; }
    
    public string PlayModeStatus() {
        if (mFollowMousePosition == true)
        {
            return "HERO:Drive(Mouse)";
        }
        else 
        {
            return "HERO:Drive(Keyboard)";
        }
        
    }

    public string touchedEnemyStatus() { return "TouchedEnemy:" + touchedEnemyCount; }
    public string destroyedEnemyStatus() { return "ENEMY:Count:10,Destroyed:" + destroyedEnemyCount; }

    public string WaypointsModeStatus()
    {
        if (sequencing == true)
        {
            return "WAYPOINTS:(sequencing)";
        }
        else
        {
            return "WAYPOINTS:(random)";
        }
    }





}
