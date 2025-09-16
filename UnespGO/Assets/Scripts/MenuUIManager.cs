
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject EventPanelUser;
    [SerializeField] GameObject MissionPanelUI;
    bool isEventPanelActive;
    int tempEvent;
    public TextMeshProUGUI locationName;
    public TextMeshProUGUI locationDescription;
    public Image locationImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayEventPanel(int eventID, string eventName, string eventDescription, Sprite eventImage)
    {
        if (isEventPanelActive == false)
        {
            Debug.Log("Abrindo tela");
            tempEvent = eventID;
            EventPanelUser.SetActive(true);
            isEventPanelActive = true;
            locationName.text = eventName;
            locationDescription.text = eventDescription;
            locationImage.sprite = eventImage;
        }
    }

    public void CloseButtonClick()
    {
        EventPanelUser.SetActive(false);
        isEventPanelActive = false;
        Debug.Log("Fechando tela");
    }

    public void ClosePanelButtonClick(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpenPanelButtonClick(GameObject panel)
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }   
  
}
