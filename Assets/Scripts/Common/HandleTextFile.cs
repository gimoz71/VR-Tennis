using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HandleTextFile : MonoBehaviour
{

    public InputField fieldNome;
    public GameObject Alert;
    private string nome;

    public Text fieldCorretti;
    public Text fieldTotali;

    private string corretti;
    private string totali;

    private Text ProtocolloAttivo;


    StreamWriter sw;

    void Start()
    {
        Alert.active = false;
        ProtocolloAttivo = GameObject.Find("Protocollo Attivo").GetComponent<Text>();
    }

    public void saveData()
    {
        nome = fieldNome.text;
        corretti = fieldCorretti.text;
        totali = fieldTotali.text;
        if (nome == "")
        {
            Alert.active = true;
        }
        else {
            Alert.active = false;
            StreamWriter sw = new StreamWriter("/" + nome + ".txt", append: true);
            sw.WriteLine("Nome utente " + nome + " | Data:  " + System.DateTime.Now, true);
            sw.WriteLine("PROTOCOLLO: " + ProtocolloAttivo.text);
            sw.WriteLine("");
            sw.WriteLine("PARAMETRI BASE: " + GlobalScore.VelocitaStatus + " | " + GlobalScore.TraiettoriaStatus + " | " + GlobalScore.PartenzaStatus);
            sw.WriteLine("OPZIONI: " + GlobalScore.MCStatus + " | " + GlobalScore.MSStatus + " | " + GlobalScore.StroboStatus);
            sw.WriteLine("");
            sw.WriteLine(corretti + " / " + totali, true);
            sw.WriteLine("");
            sw.WriteLine("-----------------------------------------------------");
            sw.WriteLine("");
            sw.Flush();
        }
    }

}