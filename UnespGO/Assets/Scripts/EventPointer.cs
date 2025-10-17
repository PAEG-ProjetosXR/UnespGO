using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mapbox Libraries
using Mapbox.Examples;
using Mapbox.Utils;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.EventSystems;
using System;

public class EventPointer : MonoBehaviour, IPointerDownHandler
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
    private Touch theTouch;
    Boolean isFirstTouch = true;
    MenuUIManager menuUIManager;
    EventManager eventManager;
    CompendiumManager compendiumManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuUIManager = GameObject.Find("Canvas").GetComponent<MenuUIManager>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        // tenta obter CompendiumManager do Canvas primeiro, senão usa FindObjectOfType como fallback
        var canvasObj = GameObject.Find("Canvas");
        if (canvasObj != null)
        {
            compendiumManager = canvasObj.GetComponent<CompendiumManager>();
        }
        if (compendiumManager == null)
        {
            compendiumManager = FindObjectOfType<CompendiumManager>();
        }
        Debug.Log($"EventPointer Start: compendiumManager found = { (compendiumManager != null) }");
    }

    void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        FloatAndRotatePoiter();
         if (Touch.activeTouches.Count > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Touch.activeTouches[0].screenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    // Simula o OnPointerDown
                    OnPointerDown(new PointerEventData(EventSystem.current));
                }
            }
        }
    }

    void FloatAndRotatePoiter()
    {
        GetComponent<Renderer>().material.color = Color.red;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude) + 15, transform.position.z);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown chamado no marker!");
        playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        var currentPlayerLocation = new GeoCoordinatePortable.GeoCoordinate(playerLocation.GetLocationLat(), playerLocation.GetLocationLong());
        var eventLocation = new GeoCoordinatePortable.GeoCoordinate(eventPose[0], eventPose[1]);
        var distance = currentPlayerLocation.GetDistanceTo(eventLocation);
        Debug.Log("Distance to event: " + distance);
        Debug.Log("Player Location: " + currentPlayerLocation);
        if (isFirstTouch)
        {
            isFirstTouch = false;
            Debug.Log($"First touch detected on {eventName} with EventID {eventID}, invoking compendium.");
            if (compendiumManager != null)
            {
                // se o objetivo é habilitar o botão no compendium (método espera id 1-based)
                compendiumManager.EnableEntryButton(eventID);

                // se o objetivo é ativar o entry (array entries) use a linha abaixo (id 0-based)
                // compendiumManager.EnableEntry(eventID - 1);
            }
            else
            {
                Debug.LogWarning("CompendiumManager is null when attempting to enable entry.");
            }
        }
        menuUIManager.DisplayEventPanel(eventID, eventName, eventDescription, eventImage);
    }
}
