using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace state
{
    interface PlayerState
    {
        void handleInput(StateMachine player);
        void UpdateState(StateMachine player);
        void OnExit(StateMachine player);
    }
}
