using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;


[RequireComponent( typeof( Interactable ) )]

public class BallSpawn : MonoBehaviour {

    [Header("Palla (Prefab)")]
    public GameObject Prefab;

    private TargetManager targetManager;

    public Toggle TGAreaPosterioreDX;
    public Toggle TGAreaAnterioreDX;
    public Toggle TGAreaPosterioreSX;
    public Toggle TGAreaAnterioreSX;


    private bool tg_ap_dx;
    private bool tg_aa_dx;
    private bool tg_ap_sx;
    private bool tg_aa_sx;

    private CanvasGroup targetCanvasGroup;

    /*[Header("Punto di creazione palla")]*/
    private Transform ballSpawnPoint;

	// Use this for initialization
	void Start () {
		ballSpawnPoint = GameObject.Find ("BallSpawnPoint").GetComponent<Transform>();
        targetManager = TargetManager.Instance;
        targetCanvasGroup = GameObject.Find("PanelTG").GetComponent<CanvasGroup>();
    }

	// Update is called once per frame
	void Update () {
	}

	private void HandHoverUpdate( Hand hand )
	{
        if (targetCanvasGroup.interactable == true)
        {
            tg_aa_dx = TGAreaAnterioreDX.GetComponent<Toggle>().isOn;
            tg_ap_dx = TGAreaPosterioreDX.GetComponent<Toggle>().isOn;
            tg_aa_sx = TGAreaAnterioreSX.GetComponent<Toggle>().isOn;
            tg_ap_sx = TGAreaPosterioreSX.GetComponent<Toggle>().isOn;

            Dictionary<string, bool> associazioneTargetArea = new Dictionary<string, bool>();

            associazioneTargetArea.Add("AreaAnterioreDX", tg_aa_dx);
            associazioneTargetArea.Add("AreaPosterioreDX", tg_ap_dx);
            associazioneTargetArea.Add("AreaAnterioreSX", tg_aa_sx);
            associazioneTargetArea.Add("AreaPosterioreSX", tg_ap_sx);

            targetManager.setAssociazioneTargetArea(associazioneTargetArea);
        }
        if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
		{
			GameObject tennisBall=Instantiate(Prefab, ballSpawnPoint.position, Quaternion.identity) as GameObject;
			Destroy (tennisBall, 15);
		}
	}
}


