using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationItemUI : MonoBehaviour
{
    public Image thumbnail;
    public TMP_Text title; // use Text se não usar TextMeshPro
    public Button button;

    // Preenche os campos (chamar após Instantiate)
    public void Setup(Sprite image, string name, UnityEngine.Events.UnityAction onClick)
    {
        if (thumbnail != null) thumbnail.sprite = image;
        if (title != null) title.text = name;
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onClick);
        }
    }
}