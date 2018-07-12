using UnityEngine;
using UnityEngine.UI;

public class HandleTextFile : MonoBehaviour
{

    public InputField fieldPath;
    public InputField fieldNome;
    public GameObject alert;
    public GameObject summaryPanel;

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
            scoreManager.setUtente(fieldPath.text, fieldNome.text, "05/07/2018");
            scoreManager.creaNuovaSessione();
            alert.active = false;
            CloseSommario();
        }
    }

    public void testSession() {
        scoreManager.annullaSessione();
    }

    public void CloseSommario()
    {
        summaryPanel.active = false;
    }

}