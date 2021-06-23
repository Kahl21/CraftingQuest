using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementButton : MonoBehaviour {

    Element myElement;
    public Text myName;
    ElementsPool myElePool;

    [SerializeField]
    Image myimage;
    

    public void Init(Element incEle, ElementsPool IncPool)
    {
        myElePool = IncPool;
        myElement = incEle;
        myName.text = myElement.GetName;
        myimage.sprite = ImagePool.Instance.getSprite(incEle.getID);
        this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    public void Select(Vector3 des)
    {
        
        myElePool.SelectElement(myElement, this, des);
      
    }

    public Element GetElement { get { return myElement; } }
}
