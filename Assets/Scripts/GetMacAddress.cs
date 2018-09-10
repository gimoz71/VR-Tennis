using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.NetworkInformation;
using System.Linq;

public class GetMacAddress : MonoBehaviour
{

    public Text mac;
    public Button startButton;
    // Use this for initialization
    void Start()
    {
        var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                    // where nic.OperationalStatus == OperationalStatus.Up // forza il dispositivo ad essere connesso
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();

        // 9CEBE8B0B294 -0C5415DB124F - 0C5415DB124E - 0C5415DB1252 --> portatile Gioele
        // C86000CC99C2 --> PC-VR ufficio
        if (macAddr.Equals("9CEBE8B0B294") || macAddr.Equals("0C5415DB124F") || macAddr.Equals("0C5415DB124E") || macAddr.Equals("0C5415DB1252") || macAddr.Equals("C86000CC99C2") || macAddr.Equals("0023AE83B346") || macAddr.Equals("0649EC638AE3") || macAddr.Equals("000272389453"))
        {
            Debug.Log("--------------------OK---------------> " + macAddr);
            mac.text = "";
            startButton.interactable = true;
        }
        else {
            Debug.Log("--------------------ERROR---------------> " + macAddr);
            mac.text = "PC non abilitato";
            startButton.interactable = false;
        }
    }
}