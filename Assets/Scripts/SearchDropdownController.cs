using UnityEngine;
using TMPro;
using System.Collections.Generic;

// ─────────────────────────────────────────────────────────────
//  SearchDropdownController.cs
//  Attach to: the same GameObject as your Dropdown_search
//  in the MainMenu scene.
//
//  Automatically rebuilds the dropdown options whenever
//  the server tells us a new location is unlocked.
//  No more hardcoded location lists.
// ─────────────────────────────────────────────────────────────

public class SearchDropdownController : MonoBehaviour
{
    [Header("Drag from Hierarchy")]
    public TMP_Dropdown dropdown;   // Dropdown_search

    void OnEnable()
    {
        APIManager.OnLocationsUpdated += RefreshDropdown;
    }

    void OnDisable()
    {
        APIManager.OnLocationsUpdated -= RefreshDropdown;
    }

    void Start()
    {
        // Build initial list from whatever is already in SceneData
        // (populated by /start on GameBootstrap)
        RefreshDropdown();
    }

    void RefreshDropdown()
    {
        if (dropdown == null) return;

        var options = new List<TMP_Dropdown.OptionData>();

        // First option is always "None" — keeps your existing
        // NavigationManager logic working (case 0 = disabled)
        options.Add(new TMP_Dropdown.OptionData("None"));

        // Add only unlocked locations from the server
        foreach (string loc in SceneData.UnlockedLocations)
            options.Add(new TMP_Dropdown.OptionData(loc));

        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.value = 0; // reset to "None"
        dropdown.RefreshShownValue();
    }
}