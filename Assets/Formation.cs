using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Formation : MonoBehaviour
{
   [SerializeField] FormationGroup[] Groups;
  [SerializeField] float _formationCycleTime = 20;
    [SerializeField] Transform[] _path;
    int _pathIndex = 0;
   [SerializeField] float _moveSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReleaseFormation());
    }
    IEnumerator ReleaseFormation()
    {
        while (true)
        {
            foreach (FormationGroup group in Groups)
            {
                yield return new WaitForSeconds(_formationCycleTime);
                group.ReleaseMembers();

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_pathIndex > 10000) _pathIndex = 0;

        transform.position = Vector2.MoveTowards(transform.position, _path[_pathIndex % _path.Length].position, _moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _path[_pathIndex % _path.Length].position) < .1f)
        {
            _pathIndex++;
        }
    }
    public FormationGroup GetOpenGroup()
    {
        int openSlots = 0;
        int slotsInGroup = 0;
        FormationGroup group = null;
        for (int i = 0; i < Groups.Length; i++)
        {
            slotsInGroup = Groups[i].GetOpenSlots();
            if (slotsInGroup > openSlots)
            {
                openSlots = slotsInGroup;
                group = Groups[i];
            }
           
        }
        return group;
    }
}

