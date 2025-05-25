namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
    using ScriptableObjects;

	public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		//[SerializeField]
		//[Geocode]
		//string[] _locationStrings;

		Vector2d[] _locations;

		[SerializeField]
		//List<LocationData> locationsData;
		LocationData[] locationsData;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;

		void Start()
		{
			//_locations = new Vector2d[_locationStrings.Length];
			_locations = new Vector2d[locationsData.Length];

			_spawnedObjects = new List<GameObject>();
			for (int i = 0; i < locationsData.Length; i++)
			{
				var locationString = locationsData[i].locationStrings;
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance = Instantiate(_markerPrefab);
				instance.GetComponent<EventPointer>().eventPose = _locations[i];
				instance.GetComponent<EventPointer>().eventID = i + 1;
				instance.GetComponent<EventPointer>().eventName = locationsData[i].locationName;
				instance.GetComponent<EventPointer>().eventDescription = locationsData[i].locationDescription;	
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
				//Debug.Log("Location name: " + locationsData[i].locationName);
				//Debug.Log("Location description: " + locationsData[i].locationDescription);
			}
		}

		private void Update()
		{
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}
	}
}