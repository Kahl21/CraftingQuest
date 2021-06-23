using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldDrag : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 scanPos;
    private Vector3 offset;


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
            scanPos = gameObject.transform.position;
     
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);

        offset = scanPos - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }
}
