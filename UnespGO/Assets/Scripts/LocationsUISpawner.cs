using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
using System.Collections.Generic;
using Mapbox.Examples;

public class LocationsUISpawner : MonoBehaviour
{
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private LocationData[] locationsData;
    [SerializeField] private MenuUIManager menuUIManager;
    [SerializeField] private CompendiumManager compendiumManager; // referência opcional via inspector

    private List<GameObject> spawned = new List<GameObject>();

    void Start()
    {
        if ((locationsData == null || locationsData.Length == 0))
        {
            var spawn = FindObjectOfType<SpawnOnMap>();
            if (spawn != null)
            {
                locationsData = spawn.GetLocations();
            }
        }

        // tenta resolver compendiumManager automaticamente se não foi atribuído
        if (compendiumManager == null)
        {
            compendiumManager = FindObjectOfType<CompendiumManager>();
        }

        if (locationsData == null || itemPrefab == null || contentParent == null) return;
        Populate();
    }

    public void Populate()
    {
        ClearExisting();

        for (int i = 0; i < locationsData.Length; i++)
        {
            var data = locationsData[i];
            var go = Instantiate(itemPrefab, contentParent);
            go.name = $"LocationItem_{i}";
            go.transform.SetSiblingIndex(i);

            var ui = go.GetComponent<LocationItemUI>();
            if (ui != null)
            {
                int index = i;
                ui.Setup(data.locationImage, data.locationName, () =>
                {
                    if (menuUIManager != null)
                    {
                        menuUIManager.DisplayEventPanel(index + 1, data.locationName, data.locationDescription, data.locationImage);
                    }
                });

                // registra o botão no CompendiumManager (se houver)
                if (compendiumManager != null && ui.button != null)
                {
                    compendiumManager.RegisterButton(ui.button);
                }
            }

            spawned.Add(go);
        }
    }

    public void ClearExisting()
    {
        for (int i = spawned.Count - 1; i >= 0; i--)
        {
            if (spawned[i] != null) Destroy(spawned[i]);
        }
        spawned.Clear();
    }
}