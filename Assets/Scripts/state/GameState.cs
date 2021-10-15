using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace state
{
    public enum GAMESTATE_TYPE
    {
        PLAYER,
        NPC,
        ENEMY
    };
    public abstract class GameState : MonoBehaviour
    {
        public GAMESTATE_TYPE ver;
        public int priority;
        protected GameState()
        {
            priority = int.MinValue;
        }
        public int Priority { get; protected set; }
        // Start is called before the first frame update
        public virtual void Enter() { }
         public virtual void HandleInput() { }
         public virtual void LogicUpdate() { }
         public virtual void PhysicsUpdate() { }
         public virtual void Exit() { }
    }
}
