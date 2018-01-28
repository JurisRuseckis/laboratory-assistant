using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSlotController : MonoBehaviour {

    public GameObject flask;
    private SpriteRenderer spriteRenderer;
    public Sprite activeSprite;
    public Sprite defaultSprite;
    public bool is_active;
    public int index;

    // Use this for initialization
    void Start () {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (is_active)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else
        {
            spriteRenderer.sprite = defaultSprite;
        }
	}

    private void OnMouseDown()
    {
        if (!is_active)
        {
            is_active = true;
            transform.parent.GetComponent<StackController>().change_active(index);
        }
    }

    public bool isEmpty()
    {
        if (flask == null) return true;
        else return false;
    }
}
