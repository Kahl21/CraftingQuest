using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour {

    private float dist;
    [HideInInspector]
    public bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    
    Vector3 v3;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        StartCoroutine(MyUpdate());
    }
	
	// Update is called once per frame
	IEnumerator MyUpdate () {
        while (true)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                switch (Input.GetTouch(i).phase)
                {
                    case TouchPhase.Began:
                        StartingTouch(Input.GetTouch(i));
                        break;
                    case TouchPhase.Moved:
                        DraggingTouch(Input.GetTouch(i));
                        break;
                    case TouchPhase.Stationary:
                        DraggingTouch(Input.GetTouch(i));
                        break;
                    case TouchPhase.Ended:
                        StopTouch(Input.GetTouch(i));
                        break;
                    case TouchPhase.Canceled:
                        StopTouch(Input.GetTouch(i));
                        break;
                    default:
                        Debug.Log("something broke homie");
                        break;
                }
            }
            yield return new WaitForEndOfFrame();
        }     
    }

    private void StartingTouch(Touch touch)
    {
        Vector3 pos = touch.position;

        Debug.Log("firing");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit) && (hit.collider.GetComponent<ElementObject>()))
        {
            Debug.Log("ima draggin");
            toDrag = hit.transform;
            dist = hit.transform.position.z - Camera.main.transform.position.z;
            v3 = new Vector3(pos.x, pos.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            offset = toDrag.position - v3;
            dragging = true;
        }
    }
    
    private void DraggingTouch(Touch touch)
    {
        if (dragging)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;
        }
    }

    private void StopTouch(Touch touch)
    {
        if (dragging)
        {
            dragging = false;
        }
    }
    public bool isdragging { get { return dragging; } }
}