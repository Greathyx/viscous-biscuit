using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class LightColor : MonoBehaviour
{
    public enum LevelType
    {
        LiquidLevel,
        GasLevel,
        None
    }
    public bool Off;

    [SerializeField]
    LevelType _level;
    public LevelType level
    {
        get => _level;
        set => _level = value;
    }

    private bool trigger;
    private Light2D light;


    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
        light = GetComponent<Light2D>();
        Off = false;
        light.color = new Color(0.454902f, 0.9529412f, 0.427451f);

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

    // Update is called once per frame 
    void Update()
    { 

        Off = getCurrentLevelValue(_level);

        if (Off)
        {
            light.color = new Color(0.9529412f, 0.4881461f, 0.427451f);
        } else {
            light.color = new Color(0.454902f, 0.9529412f, 0.427451f); //MOD
        }

    }
}
