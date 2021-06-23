using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementObject : MonoBehaviour {
    


    ObjectDrag drag;

    Element myElement;
    
    ElementsPool myElePool;

    public LayerMask mymask;

    [SerializeField]
    SpriteRenderer mySprite;

    [SerializeField]
    private float pickupSphereSize = 1f;
    [SerializeField]
    private float deathSphere = 15f;

    private Collider[] nearbyCombos = new Collider[5];
    bool onlyOnce;
    bool first_creation;


    public void Init(Element incEle, ElementsPool IncPool)
    {
        myElePool = IncPool;
        myElement = incEle;

        drag = this.gameObject.GetComponent<ObjectDrag>();
        first_creation = true;

        mySprite.sprite = ImagePool.Instance.getSprite(incEle.getID);
        Debug.Log(mySprite.sprite);
        Vector3 newpos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = newpos;
    }

    void FixedUpdate()
    {
        if (drag.isdragging)
        {
            int hitThings = Physics.OverlapSphereNonAlloc(this.gameObject.transform.position, pickupSphereSize, nearbyCombos, mymask);
            if (hitThings > 0)
            {
                for (int count = 0; count < hitThings; count++)
                {
                    if (nearbyCombos[count].GetComponent<ElementObject>() != this.gameObject.GetComponent<ElementObject>())
                    {
                            Debug.Log("trying to combine");
                            myElePool.Combine(this, nearbyCombos[count].GetComponent<ElementObject>());
                            break;
                    }
                }
            }
            onlyOnce = false;
        }

        
           else if (!drag.isdragging)
            {
                if (!onlyOnce)
                {
                    int hitThings = Physics.OverlapSphereNonAlloc(this.gameObject.transform.position, deathSphere, nearbyCombos, mymask);
                    if (hitThings > 0)
                    {
                        for (int count = 0; count < hitThings; count++)
                        {
                            Debug.Log("trying to die");
                            if (nearbyCombos[count].GetComponent<destroyObject>())
                            {
                                Destroy(gameObject);
                                break;
                            }
                        }
                    }
                }
                onlyOnce = true;
            }
        
    }

    public Element GetElement { get { return myElement; } }
}
