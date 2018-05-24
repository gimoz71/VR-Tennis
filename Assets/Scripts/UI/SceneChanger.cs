using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

     public void sceneChange()
    {
        SceneManager.LoadScene("Tennis");
    }
}
