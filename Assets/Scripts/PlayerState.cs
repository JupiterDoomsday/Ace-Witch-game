using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface PlayerState {
    void handleInput(Player player);
    void UpdateState(Player player);
}
