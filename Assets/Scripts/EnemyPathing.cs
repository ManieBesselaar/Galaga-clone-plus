using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPathing : MonoBehaviour {
     
     WaveConfig waveConfig;
    [SerializeField] bool _shouldJoinFormation = true;
    [SerializeField] int _waypointIndexToJoinFormation = 0;
    Formation _formation;
    //State variables
    int waypointIndex = 0;
   bool _isInFormation = false;
    Transform _formationPos;

    List<Transform> waypoints;

   public UnityEvent<EnemyPathing> OnLeaveFormation;
    // Use this for initialization
    void Start () {
    
        _formation = FindObjectOfType<Formation>();
        waypoints = waveConfig.getWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        waypointIndex++;

	}
    public void setWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
    }
	// Update is called once per frame
	void Update ()
    {
        move();
    }

    private void move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            //For moveTowards see https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html


            Vector3 targetPosition;
            if(_isInFormation)
            {
                targetPosition =_formationPos.position;
            }
            else
            {
                targetPosition = waypoints[waypointIndex].position;
            }
           
            var movementThisFrame = waveConfig.getmoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (Vector3.Distance( transform.position, waypoints[waypointIndex].position) < .1f)
            {
                if (!_isInFormation)
                  waypointIndex++;
              
                if (!_isInFormation && _shouldJoinFormation)
                    TryToJoinFormation();

            }
        }
        else
        {
           
                Destroy(gameObject);
           
        }
    }

    private void TryToJoinFormation()
    {
        if (_shouldJoinFormation && _waypointIndexToJoinFormation == waypointIndex)
        {

            _formationPos =_formation?.GetOpenGroup()?.GetOpenPosition(this);
        }
        // if no transform was given then the formation cannot take you, move on buddy.
        _isInFormation = _formationPos != null;
        //_shouldJoinFormation = _formationPos != null;
    }

    private void OnDestroy()
    {
        OnLeaveFormation?.Invoke(this);
    }
    public void LeaveFormation()
    {
_isInFormation = false;
        _shouldJoinFormation = false;
        OnLeaveFormation?.Invoke(this);
    }

public Transform GetFormationPos()
    {
        return _formationPos;
    }
}
