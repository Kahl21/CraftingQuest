using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteract{

    char astsplitter = '*';
    int interactID;
    int resultID;

    public ElementInteract(string incData)
    {
        try {
            string[] Interactions = incData.Split(astsplitter);

            if (int.TryParse(Interactions[0], out interactID)) { }
            else { Debug.Log("we done fucked boys"); }

            if (int.TryParse(Interactions[1], out resultID)) { }
            else { Debug.Log("we done fucked boys"); }
        }
        catch
        {
            Debug.LogError("we ran into an error parsing data: " + incData);
        }

    }

    public int GetResult { get { return resultID; } }
    public int GetInteract { get { return interactID; } }
}
