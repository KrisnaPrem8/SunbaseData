using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class APICall : MonoBehaviour
{
    public class Client
    {
        public bool isManager { get; set; }
        public int id { get; set; }
        public string label { get; set; }
    }

    public class DataItem
    {
        public string address { get; set; }
        public string name { get; set; }
        public int points { get; set; }
    }

    public class Root
    {
        public List<Client> clients { get; set; }
        public Dictionary<string, DataItem> data { get; set; }
        public string label { get; set; }
    }

    public static Root database = new Root();

    void Start()
    {
        StartCoroutine(CallAPI("https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data"));
    }

    public IEnumerator CallAPI(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }

            database = JsonConvert.DeserializeObject<Root>(webRequest.downloadHandler.text);
            UIManager.instance.DropDownButton();
        }
    }
}