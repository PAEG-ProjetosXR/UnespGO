using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;

public class Character : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Updated for PlayerCharacter
    }

    // Update is called once per frame
    void Update()
    {
        // Usando o Input System package
        Vector2 input = UnityEngine.InputSystem.Keyboard.current != null
            ? new Vector2(
                UnityEngine.InputSystem.Keyboard.current.aKey.isPressed ? -1 : UnityEngine.InputSystem.Keyboard.current.dKey.isPressed ? 1 : 0,
                UnityEngine.InputSystem.Keyboard.current.sKey.isPressed ? -1 : UnityEngine.InputSystem.Keyboard.current.wKey.isPressed ? 1 : 0)
            : Vector2.zero;

        Vector3 move = new Vector3(input.x, 0, input.y);
        characterController.Move(move * Time.deltaTime * speed);
    }
}
