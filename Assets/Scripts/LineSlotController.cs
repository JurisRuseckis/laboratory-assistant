using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSlotController : MonoBehaviour {

    public GameObject flask;
    private float speed;
    private Vector2 leftEnd;
    private Vector2 rightEnd;
    private Vector2 endPoint;
    private Vector2 startPoint;
    private Vector2 initPoint;
    private WorldController world;
    private LineController line;
    private bool is_active;
    private bool first_loop;
    private float moving_duration;
    private float loop_duration;
    public float index;

    // Use this for initialization
    void Start () {
        world = GameObject.Find("World").GetComponent<WorldController>();
        is_active = false;
        first_loop = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (is_active)
        {
            transform.position = Vector2.Lerp(initPoint, endPoint, loop_duration / moving_duration);
            loop_duration += Time.deltaTime;
            if (!isEmpty())flask.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
            if (transform.position.x == endPoint.x) LoopRestart();
        }
        else
        {
            if (transform.parent == null) return;

            line = transform.parent.GetComponent<LineController>();
            speed = line.speed;

            leftEnd = new Vector2(line.transform.position.x - 6f, line.transform.position.y);
            rightEnd = new Vector2(line.transform.position.x + 6f, line.transform.position.y);

            initPoint = transform.position;

            float time_per_unit = speed / 8;
            if (speed > 0)
            {
                endPoint = rightEnd;
                startPoint = leftEnd;
                moving_duration = speed;
                //moving_duration = 0.1f + index*time_per_unit/2;
            }
            else
            {
                endPoint = leftEnd;
                startPoint = rightEnd;
                moving_duration = -speed;
                //moving_duration = 4.1f - index*time_per_unit/2;
            }
            is_active = true;
        }
	}

    private void OnMouseDown()
    {
        GameObject active_flask = world.stackObj.GetComponent<StackController>().get_active_flask();
        if (active_flask == null) return;

        FlaskController flaskController = active_flask.GetComponent<FlaskController>();
        string lineType = line.lineType.ToString();
        string flaskType = flaskController.fType.ToString();
        if (isEmpty() 
            && lineType == flaskType)
        {
            flaskController.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
            flaskController.transform.SetParent(transform);
            flask = active_flask;
        }
        else
        {
            Object.Destroy(active_flask);
            world.lose_life();
        }
    }

    void LoopRestart()
    {
        transform.position = startPoint;
        initPoint = startPoint;
        if (first_loop)
        {
            if (speed > 0) moving_duration = speed;
            else moving_duration = -speed;
            first_loop = false;
        }

        loop_duration = 0;

        if (Random.Range(0, 2) == 1)
        {
            if (isEmpty())
            {
                flask = Instantiate(world.flaskPref).gameObject;
                flask.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
                flask.transform.SetParent(transform);
            }
        }
        else
        {
            if (!isEmpty()) Object.Destroy(flask);
        }
    }

    public bool isEmpty()
    {
        if (flask == null) return true;
        else return false;
    }
}
