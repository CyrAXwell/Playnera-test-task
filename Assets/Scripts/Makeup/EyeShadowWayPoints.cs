using System.Collections.Generic;
using UnityEngine;

public class EyeShadowkWayPoints : MonoBehaviour
{
    [SerializeField] private Transform _pickUpPoint;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _itemViewPickUpPoint;
    [SerializeField] private List<Transform> _makeupPoints;

    private List<Vector3> _makeupPointsPositions;

    public Vector3 PickUpPoint => _pickUpPoint.position;
    public Vector3 HoldPoint => _holdPoint.position;
    public Vector3 ItemViewPickUpPoint => _itemViewPickUpPoint.position;
    public List<Vector3> MakeupPoints => _makeupPointsPositions;
    
    private void Start()
    {
        _makeupPointsPositions = new List<Vector3>();

        foreach (Transform point in _makeupPoints)
        {
            _makeupPointsPositions.Add(point.position);
        }
    }
}