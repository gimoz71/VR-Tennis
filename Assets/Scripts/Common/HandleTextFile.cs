using UnityEngine;
using UnityEngine.UI;

public class HandleTextFile : MonoBehaviour
{

    public InputField fieldPath;
    public InputField fieldNome;
    public GameObject alert;
    public GameObject summaryPanel;

    public Text nomeAtleta;

    private string nome;
    private ScoreManager scoreManager;
    

    void Start()
    {
        scoreManager = ScoreManager.Instance;
    }

    public void saveData()
    {
        

        if (fieldNome.text == "" || fieldPath.text == "")
        {
            alert.active = true;
        }
        else
        {
            scoreManager.setUtente(fieldPath.text, fieldNome.text, System.DateTime.Now.ToString("dd/MM/YY"));
            scoreManager.creaNuovaSessione();
            nomeAtleta.text = fieldNome.text;
            alert.active = false;
            CloseSommario();
        }
    }

    public void testSession() {
        scoreManager.annullaSessione();
        CloseSommario();
    }

    public void CloseSommario()
    {
        summaryPanel.active = false;
    }

}