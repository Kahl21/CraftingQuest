using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuingFunctionality : MonoBehaviour {

    [SerializeField]
    GameObject Main_Menu;
    [SerializeField]
    GameObject Free_Mode_Menu;
    [SerializeField]
    GameObject Credits_Menu;
    ElementObject[] layerObjects;

	// Use this for initialization
	void Start () {

	}

    public void ToMainMenu()
    {
        Free_Mode_Menu.SetActive(false);
        Main_Menu.SetActive(true);
        layerObjects = FindObjectsOfType(typeof(ElementObject)) as ElementObject[];
        for (int count = 0; count < layerObjects.Length; count++)
        {
            if (layerObjects[count].gameObject.layer == 8)
            {
                Destroy(layerObjects[count].gameObject);
            }
        }

    }

    public void ToQuestMode()
    {

    }

    public void ToFreeMode()
    {
        Main_Menu.SetActive(false);
        Free_Mode_Menu.SetActive(true);
    }

    public void ToCredits()
    {
        Main_Menu.SetActive(false);
        Credits_Menu.SetActive(true);
    }

    public void Exit_Credits()
    {
        Main_Menu.SetActive(true);
        Credits_Menu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
