using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ImagePool : MonoBehaviour {

    private static bool hasBeenInit = false;
    //List<Sprite> sprList = new List<Sprite>();
    Dictionary<int, Sprite> SprDic = new Dictionary<int, Sprite>();

    private static ImagePool _imagePool;
    public static ImagePool Instance
    {
        get
        {
            if(_imagePool == null)
            {
                if(FindObjectOfType<ImagePool>() != null)
                {
                    _imagePool = FindObjectOfType<ImagePool>();
                    if (!hasBeenInit)
                    {
                        _imagePool.Init();
                    }
                    return _imagePool;
                }
                else
                {
                    GameObject newGo = new GameObject();
                    _imagePool = newGo.AddComponent<ImagePool>();
                    if (!hasBeenInit)
                    {
                        _imagePool.Init();
                    }
                    return _imagePool;
                }
            }
            else
            {
                if (!hasBeenInit)
                {
                    _imagePool.Init();
                }
                return _imagePool;
            }
        }
    }

    private void Init()
    {
        hasBeenInit = true;

        var LoadedSprites = Resources.LoadAll("Sprites/", typeof(Sprite)).Cast<Sprite>();
        foreach (var LoadedSprite in LoadedSprites)
        {
            try {
                int indexOf = int.Parse(LoadedSprite.name);
                SprDic.Add(indexOf, LoadedSprite as Sprite);
            }
            catch
            {
                Debug.LogError("could not add " + LoadedSprite.name + " to the dictionary");
            }
       }
    }

    public Sprite getSprite(int ID)
    {
        if (SprDic.ContainsKey(ID))
        {
            return SprDic[ID];
        }
        else
        {
            Debug.LogError("We dont have a sprite for Id of " + ID);
            return null;
        }
    }

}
