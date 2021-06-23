using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreate : MonoBehaviour {

    private bool onlyonce;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 pos = touch.position;
       
        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("firing");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out hit) && (hit.collider.GetComponent<ElementButton>()))
            {
                Debug.Log("got em");
                hit.collider.GetComponent<ElementButton>().Select(hit.point);
            }
        }
    }
}
