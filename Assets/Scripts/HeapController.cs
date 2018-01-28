using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapController : MonoBehaviour {

    private WorldController world;
    private List<GameObject> flasks;
    private Transform flaskPref;
    private float xCoord;
    private float yCoord;
    private float flaskRate;
    private int _flasksToAdd;
    private TextMesh flaskCountTextObject;
    public int flaskCount
    {
        get { return flasks.Count; }
    }
    public int flasksToAdd
    {
        get { return _flasksToAdd; }
    }

    // Use this for initialization
    void Start () {
        xCoord = gameObject.transform.position.x;
        yCoord = gameObject.transform.position.y;

        flasks = new List<GameObject>();

        world = GameObject.Find("World").GetComponent<WorldController>();
        flaskPref = world.flaskPref;
        flaskRate = world.flaskRate;
        _flasksToAdd = world.totalFlasks;

        flaskCountTextObject = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();

        Populate(world.startingFlasks);
        InvokeRepeating("AddOneFlask", flaskRate, flaskRate);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Populate(int n)
    {
        for (int i = 0; i < n; i++)
        {
            AddOneFlask();
        }
    }

    void AddOneFlask()
    {
        if (_flasksToAdd == 0) return;

        Transform tmp = Instantiate(flaskPref);
        tmp.transform.position = new Vector2(xCoord, yCoord);
        tmp.transform.SetParent(transform);
        flasks.Add(tmp.gameObject);
        flaskCountTextObject.text = flasks.Count.ToString() + " / " + world.maxflasks;
        _flasksToAdd -= 1;
    }

    void Clear()
    {
        while(flasks.Count > 0)
        {
            GameObject tmp = flasks[0];
            flasks.Remove(tmp);
            Object.Destroy(tmp);
        }
    }

    public GameObject get_random_flask()
    {
        if (flasks.Count == 0) return null;
        GameObject tmp = flasks[Random.Range(0, flasks.Count)];
        flasks.Remove(tmp);
        flaskCountTextObject.text = flasks.Count.ToString() + " / " + world.maxflasks;
        return tmp;
    }
}
