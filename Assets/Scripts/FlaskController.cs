using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskController : MonoBehaviour {
    public enum FlaskType
    {
        green,
        yellow,
        red,
    }

    private WorldController world;
    private FlaskType flaskType;
    public FlaskType fType
    {
        get { return flaskType; }
    }
    private SpriteRenderer spriteRenderer;
    private GameObject textGameobject;
    private TextMesh text;
    
    // Use this for initialization
	void Start () {
        world = GameObject.Find("World").GetComponent<WorldController>();

        if(world.worldLevel > 1) flaskType = (FlaskType)Random.Range(0,3);
        else flaskType = (FlaskType)Random.Range(0, 2);

        textGameobject = gameObject.transform.GetChild(0).gameObject;
        text = textGameobject.GetComponent<TextMesh>();
        text.text = flaskType.ToString();
        text.color = Random.ColorHSV();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = world.flaskSprites[Random.Range(0,3)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
