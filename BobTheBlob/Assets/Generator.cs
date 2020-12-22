using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
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

    [TextArea]
    public string connectionObject;
    private ButtonPress trigger;    // TODO: Generalize...

    //private BoxCollider2D collider;
    //private UnityEngine.U2D.SpriteShapeRenderer renderer;

    public bool gotToggled;

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
        //collider = this.gameObject.GetComponent<BoxCollider2D>();   
        //renderer = this.gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>();
        trigger = GameObject.Find(connectionObject).GetComponent<ButtonPress>();
        if (getCurrentLevelValue(_level))
        {
            turnOff();
        }
    }


    void changeCurrentLevelValue(LevelType levelInp)
    {
        switch (levelInp)
        {

            case LevelType.LiquidLevel:
                GameStateController.instance.liquidLevel = true;
                break;

            case LevelType.GasLevel:
                GameStateController.instance.gasLevel = true;
                break;


        }
    }

    // Update is called once per frame
    void Update()
    {

        if (trigger.pressed & !gotToggled)
        {
            gotToggled = true;
            turnOff();
        }
    }

    void turnOff()
    {
        changeCurrentLevelValue(_level);
        //GetComponent<Collider>().enabled ^= false;
        this.gameObject.GetComponent<Animator>().enabled = false;
        Debug.Log("Turn off Generator");
    }
}
