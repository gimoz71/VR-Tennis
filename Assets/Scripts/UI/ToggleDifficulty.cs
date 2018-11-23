using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleDifficulty : MonoBehaviour {

    //managers
    private DMDrawManager dmDrawManager;
    private ScoreManager scoreManager;


    // Aggancio per la variazione del target di potenza
    [Header("Target lancio")]
    public GameObject _target;

    // Toggle UI per cambiare il livello

    [Header("Dropdown Livelli Velocità/Traiettoria/Partenza (UI)")]
    public Dropdown dropDownVelocita;
    public Dropdown dropDownTraiettoria;
    public Dropdown dropDownPartenza;

    [Header("Toggle Livelli Colore/Texture Palla (UI)")]
    public Dropdown dropDownColore;
    public Dropdown dropDownSimbolo;

    [Header("Toggle Livelli Differenziazione (UI)")]
    public Dropdown dropDownDiff;

    [Header("Toggle Livelli Decision Making (UI)")]
    public Dropdown dropDownDM;

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

    [Header("Pannelli Opzioni (per cambio difficoltà)")]
    public CanvasGroup targetToggleGroup;
    public CanvasGroup multiColoreToggleGroup;
    public CanvasGroup multiSimboloToggleGroup;
    public CanvasGroup differenziazioneToggleGroup;
    public CanvasGroup decisionMakingToggleGroup;


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

    //public GameObject MCPanel;
    //public GameObject MSPanel;


    // Use this for initialization

    void Start() {

        dmDrawManager = DMDrawManager.Instance;
        scoreManager = ScoreManager.Instance;

        // settaggi di potenza e variabilità traiettoria iniziali (Livello 1)

        _target.transform.position = VelocitaLivello1.transform.position;
        _minAngTarget = 0;
        _maxAngTarget = 0;

        // setto all'avvio la posizione dell'avatar
        x = 0;
        y = 0f;
        z = Random.Range(-0f, 0f);

        pos = new Vector3(x, y, z);

        scoreManager.PartitaTemporanea.LivelloVelocita = 1;
        scoreManager.PartitaTemporanea.VariabilitaTraiettoria = "fissa";
        scoreManager.PartitaTemporanea.VariabilitaPartenza = "fissa";
    }

	// Update is called once per frame
	void Update () {

	}

	public void ActiveToggle () {

		relocateTarget ();

        // Variazione livelli velocità
        if (dropDownVelocita.value == 0)
        {
            _target.transform.position = VelocitaLivello1.transform.position;
            scoreManager.PartitaTemporanea.LivelloVelocita = 1;
        }
        else if (dropDownVelocita.value == 1)
        {
            _target.transform.position = VelocitaLivello2.transform.position;
            scoreManager.PartitaTemporanea.LivelloVelocita = 2;
        }
        else if (dropDownVelocita.value == 2)
        {
            _target.transform.position = VelocitaLivello3.transform.position;
            scoreManager.PartitaTemporanea.LivelloVelocita = 3;
        }
        else if (dropDownVelocita.value == 3)
        {
            _target.transform.position = VelocitaLivello4.transform.position;
            scoreManager.PartitaTemporanea.LivelloVelocita = 4;
        }


        // Variazione livelli traiettoria
        if (dropDownTraiettoria.value == 0)
        {
            _minAngTarget = 0;
            _maxAngTarget = 0;
            scoreManager.PartitaTemporanea.VariabilitaTraiettoria = "fissa";
        }
        else if (dropDownTraiettoria.value == 1)
        {
            _minAngTarget = -4;
            _maxAngTarget = 4;
            scoreManager.PartitaTemporanea.VariabilitaTraiettoria = "5%";
        }
        else if (dropDownTraiettoria.value == 2)
        {
            _minAngTarget = -8;
            _maxAngTarget = 8;
            scoreManager.PartitaTemporanea.VariabilitaTraiettoria = "10%";
        }
        else if (dropDownTraiettoria.value == 3)
        {
            _minAngTarget = -12;
            _maxAngTarget = 12;
            scoreManager.PartitaTemporanea.VariabilitaTraiettoria = "15%";
        }

        // Variazione livelli posizione di partenza
        if (dropDownPartenza.value == 0)
        {
            z = Random.Range(-0f, 0f);
            scoreManager.PartitaTemporanea.VariabilitaPartenza = "fissa";
        }
        else if (dropDownPartenza.value == 1)
        {
            z = Random.Range(-1f, 1f);
            scoreManager.PartitaTemporanea.VariabilitaPartenza = "5%";
        }
        else if (dropDownPartenza.value == 2)
        {
            z = Random.Range(-1.5f, 1.5f);
            scoreManager.PartitaTemporanea.VariabilitaPartenza = "10%";
        }
        else if (dropDownPartenza.value == 3)
        {
            z = Random.Range(-2f, 2f);
            scoreManager.PartitaTemporanea.VariabilitaPartenza = "15%";
        }


        BallTextureManager tm = BallTextureManager.Instance;

        if(multiColoreToggleGroup.interactable)
        {
            // Variazione Colori palle
            tm.setMaxTextureIndex(dropDownColore.value + 2);
            scoreManager.PartitaTemporanea.NumColoriMulticolore = dropDownColore.value + 1;

        } else if (multiSimboloToggleGroup.interactable)
        {
            // Variazione Colori palle
            tm.setMaxTextureIndex(dropDownSimbolo.value + 2);
            scoreManager.PartitaTemporanea.NumSimboliMultisimbolo = dropDownSimbolo.value + 1;
        } else if (decisionMakingToggleGroup.interactable)
        {
            // Decision Making
            dmDrawManager.setLivello(dropDownDM.value);
            scoreManager.PartitaTemporanea.LivelloDecisionMaking = dropDownDM.value + 1;
        }
        else
        {
            tm.setMaxTextureIndex(1);
            
        }


        DiffManager df = DiffManager.Instance;

        //Debug.Log("dropDownDiff.value: " + dropDownDiff.value);

        // Variazione livelli posizione di partenza
        if (dropDownDiff.value == 0)
        {
            df.setLivello1();
            
        }
        else if (dropDownDiff.value == 1)
        {
            df.setLivello2();
        }
        else if (dropDownDiff.value == 2)
        {
            df.setLivello3();
        }

        scoreManager.PartitaTemporanea.LivelloDifferenziazione = dropDownDiff.value + 1;

        relocateTarget();
    }

    // Riposiziono il target al momento della difficoltà scelta

	public void relocateTarget() {
		targetRotation = Quaternion.Euler(0, Random.Range(_minAngTarget, _maxAngTarget), 0);
        origineLancio.transform.localRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15.0f);
        pos = new Vector3(x, y, z);
        avversario.transform.localPosition = pos;
        //Debug.Log("RELOCATE!");
	}

	public void OnSubmit () {
		ActiveToggle();
	}
		
}
 