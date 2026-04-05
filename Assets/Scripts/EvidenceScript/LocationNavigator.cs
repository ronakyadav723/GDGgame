using UnityEngine;

public class LocationNavigator : MonoBehaviour
{
   // public OfficerSearchManager searchManager;
    public GameObject storageButton;

    [Header("Room Configurations")]
    public StorageLocationManager storageRoom;

    void Update()
    {
        // Simple gate: Show the storage button only when criteria are met
        // Based on Master Reference: Score > 15
        if (SceneData.TotalSuspicionScore >= 15 && !storageButton.activeSelf)
        {
            storageButton.SetActive(true);
        }
    }

    public void MoveToStorage()
    {
        // 1. Camera Jump
        Camera.main.transform.position = new Vector3(storageRoom.roomCoordinates.x, storageRoom.roomCoordinates.y, -10f);

        // 2. Data Swap
       // searchManager.SwitchLocation(storageRoom.storageEvidence);

        // 3. UI Cleanup
        storageButton.SetActive(false);
    }
}