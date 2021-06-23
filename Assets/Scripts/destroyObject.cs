using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour {

    ElementObject[] layerObjects;
    [SerializeField]
    GameObject stupidsolution;

    public void goToSleep()
    {
        Vector3 dumblocation = new Vector3(-500, -500, 0);
        layerObjects = FindObjectsOfType(typeof(ElementObject)) as ElementObject[];
        if (layerObjects.Length > 0)
        {
            for (int count = 0; count < layerObjects.Length; count++)
            {
                if (layerObjects[count].gameObject.layer == this.gameObject.layer)
                {
                    Destroy(layerObjects[count].gameObject);
                }
            }
        }
        GameObject dumbthing = (GameObject)Instantiate(stupidsolution, dumblocation, Quaternion.identity);
    }
}
