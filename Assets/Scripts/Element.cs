using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element{

    Dictionary<int, ElementInteract> eleDic = new Dictionary<int, ElementInteract>(); //reference for list of all elements

    char carrotSplitter = '^'; //splitter for lines of combos on text file 
    char colonSplitter = ':'; //splitter for interaction list in text file
    int myId; //element ID denoted in by number (1,2,3,etc.)
    string EleName; //name of elemenet in line in text flie(first thing)
    string EleInteract; //name of element that reacts to the first element(second thing)
    int ResultID; //name of element that results from the two elements combining

    




    //function that parses out the text file
    public Element(string incData) 
    {

        string[] parsedData = incData.Split(carrotSplitter); //array that holds all lines of text files that is seperated everytime a carrot is seen in the file


        //myId = int.Parse(parsedData[0]);
        try {
            myId = System.Convert.ToInt32(parsedData[0]); //ID of element being stored
        }
        catch
        {

        }
        EleName = parsedData[1]; //Name of element being stored
        if (parsedData.Length > 2)
        {
            string[] Interactions = parsedData[2].Split(colonSplitter);
            for(int interactlist = 0; interactlist < Interactions.Length; interactlist++)
            {
                if (Interactions[interactlist].Length > 0)
                {
                    ElementInteract newInteraction = new ElementInteract(Interactions[interactlist]);
                    eleDic.Add(newInteraction.GetInteract, newInteraction);
                }
            }
        }

        //Debug.Log(EleName + " " + myId + " id " + ResultID);
        //EleName = EleName.Normalize();
       
    }

    //function that checks to see if the elements combine and returns a bool(yes/no)
    public bool doesInteract(int other) 
    {
        return eleDic.ContainsKey(other);
    }

    public string GetName {get { return EleName; } } //gets name of element
    public int GetResult { get { return ResultID; } } //gets resulting element
    public int getID { get { return myId; } } //gets ID of element(1,2,3,etc.)
    public Dictionary<int, ElementInteract> getEleDic { get { return eleDic; } }
}
