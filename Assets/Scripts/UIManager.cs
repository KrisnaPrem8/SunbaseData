using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI details;
    public RectTransform layoutGroup;
    public TMP_Dropdown dropdown;
    public GameObject panel;
    public static UIManager instance;

    void Start()
    {
        instance = this;
    }

    private void CreateClientUI(APICall.Client client)
    {
        TextMeshProUGUI child = Instantiate(details, layoutGroup);
        string points = "Null";
        if (APICall.database.data.ContainsKey(client.id.ToString()))
            points = APICall.database.data[client.id.ToString()].points.ToString();
        child.text = $"{client.label}: {points}";
        child.GetComponent<Button>().onClick.AddListener(() => PopUpButton(client));
    }

    public void DisplayClients(List<APICall.Client> clientList)
    {
        foreach (var client in clientList)
        {
            CreateClientUI(client);
        }
    }

    public void DropDownButton()
    {
        foreach (Transform child in layoutGroup)
        {
            Destroy(child.gameObject);
        }

        if (dropdown.value == 0)
        {
            DisplayClients(APICall.database.clients);
        }
        else if (dropdown.value == 1)
        {
            DisplayClients(APICall.database.clients.FindAll(client => client.isManager));
        }
        else
        {
            DisplayClients(APICall.database.clients.FindAll(client => !client.isManager));
        }

        layoutGroup.DOAnchorPosX(0f, 1f);
        dropdown.GetComponent<RectTransform>().DOAnchorPosY(-100f, 1f);
    }

    public void PopUpButton(APICall.Client client)
    {
        panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -2000f);
        panel.SetActive(true);
        panel.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f);

        Transform panelContent = panel.transform.GetChild(0);

        if (panelContent.childCount > 1)
        {
            Destroy(panelContent.GetChild(1).gameObject);
        }

        string name = "Null";
        string points = "Null";
        string address = "Null";

        if (APICall.database.data.ContainsKey(client.id.ToString()))
        {
            var data = APICall.database.data[client.id.ToString()];
            name = data.name;
            points = data.points.ToString();
            address = data.address;
        }

        TextMeshProUGUI child = Instantiate(details, panelContent);
        child.color = new Color(0f, 0f, 0f);
        Destroy(child.GetComponent<Button>());

        child.text = $"Name: {name}\nPoints: {points}\nAddress: {address}";
    }
}