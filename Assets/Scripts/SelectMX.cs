using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class SelectMX : MonoBehaviour {

    // Use this for initialization

    // public string main;

    //private Sprite mxddref;
    public Dropdown dDMain;
    int dropDownValue;


    public Dropdown dDAreaPosterioreDX;
    public Dropdown dDAreaAnterioreDX;
    public Dropdown dDAreaPosterioreSX;
    public Dropdown dDAreaAnterioreSX;



    public Sprite defaultSprite;
    public Sprite firstSprite;
    public Sprite secondSprite;
    public Sprite thirdSprite;
    public Sprite fourthSprite;
    

     void Start () {
        
        
        List<Dropdown.OptionData> m_Messages = getDefaultOptionsList();

        populateDD(dDAreaPosterioreDX, m_Messages);
        populateDD(dDAreaAnterioreDX, m_Messages);
        populateDD(dDAreaPosterioreSX, m_Messages);
        populateDD(dDAreaAnterioreSX, m_Messages);

        dDMain.onValueChanged.AddListener(delegate {
            MainDropdownValueChanged(dDMain);
        });

        dDAreaPosterioreDX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaPosterioreDX);
        });

        dDAreaAnterioreDX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaAnterioreDX);
        });

        dDAreaPosterioreSX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaPosterioreSX);
        });

        dDAreaAnterioreSX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaAnterioreSX);
        });
    }

    void DropdownValueChanged(Dropdown dDAree)
    {
        Debug.Log("AREA: " + dDAree + " ! VALORE: " + dDAree.value);

    }

    void MainDropdownValueChanged(Dropdown dD) {
        Debug.Log("scelta opzione: " + dD.value);
        List<Dropdown.OptionData> m_current_Messages = getOptionsList(dDMain.value);
        populateDD(dDAreaPosterioreDX, m_current_Messages);
        populateDD(dDAreaAnterioreDX, m_current_Messages);
        populateDD(dDAreaPosterioreSX, m_current_Messages);
        populateDD(dDAreaAnterioreSX, m_current_Messages);
    }

    private void populateDD(Dropdown dd, List<Dropdown.OptionData> options) {
        dd.options.Clear();

        foreach (Dropdown.OptionData message in options)
        {
            //Add each entry to the Dropdown
            dd.options.Add(message);
            //Debug.Log(message);
        }
        dd.value = 0;
    }

    private List<Dropdown.OptionData> getOptionsList(int selectedOption) {

        List<Dropdown.OptionData> m_Messages = new List<Dropdown.OptionData>();
        Dropdown.OptionData zero, one, two, three, four;

        zero = new Dropdown.OptionData();
        zero.text = "default";
        zero.image = defaultSprite;
        m_Messages.Add(zero);

        one = new Dropdown.OptionData();
        one.text = "option 1";
        one.image = firstSprite;
        m_Messages.Add(one);

        if (selectedOption > 0)
        {
            two = new Dropdown.OptionData();
            two.text = "option 2";
            two.image = secondSprite;
            m_Messages.Add(two);
        }
        if (selectedOption > 1)
        {
            three = new Dropdown.OptionData();
            three.text = "option 3";
            three.image = thirdSprite;
            m_Messages.Add(three);
        }
        if (selectedOption > 2)
        {
            four = new Dropdown.OptionData();
            four.text = "option 4";
            four.image = fourthSprite;
            m_Messages.Add(four);
        }

        return m_Messages;
    }

    private List<Dropdown.OptionData> getDefaultOptionsList() {
        
        return getOptionsList(0);
    }
}
