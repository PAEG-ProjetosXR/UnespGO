using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using UnityEngine.UI;


namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class LocationData : ScriptableObject
    {
        [field: SerializeField]
        [Geocode]
        public string locationStrings;
        //Vector2d[] locations;

        [field: SerializeField] public string locationName { get; set; }
        [field: SerializeField] public Sprite locationImage { get; set; }
        [field: SerializeField, TextAreaAttribute(100,10)] public string locationDescription { get; set; }
        //[field: SerializeField] public GameObject PopUpPrefab { get; set; }

    }
}
