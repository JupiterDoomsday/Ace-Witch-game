using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface PlayerState {

    void handleInput(Player marji);
    void FixedUpdate(Player marji);
}
