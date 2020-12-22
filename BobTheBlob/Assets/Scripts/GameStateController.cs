using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance = null;
    public string lastScene;
    Dictionary<string, Dictionary<string, Vector3>> spawnCoords;

    public bool liquidLevelDone, gasLevelDone;

    public bool liquidLevel 
    {
        get 
        {
            return liquidLevelDone;
        }
        set 
        {
            liquidLevelDone = value;
        }
    }

    public bool gasLevel 
    {
        get 
        {
            return gasLevelDone;
        }
        set 
        {
            gasLevelDone = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        lastScene = "SpawningRoom";
        spawnCoords = new Dictionary<string, Dictionary<string, Vector3>>();
        //Populating the transition graph
        spawnCoords.Add("SpawningRoom", new Dictionary<string, Vector3>(){
            {"SpawningRoom", new Vector3(11.5f, 0f, 0)}, // From spawn, to spawn
            {"DemoGasLevel", new Vector3(-51.8f, 3, 0)},    // From spawn to gas
            {"LiquidLevel", new Vector3(75, 9, 0)},         // From spawn to liquid
            {"AscensionRoom", new Vector3(13.5f, -34f, 0f)}
        });
        spawnCoords.Add("LiquidLevel", new Dictionary<string, Vector3>(){
            {"SpawningRoom", new Vector3(-42.5f, -1, 0)}    // From liquid back to spawn, you get the picture...
        });
        spawnCoords.Add("DemoGasLevel", new Dictionary<string, Vector3>(){
            {"SpawningRoom", new Vector3(64.5f, -17, 0)}    // From liquid back to spawn, you get the picture...
        });
        spawnCoords.Add("AscensionRoom", new Dictionary<string, Vector3>(){
            {"BossRoom", new Vector3(2f, -34f, 0f)}
        });

        liquidLevelDone = false;
        gasLevelDone = false;

        // If we don't have an instance set - set it now
        if(!instance)
            instance = this;
        // Otherwise, its a double, we dont need it - destroy
        else {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public Vector3 currentTransition() {
        return spawnCoords[lastScene][SceneManager.GetActiveScene().name];
    }
}
