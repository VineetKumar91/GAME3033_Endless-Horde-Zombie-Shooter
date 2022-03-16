using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected StateMachine stateMachine;

    public float UpdateInterval { get; protected set; } = 1f;

    protected State(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
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
