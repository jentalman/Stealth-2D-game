using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaseState : MonoBehaviour , IEnemyStates
{
    private AIPath _ai;
    private AIDestinationSetter _target;
    private float _rotateDirection;
    private Transform _sectorTransform;

    private void  Awake()
    {
        _ai = GetComponent<AIPath>();
        _target = GetComponent<AIDestinationSetter>();
        _sectorTransform = transform.GetChild(0);
    }

    private void Update()
    {
        TrackTarget();
    }

    public void InitTarget(Transform target)
    {
        _target.target = target;
    }

    public void Enter()
    {
        if (enabled == false) 
        { 
            enabled = true;
            _ai.enabled = true;
            ChangeColorSector();
        }
    }

    public void Exit()
    {
        if (enabled == true)
        { 
            enabled = false;
            _ai.enabled = false;
        }
    }

    private void TrackTarget()
    {
        if (_sectorTransform.InverseTransformPoint(_target.target.position).x > 0.1f)
        {
            _rotateDirection = -1;
        }
        else if (_sectorTransform.InverseTransformPoint(_target.target.position).x < 0f)
        {
            _rotateDirection = 1;
        }
        else
        {
            if (_sectorTransform.InverseTransformPoint(_target.target.position).y < 0)
            {
                _rotateDirection = 1;
            }
            else
            {
                _rotateDirection = 0;
            }
        }
        _sectorTransform.rotation *= Quaternion.Euler(0, 0, _rotateDirection);
    }

    public void ChangeColorSector()
    {
        _sectorTransform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color (1 , 0, 0, 0.25f);
    }
}
