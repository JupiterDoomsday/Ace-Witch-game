using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private GameState curState;
    public void Initialize( GameState start_state)
    {
        curState = start_state;
        start_state.Enter();
    }
    public void ChangeState(GameState new_state)
    {
        curState.Exit();
        curState = new_state;
        new_state.Enter();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
