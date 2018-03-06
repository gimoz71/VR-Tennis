using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawnFire : MonoBehaviour {

    private BallTextureManager balltextureManager;

    public Animator AvatarAnim;

    [Header("Colore Palla (Prefab)")]
    public Texture BaseTexture;
    public Texture BlueTexture;
    public Texture FucsiaTexture;
    public Texture OrangeTexture;
    public Texture RedTexture;

    [Header("Simbolo Palla (Prefab)")]
    public Texture ATexture;
    public Texture BTexture;
    public Texture CTexture;
    public Texture DTexture;
    public Texture ETexture;

    [Header("Palla (Prefab)")]
    public GameObject Prefab;

    [Header("Origine lancio")]
    public Transform playerTransform;
        
    [Header("Sorgente Lancio")]
    public Transform source;

    [Header("Target Lancio")]
    public Transform target;

    [Header("Modificatore di potenza")]
    public int pulseForce;

    [Header("Selettore difficoltà (UI)")]
    public GameObject GetActiveToggle;

    [Header("Modificatori di sequenza lancio (UI)")]
    public GameObject IntervalSlider;
    public GameObject QuantitySlider;
    public GameObject DelaySlider;

    [Header("Aggancio difficoltà (trigger ActiveToggle)")]
    public ToggleDifficulty _ToggleDifficultyScript;

    private float interval = 4;
    private float quantity = 10;
    private float delay = 4;

    private CanvasGroup CanvasSwitch;


    private void Start()
    {
        balltextureManager = BallTextureManager.Instance;

        AvatarAnim = GameObject.Find("AvatarAvversario").GetComponent<Animator>();
        CanvasSwitch = GameObject.Find("[MENU ISTRUTTORE (UI)]").GetComponent<CanvasGroup>();

        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_A, BaseTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_B, BlueTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_C, FucsiaTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_D, OrangeTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_E, RedTexture);

        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_A, ATexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_B, BTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_C, CTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_D, DTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_E, ETexture);
    }

    // Lancio palle come da parametri settati negli sliders
    public void SerialFire()
    {
        float interval = IntervalSlider.GetComponent<Slider>().value;
        float quantity = QuantitySlider.GetComponent<Slider>().value;
        float delay = DelaySlider.GetComponent<Slider>().value;

        // avvio la routine di ritardo (che lancia la sequenza lancio)
        StartCoroutine(ritardoLancio(delay, quantity, interval));
    }

    // Funzione parametro di ritardo per il loop base del lancio
    public IEnumerator ritardoLancio(float myDelay, float myQuantity, float myInterval)
    {
        //yield return new WaitForSeconds(3);
        yield return new WaitForSeconds(myDelay);
        yield return StartCoroutine(sequenzaLancio(myQuantity, myInterval));
    }

	// loop base del lancio
	public IEnumerator sequenzaLancio(float count, float separation) {
        _ToggleDifficultyScript.ActiveToggle();

        CanvasSwitch.interactable = false;
        for (int i = 0; i < count; i++) {
            
            // toggle avatar animation
            yield return new WaitForSeconds(0.5f);
            AvatarAnim.SetBool("Start", true);
            yield return new WaitForSeconds(0.75f);
            fire();
            yield return new WaitForSeconds(2);
            AvatarAnim.SetBool("Start", false);
            yield return new WaitForSeconds (separation);
            if(i == count-1)
            {
                //Debug.Log("FINE LOOP----------------");
                CanvasSwitch.interactable = true;
            }
        }
	}

    // Funziona base lancio palle
    public void fire()
    {

        // Creo L'istanza del prefab della pallina
        GameObject tennisBall = Instantiate(Prefab, playerTransform.position, Quaternion.identity) as GameObject;
           
        // Lancio l'istanza nella scena in base ai parametri di forza e rotazione
        tennisBall.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);
        tennisBall.GetComponent<Rigidbody>().AddTorque(0,0,-0.2f);

        // trovo la mesh della pallina (child della pallina)) e gli assegno una texture random tra quelle definite in textureManager.cs
        GameObject palla = tennisBall.transform.Find("TennisBall/ball").gameObject;
        Renderer pallaPrefab = palla.GetComponent<Renderer>();
        //balltextureManager.stampaMappa();
        pallaPrefab.material.mainTexture = balltextureManager.RandomTexture();
        
        // Distruggo la pallina dopo N secondi
        Destroy(tennisBall, 15);
    }
}

