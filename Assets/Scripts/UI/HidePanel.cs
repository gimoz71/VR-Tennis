using UnityEngine;

public class HidePanel : MonoBehaviour {

    public GameObject Panel;
    private int counter;

	public void showHidePanel()
    {
        counter++;
        if (counter % 2 == 1) {
            Panel.gameObject.SetActive(false);
        } else
        {
            Panel.gameObject.SetActive(true);
        }
    }
}
