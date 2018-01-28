using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour {

    [System.Serializable]
    public struct level
    {
        int flasks;
    }

    public Sprite[] flaskSprites;
    public enum type { green, yellow, red };

    // playerinfo
    public int lives;

    // prefs
    public Transform flaskPref;
    public Transform heapPref;
    public Transform stackPref;
    public Transform stackSlotPref;
    public Transform linePref;
    public Transform lineSlotPref;

    // instantiated transforms
    public Transform heapObj;
    public Transform stackObj;
    public Transform[] lineObjs;

    // level gameObjs
    public Transform mistakes;
    public Transform timer;
    public Transform explosion;

    // level variables
    public int worldLevel;
    public int lineCount;
    public float time;

    public int startingFlasks;
    public int totalFlasks;
    public int maxflasks;
    public float flaskRate;

    public int stackSize = 2;
    public List<int> lineColors = new List<int>();

    private bool initialised;

    // position vectors 
    private Vector2[] linePositions = new Vector2[3]
    {
        new Vector2(0, -0.6f),
        new Vector2(0, -3.6f),
        new Vector2(0, -6.6f),
        //new Vector2(0, -5.0f),
        //new Vector2(0, -6.6f)
    };


    // Use this for initialization
    void Start () {
        //worldLevel = 3;
        //time = 60f;
        timer.GetChild(0).GetComponent<TextMesh>().text = time.ToString();

        lineColors.Add(0);
        lineColors.Add(1);
        if(worldLevel > 1)lineColors.Add(2);

        //startingFlasks = 10;
        //totalFlasks = 12;
        //maxflasks = 25;

        //flaskRate = 2.0f;

        initialised = false;
        heapObj = Instantiate(heapPref);
        stackObj = Instantiate(stackPref);
        //lineCount = 3;
        lives = 3;
        lineObjs = new Transform[lineCount];

        
        for(int i = 0; i<lineCount; i++)
        {
            lineObjs[i] = Instantiate(linePref);
            lineObjs[i].transform.position = linePositions[i];
        }
        initialised = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (initialised)
        {
            int flaskCount = heapObj.GetComponent<HeapController>().flaskCount;
            int flasksToAdd = heapObj.GetComponent<HeapController>().flasksToAdd;
            if (flaskCount >= maxflasks || time <=0)
            {
                gameOver();
            }
            if ( flaskCount == 0 && flasksToAdd == 0 )
            {
                win();
            }
            time -= Time.deltaTime;
            timer.GetChild(0).GetComponent<TextMesh>().text = time.ToString().Substring(0,4);
        }
	}

    public void lose_life()
    {
        mistakes.transform.GetChild(lives - 1).gameObject.SetActive(true);
        if (lives == 1) gameOver();
        lives--;

    }

    private void gameOver()
    {
        StartCoroutine(LabExplode());
    }

    public void win()
    {
        if (worldLevel == 3)
        {
            SceneManager.LoadScene(3);
        }
        else if (worldLevel == 1)
        {
            SceneManager.LoadScene(6);
        }
        else if (worldLevel == 2)
        {
            SceneManager.LoadScene(7);
        }
    }

    IEnumerator LabExplode()
    {
        for (int i = 0; i < 3; i++)
        {
            if(i != 0) explosion.GetChild(i-1).gameObject.SetActive(false);
            explosion.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
        }
        SceneManager.LoadScene(2);
    }
}
