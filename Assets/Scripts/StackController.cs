using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour {

    private WorldController world;
    private HeapController heap;

    private float xCoord;
    private float yCoord;

    private GameObject[] flaskSlots;
    private Transform stackSlotPref;
    private int activeSlot;
    
    // Use this for initialization
	void Start () {
        world = GameObject.Find("World").GetComponent<WorldController>();
        heap = world.heapObj.gameObject.GetComponent<HeapController>();
        stackSlotPref = world.stackSlotPref;

        xCoord = gameObject.transform.position.x;
        yCoord = gameObject.transform.position.y;

        flaskSlots = new GameObject[world.stackSize];
        for(int i = 0; i<flaskSlots.Length; i++)
        {
            flaskSlots[i] = Instantiate(stackSlotPref).gameObject;
            flaskSlots[i].transform.position = new Vector2(xCoord - 1.6f + i*3.2f , yCoord - 1f);
            flaskSlots[i].transform.SetParent(transform);
            flaskSlots[i].transform.GetComponent<StackSlotController>().index = i;
        }
        activeSlot = 0;
        flaskSlots[activeSlot].transform.GetComponent<StackSlotController>().is_active = true;
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < flaskSlots.Length; i++)
        {
            StackSlotController stackSlot = flaskSlots[i].GetComponent<StackSlotController>();
            if (stackSlot.isEmpty())
            {
                stackSlot.flask = pull_flask();
                if (stackSlot.flask != null)
                {
                    stackSlot.flask.transform.position = stackSlot.transform.position;
                    stackSlot.flask.transform.SetParent(stackSlot.transform);
                }
            }
        }
	}

    GameObject pull_flask()
    {
        return heap.get_random_flask();
    }

    public GameObject get_active_flask()
    {
        StackSlotController controller = flaskSlots[activeSlot].GetComponent<StackSlotController>();

        GameObject tmp = controller.flask;
        controller.flask = null;
        return tmp;
    }

    public void change_active(int index)
    {
        flaskSlots[activeSlot].transform.GetComponent<StackSlotController>().is_active = false;
        activeSlot = index;
    }
}
