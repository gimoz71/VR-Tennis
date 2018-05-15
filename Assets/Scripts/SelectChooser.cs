using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class SelectChooser : MonoBehaviour {

    // Use this for initialization

    // public string main;

    private Sprite mxddref;
    private Dropdown dDMain;
    int dropDownValue;


    private Dropdown dDAreaPosterioreDX;
    private Dropdown dDAreaAnterioreDX;
    private Dropdown dDAreaPosterioreSX;
    private Dropdown dDAreaAnterioreSX;

    

    private Sprite defaultSprite;
    private Sprite firstSprite;
    private Sprite secondSprite;
    private Sprite thirdSprite;
    private Sprite fourthSprite;
    

    public void ChooseSelect (string context, string path, string path2) {

        Debug.Log(path+path2);
        dDMain = GameObject.Find(path + "/Dropdown").GetComponent<Dropdown>();
        //mxddref = GameObject.Find("MXDDRef").GetComponent<Sprite>();

        if(context == "MC")
        {
            defaultSprite = Resources.Load<Sprite>("UI/panel") as Sprite;
            firstSprite = Resources.Load<Sprite>("UI/redUI") as Sprite;
            secondSprite = Resources.Load<Sprite>("UI/fucsiaUI") as Sprite;
            thirdSprite = Resources.Load<Sprite>("UI/orangeUI") as Sprite;
            fourthSprite = Resources.Load<Sprite>("UI/blueUI") as Sprite;
        } else if (context == "MS")
        {
            defaultSprite = Resources.Load<Sprite>("UI/panel") as Sprite;
            firstSprite = Resources.Load<Sprite>("UI/a") as Sprite;
            secondSprite = Resources.Load<Sprite>("UI/b") as Sprite;
            thirdSprite = Resources.Load<Sprite>("UI/c") as Sprite;
            fourthSprite = Resources.Load<Sprite>("UI/d") as Sprite;
        }

        dDAreaPosterioreDX = GameObject.Find(path + "/" + path2 + "/DDAreaPosterioreDX").GetComponent<Dropdown>();
        dDAreaAnterioreDX = GameObject.Find(path + "/" + path2 + "/DDAreaAnterioreDX").GetComponent<Dropdown>();
        dDAreaPosterioreSX = GameObject.Find(path + "/" + path2 + "/DDAreaPosterioreSX").GetComponent<Dropdown>();
        dDAreaAnterioreSX = GameObject.Find(path + "/" + path2 + "/DDAreaAnterioreSX").GetComponent<Dropdown>();
        
        List<Dropdown.OptionData> m_Messages = getDefaultOptionsList();

        populateDD(dDAreaPosterioreDX, m_Messages);
        populateDD(dDAreaAnterioreDX, m_Messages);
        populateDD(dDAreaPosterioreSX, m_Messages);
        populateDD(dDAreaAnterioreSX, m_Messages);

        dDMain.onValueChanged.AddListener(delegate {
            //DropdownValueChanged(dDMain);
            List<Dropdown.OptionData> m_current_Messages = getOptionsList(dDMain.value);
            populateDD(dDAreaPosterioreDX, m_current_Messages);
            populateDD(dDAreaAnterioreDX, m_current_Messages);
            populateDD(dDAreaPosterioreSX, m_current_Messages);
            populateDD(dDAreaAnterioreSX, m_current_Messages);
        });
    }

    private void populateDD(Dropdown dd, List<Dropdown.OptionData> options) {
        dd.options.Clear();

        foreach (Dropdown.OptionData message in options)
        {
            //Add each entry to the Dropdown
            dd.options.Add(message);
            Debug.Log(message);
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

        if (selectedOption > 1)
        {
            two = new Dropdown.OptionData();
            two.text = "option 2";
            two.image = secondSprite;
            m_Messages.Add(two);
        }
        if (selectedOption > 2)
        {
            three = new Dropdown.OptionData();
            three.text = "option 3";
            three.image = thirdSprite;
            m_Messages.Add(three);
        }
        if (selectedOption > 3)
        {
            four = new Dropdown.OptionData();
            four.text = "option 4";
            four.image = fourthSprite;
            m_Messages.Add(four);
        }

        return m_Messages;
    }

    private List<Dropdown.OptionData> getDefaultOptionsList() {
        
        return getOptionsList(4);
    }
}
