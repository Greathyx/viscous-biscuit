using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimation : MonoBehaviour
{

    public enum LevelType
    {
        LiquidLevel,
        GasLevel,
        None
    }

    [SerializeField]
    LevelType _level;
    public LevelType level
    {
        get => _level;
        set => _level = value;
    }

    bool getCurrentLevelValue(LevelType levelInp)
    {
        switch (levelInp)
        {

            case LevelType.LiquidLevel:
                return GameStateController.instance.liquidLevel;
                break;

            case LevelType.GasLevel:
                return GameStateController.instance.gasLevel;
                break;

            case LevelType.None:
                return false;
                break;

            default:
                return false;
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (getCurrentLevelValue(_level))
        {
            turnOff();
        }
    }

    void Update()
    {
        if (getCurrentLevelValue(_level))
        {
            turnOff();
        } else {
            turnOn();
        }
    }

    void turnOff()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //this.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().enabled = false;
        }
        //this.gameObject.GetComponent<Animator>().enabled = false;
    }
    
    void turnOn() //MOD
    {
         for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
