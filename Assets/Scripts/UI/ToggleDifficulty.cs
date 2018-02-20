using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleDifficulty : MonoBehaviour {


    // Aggancio per la variazione del target di potenza
    [Header("Target lancio")]
    public GameObject _target;

    // Toggle UI per cambiare il livello

    [Header("Toggle Livelli Velocità (UI)")]
    public Toggle ToggleVelocitaLivello1;
    public Toggle ToggleVelocitaLivello2;
    public Toggle ToggleVelocitaLivello3;
    public Toggle ToggleVelocitaLivello4;

    [Header("Toggle Livelli Traiettoria (UI)")]
    public Toggle ToggleTraiettoriaLivello1;
    public Toggle ToggleTraiettoriaLivello2;
    public Toggle ToggleTraiettoriaLivello3;
    public Toggle ToggleTraiettoriaLivello4;

    [Header("Toggle Livelli Partenza (UI)")]
    public Toggle TogglePartenzaLivello1;
    public Toggle TogglePartenzaLivello2;
    public Toggle TogglePartenzaLivello3;
    public Toggle TogglePartenzaLivello4;

    [Header("Toggle Livelli Colore Palla (UI)")]
    public Toggle ToggleColoriLivello1;
    public Toggle ToggleColoriLivello2;
    public Toggle ToggleColoriLivello3;
    public Toggle ToggleColoriLivello4;
    

    // Punto di origine del lancio

    [Header("Origine lancio")]
    public GameObject origineLancio;

    [Header("Avatar avversario")]
    public GameObject avversario;

    [Header("Target velocità lancio")]
    // Punti per la potenza del lancio
    public GameObject VelocitaLivello1;
    public GameObject VelocitaLivello2;
    public GameObject VelocitaLivello3;
    public GameObject VelocitaLivello4;

    // modificatore di traiettoria (n.b.: separato dalla variazione in percentuale)
    private Quaternion targetRotation;

    // Min Max (espresso in angolo) della variazione di traiettoria
    private int _minAngTarget;
    private int _maxAngTarget;

    private int _minPosStart;
    private int _maxPosStart;

    private float x;
    private float y;
    private float z;
    private Vector3 pos;


    // Use this for initialization

    void Start() {

        // settaggi di potenza e variabilità traiettoria iniziali (Livello 1)

        _target.transform.position = VelocitaLivello1.transform.position;
        _minAngTarget = -2;
        _maxAngTarget = 2;

        x = 0;
        y = 0f;
        z = Random.Range(-0.5f, 0.5f);

        pos = new Vector3(x, y, z);
    }

	// Update is called once per frame
	void Update () {

	}

	public void ActiveToggle () {

		relocateTarget ();
       
        // Variazione livelli velocità
		if (ToggleVelocitaLivello1.isOn) {
			_target.transform.position = VelocitaLivello1.transform.position;

		} else if (ToggleVelocitaLivello2.isOn) {
			_target.transform.position = VelocitaLivello2.transform.position;


		} else if (ToggleVelocitaLivello3.isOn) {
			_target.transform.position = VelocitaLivello3.transform.position;


		} else if (ToggleVelocitaLivello4.isOn) {
            _target.transform.position = VelocitaLivello4.transform.position;

        }


        // Variazione livelli traiettoria
        if (ToggleTraiettoriaLivello1.isOn)
        {
            _minAngTarget = 0;
            _maxAngTarget = 0;

        }
        else if (ToggleTraiettoriaLivello2.isOn)
        {
            _minAngTarget = -4;
            _maxAngTarget = 4;

        }
        else if (ToggleTraiettoriaLivello3.isOn)
        {
            _minAngTarget = -8;
            _maxAngTarget = 8;

        }
        else if (ToggleTraiettoriaLivello4.isOn)
        {
            _minAngTarget = -12;
            _maxAngTarget = 12;

        }

        // Variazione livelli posizione di partenza
         if (TogglePartenzaLivello1.isOn)
         {
            z = Random.Range(-0.5f, 0.5f);

        }
         else if (TogglePartenzaLivello2.isOn)
         {
            z = Random.Range(-1f, 1f);

        }
         else if (ToggleTraiettoriaLivello3.isOn)
         {
            z = Random.Range(-1.5f, 1.5f);

        }
         else if (TogglePartenzaLivello4.isOn)
         {
            z = Random.Range(-2f, 2f);

        }


        ColorManager cm = ColorManager.Instance;

        // Variazione Colori palle
        if (ToggleColoriLivello1.isOn)
        {
            cm.setMaxColorIndex(3);
        }
        else if (ToggleColoriLivello2.isOn)
        {
            cm.setMaxColorIndex(4);
        }
        else if (ToggleColoriLivello3.isOn)
        {
            cm.setMaxColorIndex(5);
        }
        else if (ToggleColoriLivello4.isOn)
        {
            cm.setMaxColorIndex(6);
        }
        

        relocateTarget();
    }

    // Riposiziono il target al momento della difficoltà scelta

	public void relocateTarget() {
		targetRotation = Quaternion.Euler(0, Random.Range(_minAngTarget, _maxAngTarget), 0);
        origineLancio.transform.localRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15.0f);
        pos = new Vector3(x, y, z);
        avversario.transform.localPosition = pos;

	}

	public void OnSubmit () {
		ActiveToggle();
	}
		
}
 