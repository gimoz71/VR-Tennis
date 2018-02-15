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

    private int _min;
    private int _max;

    // Use this for initialization

    void Start() {
		_target.transform.position = targetLivello1.transform.position;
		_min = -2;
		_max = 2;
	}

	// Update is called once per frame
	void Update () {

	}

	public void ActiveToggle () {

		relocateTarget ();
       
		if (livello1.isOn) {
			Debug.Log("Livello1");

			// Difficoltà palla
			_target.transform.position = targetLivello1.transform.position;
            _min = -2;
            _max = 2;

		} else if (livello2.isOn) {
			Debug.Log("Livello2");

			// Difficoltà palla
			_target.transform.position = targetLivello2.transform.position;
            _min = -4;
            _max = 4;

		} else if (livello3.isOn) {
			Debug.Log("Livello3");

			// Difficoltà palla
			_target.transform.position = targetLivello3.transform.position;
            _min = -8;
            _max = 8;

		} else if (livello4.isOn) {
			Debug.Log("Livello4");

			// Difficoltà palla
            _target.transform.position = targetLivello4.transform.position;
            _min = -12;
            _max = 12;

        }
    }

	public void relocateTarget() {
		targetRotation = Quaternion.Euler(0, Random.Range(_min, _max), 0);
		targetParent.transform.localRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15.0f);

	}

	public void OnSubmit () {
		ActiveToggle();
	}
		
}
 