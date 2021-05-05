using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Dictionary<Type,IEnemyStates> _statesMap;
    private IEnemyStates _currentState;

    private void Start()
    {
        InitStates();
        SetStateByDefault();
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, IEnemyStates>();

        _statesMap[typeof(PatrolState)] = GetComponent<PatrolState>();
        _statesMap[typeof(ChaseState)] = GetComponent<ChaseState>();
    }

    private void SetState(IEnemyStates newState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = newState;
        _currentState.Enter();
    }

    private IEnemyStates GetState<T>() where T : IEnemyStates
    {
        var type = typeof(T);
        return _statesMap[type];
    }

    private void SetStateByDefault()
    {
        SetStatePatrol();
    }

    public void SetStatePatrol()
    {
        var state = GetState<PatrolState>();
        SetState(state);
    }

    public void SetStateChase()
    {
        var state = GetState<ChaseState>();
        SetState(state);
    }
}
