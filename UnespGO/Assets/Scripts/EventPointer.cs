using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mapbox Libraries
using Mapbox.Examples;
using Mapbox.Utils;


public class EventPointer : MonoBehaviour
{
    LocationStatus playerLocation; // Moved inside the class
    public Vector2d eventPose;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float amplitude = 2.0f;
    [SerializeField] private float frequency = 0.50f;
    public int eventID;
    public string eventName;
    public string eventDescription;
    public Sprite eventImage;
    MenuUIManager menuUIManager;
    EventManager eventManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuUIManager = GameObject.Find("Canvas").GetComponent<MenuUIManager>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        FloatAndRotatePoiter();
    }

    void FloatAndRotatePoiter()
    {
        GetComponent<Renderer>().material.color = Color.red;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude) + 15, transform.position.z);
    }

    private void OnMouseDown()
    {
        playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        var currentPlayerLocation = new GeoCoordinatePortable.GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLong());
        var eventLocation = new GeoCoordinatePortable.GeoCoordinate(eventPose[0], eventPose[1]);
        var distance = currentPlayerLocation.GetDistanceTo(eventLocation);
        Debug.Log("Distance to event: " + distance);
        Debug.Log("Player Location: " + currentPlayerLocation);
        menuUIManager.DisplayEventPanel(eventID, eventName, eventDescription, eventImage);
    }
}
