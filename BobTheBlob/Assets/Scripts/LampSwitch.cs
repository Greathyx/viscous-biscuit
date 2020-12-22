using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitch : MonoBehaviour
{

    public enum LevelType {
        LiquidLevel,
        GasLevel,
        None
    }
    public bool Off;

    [SerializeField]
    LevelType _level;
    public LevelType level {
        get => _level;
        set => _level = value;
    }

    private bool trigger;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
        sprite = GetComponent<SpriteRenderer>();
        Off = false;

    }

    bool getCurrentLevelValue(LevelType levelInp) {
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
    

    // Update is called once per frame 
    void Update()
    {
        
        Off = getCurrentLevelValue(_level);
        
        if (Off)
        {
             sprite.enabled = false;
        } else
        {
            sprite.enabled = true; //MOD
        }
    }
}
