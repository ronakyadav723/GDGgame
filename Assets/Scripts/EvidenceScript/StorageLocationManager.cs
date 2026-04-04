using System.Collections.Generic;
using UnityEngine;

public class StorageLocationManager : MonoBehaviour
{
    [Header("Evidence List")]
    public List<EvidenceItem> storageEvidence;

    [Header("Location Coordinates")]
    public Vector2 roomCoordinates; // Set these to where your Storage background is
}