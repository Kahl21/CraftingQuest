using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementsPool : MonoBehaviour {

    List<Element> myElements = new List<Element>(); //List of all elements
    Dictionary<int, Element> eleDic = new Dictionary<int, Element>(); //reference for list of all elements

    Element SelectedEle; //variable holding the current element of whatever is being used
    [SerializeField]
    GameObject EleButton; //where the button prefab goes
    [SerializeField]
    GameObject dragElement; //where the draggable object prefab goes
    [SerializeField]
    GameObject hammer;

    List<ElementButton> myButtons = new List<ElementButton>(); //List used to create a button for every element

    
    int max_combos_count = 0;
    int current_combos_count = 4;
    int hint_counter = 0;
    string hint_start = "Hint: ";

    [SerializeField]
    Text combos_count;

    [SerializeField]
    GameObject Hint_Window;

    [SerializeField]
    Text hint_text;

    [SerializeField]
    Text hint_button_text;

    // Use this for initialization
    void Start () {
        TextAsset myFile = Resources.Load("Currenttext") as TextAsset; //storing the text file,but as a whole, so it can be parsed later

        string temp = myFile.text; //storing the entire text file as an string
        string[] splitData = temp.Split(System.Environment.NewLine.ToCharArray()); //splits the string holding the textfile into different areas of an array by finding the endline character
        
        for (int line = 0; line < splitData.Length; line++) // goes through every line in the text file
        {
            if (splitData[line].Length > 4) // checks to see if line in text document has four things that can be read (ID, Name, Interaction, Result)
            {
                Element newEle = new Element(splitData[line]); //sets the newest element in the list by send the line from the text document to the Element script to be parsed
                myElements.Add(newEle); // adds newEle to the List of elements
                eleDic.Add(newEle.getID, newEle); //adds newEle and the ID of newEle to the dictionary to be referenced later   
            }
        }

        for (int ele = 0; ele < myElements.Count; ele++) // checks every Element in the Element List
        {
            GameObject newGeo = (GameObject)Instantiate(EleButton, this.transform); //creates new button as a child of whatever this script is attached to
            Debug.Log(newGeo.transform.position.z);
            newGeo.GetComponent<ElementButton>().Init(myElements[ele],this); //gives that button an element to be referenced
            newGeo.transform.position = new Vector3(newGeo.transform.position.x, newGeo.transform.position.y, 0);
            

            myButtons.Add(newGeo.GetComponent<ElementButton>()); //adds current created button to list of buttons
            max_combos_count++;

            
            
        }

        for (int finding = 0; finding < myButtons.Count; finding++)
        {
            if(myButtons[finding].GetElement.GetName == "Fire" || myButtons[finding].GetElement.GetName == "Earth" || myButtons[finding].GetElement.GetName == "Water" || myButtons[finding].GetElement.GetName == "Nature")
            {
                myButtons[finding].gameObject.SetActive(true);                
            }
            else
            {
                myButtons[finding].gameObject.SetActive(false);
            }
        }

        for (int ID = 0; ID < myButtons.Count; ID++)
        {
            myButtons[ID].gameObject.transform.position = new Vector3(myButtons[ID].transform.position.x, myButtons[ID].transform.position.y, -70f);
            myButtons[ID].gameObject.transform.localScale = new Vector3(1,1,1);
        }
       
        Debug.LogError("We doin it fam");
        combos_count.text = current_combos_count + "/" + max_combos_count;
    }

    //fuction called to select buttons and create draggable objects
    public void SelectElement(Element IncEle, ElementButton buttonbuttonwhosgotthebutton, Vector3 spawn)
    {

        //Vector3 destination = new Vector3(buttonbuttonwhosgotthebutton.gameObject.transform.position.x, buttonbuttonwhosgotthebutton.gameObject.transform.position.y,-70f);//Random.Range(-132f, 7f), Random.Range(-205, 185f), -70f); //vector for spawing draggables
        SelectedEle = IncEle; //sets selected element to whatever element is being passed
        GameObject newEle = (GameObject)Instantiate(dragElement, spawn, Quaternion.identity); //creates draggable in a space in the crafting zone
        newEle.GetComponent<ElementObject>().Init(SelectedEle, this); //gives element of button pressed to draggable object
        Debug.Log(newEle.GetComponent<ElementObject>().GetElement.GetName);
        /*if (CurrentButton == null)
        {
            CurrentButton = Button1;
        }
        CurrentButton.Init(SelectedEle, this);*/
    }

    
    //function to see if objects have a proper result or not
    public void Combine(ElementObject object1, ElementObject object2)
    {
        Debug.Log("Im trying to combine.");
        int resultID = -1; //sets combination reuslt to nothing

       // outPut.gameObject.GetComponent<Text>().text = myElements.Count + " eles" ;
        if(object1.GetElement != null && object2.GetElement != null)
        {

           // outPut.gameObject.GetComponent<Text>().text = "we have eles";
            if(object1.GetElement.doesInteract(object2.GetElement.getID)) //checks to see if object1 interacts with object2
            {
                Debug.Log("Combo found");
                resultID = object1.GetElement.getEleDic[object2.GetElement.getID].GetResult; //sets resultId to whatever the result of the combos is
            }
            else if(object2.GetElement.doesInteract(object1.GetElement.getID)) //checks to see if object2 interacts with object1
            {
                resultID = object2.GetElement.getEleDic[object1.GetElement.getID].GetResult; //sets resultId to whatever the result of the combos is
            }

            Debug.Log(resultID);
            if(resultID != -1) //checks to see if there was a combo
            {
                bool gottem = false; 

               /*
                for (int eleButt = 0; eleButt < myButtons.Count; eleButt++)
                {
                    if(myButtons[eleButt].GetElement.GetName == resultstring)
                    {
                        gottem = true;
                        myButtons[eleButt].gameObject.SetActive(true);
                        outPut.gameObject.GetComponent<Text>().text = "we found the ele";
                        break;
                    }
                }
                */
 
                if (eleDic.ContainsKey(resultID)) //checks to see if the result exists
                {
                    Vector3 obj1pos = object1.transform.position;
                    Vector3 obj2pos = object2.transform.position;
                    Vector3 newobjpos = new Vector3((obj1pos.x + obj2pos.x)/2, (obj1pos.y + obj2pos.y) / 2, obj1pos.z);


                    gottem = true;

                    Element resultEle = eleDic[resultID]; //sets resulting Element to currently found dictionary result
                    int indexOf = myElements.IndexOf(resultEle); //sets index equal to the element that has been found
                    Debug.Log(indexOf);
                    
                    if (myButtons[indexOf].gameObject.activeInHierarchy == false)
                    {
                        StartCoroutine(hammerAnim(newobjpos));
                        myButtons[indexOf].gameObject.SetActive(true); //makes button with corresponding new result element visbile and useable
                        current_combos_count++;
                        combos_count.text = current_combos_count + "/" + max_combos_count;
                    }

                    object1.gameObject.SetActive(false);
                    Debug.Log("Object1 dead");
                    object2.gameObject.SetActive(false);
                    Debug.Log("Object2 dead");
                    GameObject newEle = (GameObject)Instantiate(dragElement, newobjpos, Quaternion.identity); //creates draggable in a space in the crafting zone
                    newEle.GetComponent<ElementObject>().Init(resultEle, this); //gives element of button pressed to draggable object
                    Debug.Log("New element created " + newEle.GetComponent<ElementObject>().GetElement.GetName);

                }
                else
                {
                //    outPut.gameObject.GetComponent<Text>().text = resultID + System.Environment.NewLine + 
                }
                
                
                if (!gottem)
                {
                    bool equals = (resultID == myButtons[5].GetElement.getID);
                //    outPut.gameObject.GetComponent<Text>().text = resultID + resultID + System.Environment.NewLine + myButtons[5].GetElement.GetName + myButtons[5].GetElement.GetName.Length + System.Environment.NewLine + "=" + equals;
                }
                else
                {
                 //   outPut.gameObject.GetComponent<Text>().text = "ayyyee";
                }
                
            }
        }
    }

    IEnumerator hammerAnim(Vector3 newpos)
    {
        GameObject hammerAni = (GameObject)Instantiate(hammer, newpos, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(hammerAni);
        StopAllCoroutines();
    }

    public void Hinting()
    {
        hint_counter++;
        if (hint_counter < 23 || hint_counter >= 34)
        {
            if (Hint_Window.gameObject.activeInHierarchy == false)
            {
                Hint_Window.SetActive(true);
                hint_button_text.text = "Hint";
                for (int count = 0; count < myButtons.Count; count++)
                {
                    if (myButtons[count].gameObject.activeInHierarchy == false)
                    {
                        Hint_Window.GetComponentInChildren<Text>().text = "Try to make " + myButtons[count].GetElement.GetName;
                        Hint_Window.GetComponentInChildren<SpriteRenderer>().sprite = ImagePool.Instance.getSprite(myButtons[count].GetElement.getID);
                        if (hint_counter <= 10)
                        {
                            int hinting = (int)Random.Range(1f, 5f);
                            Debug.Log(hinting);
                            switch (hinting)
                            {
                                case 1:
                                    hint_text.text = hint_start + "You can combine two of the same thing together!";
                                    break;
                                case 2:
                                    hint_text.text = hint_start + "Try some crazy combos, you never know if things will combine.";
                                    break;
                                case 3:
                                    hint_text.text = hint_start + "Try your hardest and never give up. It is the blacksmith way!";
                                    break;
                                case 4:
                                    hint_text.text = "Disclaimer: Some combos are a result of poor planning.";
                                    break;
                                default:
                                    Debug.Log("I live... I die...");
                                    break;
                            }

                        }
                        else if (hint_counter > 10 && hint_counter <= 17)
                        {
                            switch (hint_counter)
                            {
                                case 11:
                                    hint_text.text = hint_start + "I get it the game is a little confusing at times.";
                                    break;
                                case 12:
                                    hint_text.text = hint_start + "Man people just really love to press my buttons.";
                                    break;
                                case 13:
                                    hint_text.text = hint_start + "You know, this game is kinda dumb with SOME of its combos.";
                                    break;
                                case 14:
                                    hint_text.text = "Disclaimer: There is nothing wrong with continuing to use a hint system.";
                                    break;
                                case 15:
                                    hint_text.text = hint_start + "Yeah yeah yeah here's what you gotta make.";
                                    break;
                                case 16:
                                    hint_text.text = hint_start + "Game's too hard? Game's too dumb? Not sure yet.";
                                    break;
                                case 17:
                                    hint_text.text = hint_start + "Oh man you must be near the end with using the hint system this much.";
                                    break;
                                default:
                                    Debug.Log("I live... I die...");
                                    break;
                            }
                        }
                        else if (hint_counter > 17 && hint_counter <= 22)
                        {
                            switch (hint_counter)
                            {
                                case 18:
                                    hint_text.text = hint_start + "Come on dude, game can't be that hard.";
                                    break;
                                case 19:
                                    hint_text.text = "Disclaimer: I will stop harassing you after this hint.";
                                    break;
                                case 20:
                                    hint_text.text = hint_start + "You can combine two of the same thing together!";
                                    break;
                                case 21:
                                    hint_text.text = "Disclaimer: I lied. This can't really be that bad.";
                                    break;
                                case 22:
                                    hint_text.text = "Alright you know what!? you lost your hint privledges.";
                                    break;
                                default:
                                    Debug.Log("I live... I die...");
                                    break;
                            }
                        }
                        else if(hint_counter >33 && hint_counter <= 40)
                        {
                            switch(hint_counter)
                            {
                                case 34:
                                    hint_text.text = "Dsiclaimer: I can't believe you've made me do this.";
                                    break;
                                case 35:
                                    hint_text.text = hint_start + "But all good things must come to an end I suppose.";
                                    break;
                                case 36:
                                    hint_text.text = "Disclaimer: You know it's funny I thought it would last longer than this.";
                                    break;
                                case 37:
                                    hint_text.text = "Disclaimer: It was fun while it lasted.";
                                    break;
                                case 38:
                                    hint_text.text = hint_start + "Maybe we'll meet each other again on the other side.";
                                    break;
                                case 39:
                                    hint_text.text = "See ya space cowboy.";
                                    break;
                                case 40:
                                    hint_text.text = "STARTING REBOOT...CLEAN_STATE.EXE...ACTIVATED...PROCEED TO NEW HINT";
                                    break;
                            }
                        }
                        else
                        {
                            int hinting = (int)Random.Range(1f, 5f);
                            Debug.Log(hinting);
                            switch (hinting)
                            {
                                case 1:
                                    hint_text.text = hint_start + "You can combine two of the same thing together!";
                                    break;
                                case 2:
                                    hint_text.text = hint_start + "Try some crazy combos, you never know if things will combine.";
                                    break;
                                case 3:
                                    hint_text.text = hint_start + "Try your hardest and never give up. It is the blacksmith way!";
                                    break;
                                case 4:
                                    hint_text.text = "Disclaimer: Some combos are a result of poor planning.";
                                    break;
                                default:
                                    Debug.Log("I live... I die...");
                                    break;
                            }
                        }
                        break;
                    }
                }
            }
        }
        else
        {
            switch (hint_counter)
            {
                case 23:
                    hint_button_text.text = "No.";
                    break;
                case 24:
                    hint_button_text.text = "Stop.";
                    break;
                case 25:
                    hint_button_text.text = "Silent";
                    break;
                case 26:
                    hint_button_text.text = "Mode";
                    break;
                case 27:
                    hint_button_text.text = "Engaged";
                    break;
                case 28:
                    hint_button_text.text = "";
                    break;
                case 29:
                    hint_button_text.text = ".";
                    break;
                case 30:
                    hint_button_text.text = "..";
                    break;
                case 31:
                    hint_button_text.text = "...";
                    break;
                case 32:
                    hint_button_text.text = "Fine.";
                    break;
                case 33:
                    hint_button_text.text = "You Win.";
                    break;
                default:
                    Debug.Log("I live... I die...");
                    break;
            }
        }
    }

    public void closeWindow()
    {
        if (Hint_Window.gameObject.activeInHierarchy == true)
        {
            Hint_Window.gameObject.SetActive(false);
        }
        Debug.Log(hint_counter);
    }
}