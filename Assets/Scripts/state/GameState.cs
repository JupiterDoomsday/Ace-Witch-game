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
    public Player marji;
    public StateMachine stateMachine;
    // Start is called before the first frame update
    public virtual void Enter() { }
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}
