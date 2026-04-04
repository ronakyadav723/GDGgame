using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StudyLocationManager : MonoBehaviour
{   int count=0;
    public List<EvidenceItem> studyEvidence;

    [Header("Navigation Settings")]
    public Button storageRoomButton; 
    public Vector2 storageRoomCoordinates = new Vector2(25, 0); 

    [Header("References")]
    public OfficerSearchManager searchManager;
    public StorageLocationManager storageRoomScript; // Reference to the next room's script

    void Update()
    {
        // Unlock Logic
        if (SceneData.TotalSuspicionScore >= 15 && !storageRoomButton.gameObject.activeSelf&&count<1)
        {
            storageRoomButton.gameObject.SetActive(true);
        }
    }

    public void MoveToStorage()
    {
        // 1. Move Camera
        Camera.main.transform.position = new Vector3(storageRoomCoordinates.x, storageRoomCoordinates.y, -10f);
        
        // 2. Tell the Search Manager to use Storage Evidence now
        if (searchManager != null && storageRoomScript != null)
        {
            searchManager.SwitchLocation(storageRoomScript.storageEvidence);
        }

        // 3. Cleanup UI
        storageRoomButton.gameObject.SetActive(false);
        count++;
    }
}