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
public abstract class GameState : MonoBehaviour
{
    public GAMESTATE mode;
    // Start is called before the first frame update

}
