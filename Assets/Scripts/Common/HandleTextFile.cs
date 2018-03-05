using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HandleTextFile : MonoBehaviour
{
    
    public InputField fieldNome;
    public GameObject alert;
    private string nome;

    private PlayerState playerState;

    void Start()
    {
        playerState = PlayerState.Instance;
    }

    public void saveData()
    {
        playerState.playerName = fieldNome.text;
        if (playerState.playerName == "")
        {
            alert.active = true;
        }
        else
        {
            alert.active = false;
            File.WriteAllText("e:/"+ playerState.playerName + ".json", PlayerState.Instance.SaveToString());
        }
        
    }

}