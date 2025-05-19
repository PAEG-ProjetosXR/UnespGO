using UnityEngine;

public class EventPointer : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float amplitude = 2.0f;
    [SerializeField] private float frequency = 0.50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.fixedTime*Mathf.PI*frequency) * amplitude) + 15, transform.position.z);
    }

    private void OnMouseDown()
    {
        Debug.Log("Pointer clicked");
    }
}
