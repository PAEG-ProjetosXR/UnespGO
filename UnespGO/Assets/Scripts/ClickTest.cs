using UnityEngine;

public class ClickTest : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} was clicked!");
    }
}
