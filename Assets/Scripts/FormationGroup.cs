using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationGroup : MonoBehaviour
{
    Transform[] _positions;
    List<Transform> _assignedPositions = new List<Transform>();
    List<EnemyPathing> _members = new List<EnemyPathing>();
    // Start is called before the first frame update
    private void Awake()
    {
        _positions = transform.GetComponentsInChildren<Transform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetOpenPosition(EnemyPathing groupMember)
    {
        Transform positionToReturn = null;
        for (int i=0; i< _positions.Length; i++)
        {
            if (!_assignedPositions.Contains(_positions[i]))
            {
                positionToReturn = _positions[i];
                _assignedPositions.Add(_positions[i]);
                _members.Add(groupMember);
                groupMember.OnLeaveFormation.AddListener(RemoveMember);
                break;
            }
        }
        return positionToReturn;
    }

    private void RemoveMember(EnemyPathing member)
    {
        _assignedPositions.Remove(member.GetFormationPos());
        _members.Remove(member);
    }
    public int GetOpenSlots()
    {
        return _positions.Length - _assignedPositions.Count; 
    }
    public void ReleaseMembers()
    {
        for(int i = 0; i< _members.Count; i++)
        {
            _members[i].LeaveFormation();
        }
    }
}
