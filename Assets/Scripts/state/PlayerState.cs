using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace state
{
    interface PlayerState
    {
        void handleInput(Player player);
        void UpdateState(Player player);
        void OnExit(Player player);
    }
}
