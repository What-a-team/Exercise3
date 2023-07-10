using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required to work with UI, e.g., Text

public class GameManager : MonoBehaviour
{
    public static GameManager sTheGlobalBehavior = null; // Single pattern
    public int mExistingPlaneCount = 0;
    public int mTotalPlaneCount = 0;


    public GreenArrowBehavior mHero = null;  // must set in the editor

    // for display egg count
    public Text mEggCountEcho = null;
    public Text mPlayModeEcho = null;
    public Text mTouchedEnemyEcho = null;
    public Text mDestroyedEnemyEcho = null;





    // Start is called before the first frame update
    void Start()
    {
        mEggCountEcho = GameObject.Find("EggCountEcho").GetComponent<Text>();
        mPlayModeEcho = GameObject.Find("PlayModeEcho").GetComponent<Text>();
        mTouchedEnemyEcho = GameObject.Find("TouchedEcho").GetComponent<Text>();
        mDestroyedEnemyEcho = GameObject.Find("EnemyEcho").GetComponent<Text>();



        GameManager.sTheGlobalBehavior = this;  // Singleton pattern

        Debug.Assert(mEggCountEcho != null);    // Assume setting in the editor!
        Debug.Assert(mPlayModeEcho != null);    // Assume setting in the editor!
        Debug.Assert(mTouchedEnemyEcho != null);    // Assume setting in the editor!
        Debug.Assert(mDestroyedEnemyEcho != null);    // Assume setting in the editor!


        Debug.Assert(mHero != null);


        //随机生成飞机
        Camera mainCamera = Camera.main;
        for (int i = 0; i < 10; i++)
        {

            Vector3 randomPosition = new Vector3(
                            Random.Range(mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x),
                            Random.Range(mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y, mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y),
                            0);
            randomPosition *= 0.9f;
            Instantiate(Resources.Load("Prefabs/plane") as GameObject, randomPosition, Quaternion.identity);
            mExistingPlaneCount = 10;
        }
        Debug.Log("count to 10");


        // Connect up everyone who needs to know about each other
        EggBehavior.SetGreenArrow(mHero);
        PlaneBehaviors.SetGreenArrow(mHero);



        // Notice the symantics: this is a call to class method (NOT instance method)
    }

    // Update is called once per frame


    void Update()
    {
        Camera mainCamera = Camera.main;
        mEggCountEcho.text = mHero.EggStatus();
        mPlayModeEcho.text = mHero.PlayModeStatus();
        mTouchedEnemyEcho.text = mHero.touchedEnemyStatus();
        mDestroyedEnemyEcho.text = mHero.destroyedEnemyStatus();




        while (mExistingPlaneCount < 10)
        {
            Vector3 randomPosition = new Vector3(
                           Random.Range(mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x),
                           Random.Range(mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y, mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y),
                           0);
            randomPosition *= 0.9f;
            Instantiate(Resources.Load("Prefabs/plane") as GameObject, randomPosition, Quaternion.identity);
            mExistingPlaneCount++;
            if(mExistingPlaneCount == 10)
            {
                break;
            }
        }

    }

}
