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

    public static string MAC_ADDRESS_SVILUPPO = "C86000CC99C2";
    public static string MAC_ADDRESS_CLIENTE = "0C5415DB124E";

    // Use this for initialization
    void Start()
    {
        var addresses = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                    // where nic.OperationalStatus == OperationalStatus.Up // forza il dispositivo ad essere connesso
                select nic.GetPhysicalAddress().ToString()
            ).ToList();

        if (addresses.IndexOf(MAC_ADDRESS_CLIENTE) != -1 || addresses.IndexOf(MAC_ADDRESS_SVILUPPO) != -1)
        {
            
            //trovato
            Debug.Log("--------------------OK---------------> " + addresses);
            mac.text = ""/* + addresses*/;
            startButton.interactable = true;
        }
        else
        {
            //non trovato
            Debug.Log("--------------------ERROR---------------> " + addresses);
            mac.text = "PC non abilitato: "/* + addresses*/;
            startButton.interactable = false;
        }

        //var macAddr =
        //    (
        //        from nic in NetworkInterface.GetAllNetworkInterfaces()
        //            // where nic.OperationalStatus == OperationalStatus.Up // forza il dispositivo ad essere connesso
        //        select nic.GetPhysicalAddress().ToString()
        //    ).FirstOrDefault();

        // 9CEBE8B0B294 -0C5415DB124F - 0C5415DB124E - 0C5415DB1252 - 0E5415DB124E --> portatile Gioele
        // C86000CC99C2 --> PC-VR ufficio
        //if (macAddr.Equals("9CEBE8B0B294") || macAddr.Equals("0C5415DB124F") || macAddr.Equals("0E5415DB124E") || macAddr.Equals("0C5415DB124E") || macAddr.Equals("0C5415DB1252") || macAddr.Equals("C86000CC99C2") || macAddr.Equals("0023AE83B346") || macAddr.Equals("0649EC638AE3") || macAddr.Equals("000272389453"))
        //{
        //    Debug.Log("--------------------OK---------------> " + macAddr);
        //    mac.text = ""/* + macAddr*/;
        //    startButton.interactable = true;
        //}
        //else {
        //    Debug.Log("--------------------ERROR---------------> " + macAddr);
        //    mac.text = "PC non abilitato"/* + macAddr*/;
        //    startButton.interactable = false;
        //}
    }
}