using TMPro;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject EventPanelUser;
    bool isEventPanelActive;
    int tempEvent;
    public TextMeshProUGUI locationName;
    public TextMeshProUGUI locationDescription;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayEventPanel(int eventID, string eventName, string eventDescription)
    {
        if (isEventPanelActive == false)
        {
            tempEvent = eventID;
            EventPanelUser.SetActive(true);
            isEventPanelActive = true;
            locationName.text = eventName;
            locationDescription.text = eventDescription;
        }
    }

    public void CloseButtonClick()
    {
        EventPanelUser.SetActive(false);
        isEventPanelActive = false;
        Debug.Log("Fechando tela");
    }
}
