using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HandleTextFile : MonoBehaviour
{
    public InputField fieldNome;
    private string nome;

    public Text fieldCorretti;
    private string corretti;

    public Text fieldTotali;
    private string totali;

    StreamWriter sw;

    void Start()
    {
        
    }

    public void saveData()
    {
        nome = fieldNome.text;
        corretti = fieldCorretti.text;
        totali = fieldTotali.text;
        if (nome == "")
        {
            Debug.Log("VUOTO!!");
        }
        else {
            
            sw = new StreamWriter(nome + ".txt");   //The file is created or Overwritten outside the Assests Folder.
            sw.WriteLine("Nome utente " + nome + ". Data:  " + System.DateTime.Now, true);
            sw.WriteLine("Corretti " + corretti + " / Totali:  " + totali, true);
            sw.Flush();
        }
    }

}