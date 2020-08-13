using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GAMESTATE {
    ACTIVE,
    PAUSE,
    CUTSCENE,
    INVENTORY,
    MENU,
    END
};
public class GameState : MonoBehaviour
{
    public GAMESTATE mode;
    // Start is called before the first frame update
    void Start()
    {
        // Sync framerate to monitors refresh rate
        QualitySettings.vSyncCount = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
