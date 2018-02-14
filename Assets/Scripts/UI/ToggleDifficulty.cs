using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleDifficulty : MonoBehaviour {

	public GameObject _target;

	public Toggle livello1;
	public Toggle livello2;
	public Toggle livello3;
    public Toggle livello4;

    public GameObject targetParent;
    public GameObject targetLivello1;
    public GameObject targetLivello2;
    public GameObject targetLivello3;
    public GameObject targetLivello4;

    private Quaternion targetRotation;

    private int _min = -2;
    private int _max = 2;

    // Use this for initialization

    void Start() {
		
	}
	public void ActiveToggle () {

        targetRotation = Quaternion.Euler(0, Random.Range(_min, _max), 0);
        targetParent.transform.localRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15.0f);

        if (livello1.isOn) {
			_target.transform.position = targetLivello1.transform.position;
			Debug.Log("Livello1");
            _min = -4;
            _max = 4;

		} else if (livello2.isOn) {
			_target.transform.position = targetLivello2.transform.position;
			Debug.Log("Livello2");
            _min = -8;
            _max = 8;

        } else if (livello3.isOn) {
			_target.transform.position = targetLivello3.transform.position;
			Debug.Log("Livello3");
            _min = -12;
            _max = 12;

        } else if (livello4.isOn) {
            _target.transform.position = targetLivello4.transform.position;
            Debug.Log("Livello4");
            _min = -16;
            _max = 16;

        }
    }

	public void OnSubmit () {
		ActiveToggle();
	}

	// Update is called once per frame
	void Update () {
       
    }
}
 