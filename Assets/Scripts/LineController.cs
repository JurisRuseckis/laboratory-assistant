using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {
    public enum LineType
    {
        green,
        yellow,
        red,
    }
    public Sprite[] slotSprites;
    private float[] speeds = new float[4] { -16f, -8f, 8f , 16f };


    public float speed;
    private WorldController world;
    public LineType lineType;
    private SpriteRenderer spriteRenderer;
    private GameObject[] slots;
    
    // Use this for initialization
	void Start () {
        speed = speeds[Random.Range(0, 4)];

        world = GameObject.Find("World").GetComponent<WorldController>();

        lineType = (LineType)world.lineColors[Random.Range(0,world.lineColors.Count)];
        world.lineColors.Remove((int)lineType);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = world.allSprites[Random.Range(0,3)];

        slots = new GameObject[7];
        StartCoroutine(Populate());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Populate()
    {
        float time;
        float tmpSpeed = speed;
        if (tmpSpeed < 0) tmpSpeed *= -1;
        if (tmpSpeed == 8f) time = 1.1f;
        else time = 2.1f;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(world.lineSlotPref.gameObject);
            slots[i].GetComponent<SpriteRenderer>().sprite = slotSprites[(int)lineType];
            slots[i].gameObject.GetComponent<LineSlotController>().index = i;
            slots[i].transform.parent = transform;
            //slots[i].transform.position = new Vector2(i*1.4f -5.6f, transform.position.y);
            slots[i].transform.position = new Vector2(transform.position.x - 6f, transform.position.y);
            yield return new WaitForSeconds(time);
        }
    }
}
