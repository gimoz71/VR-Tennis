using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class SelectMX : MonoBehaviour {

    public static int INDICE_RIGA_1 = 0;
    public static int INDICE_RIGA_2 = 1;

    public static int INDICE_COLONNA_1 = 0;
    public static int INDICE_COLONNA_2 = 1;

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

    public int[,] matrice;

    void Start() {


        List<Dropdown.OptionData> m_Messages = getDefaultOptionsList();

        populateDD(dDAreaPosterioreDX, m_Messages);
        populateDD(dDAreaAnterioreDX, m_Messages);
        populateDD(dDAreaPosterioreSX, m_Messages);
        populateDD(dDAreaAnterioreSX, m_Messages);

        dDAreaPosterioreDX.value = 1;

        dDMain.onValueChanged.AddListener(delegate {
            MainDropdownValueChanged(dDMain);
        });

        dDAreaPosterioreDX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaPosterioreDX, 0, 0);
        });

        dDAreaAnterioreDX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaAnterioreDX, 0, 1);
        });

        dDAreaPosterioreSX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaPosterioreSX, 1, 0);
        });

        dDAreaAnterioreSX.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dDAreaAnterioreSX, 1, 1);
        });

        matrice = new int[2, 2] {{0,0},{0,0}};
    }

    public int[,] getMatrice() {
        return matrice;
    }

    void MatrixColorExchange(int row, int column, int colorIndex) {
        //1 trovo le coordinate vecchie del colore (se ci sono)
        int oldRow = 2;//valore di default che non puo' mai essere raggiunto in quanto la matrice è 2X2
        int oldCol = 2;
        for (int i = 0; i < 2; i++) {
            for (int j=0; j<2; j++) {
                if (matrice[i,j] == colorIndex) {
                    oldRow = i;
                    oldCol = j;
                }
            }
        }

        //2 trovo il colore vecchio presente alle coordinate nuove
        int oldColorIndex = matrice[row, column];

        //3 eseguo lo scambio dei colori
        if (oldCol != 2 && oldRow != 2) {
            matrice[oldRow, oldCol] = oldColorIndex;
        }
        matrice[row, column] = colorIndex;
    }

    void RedrawInterfaceMatrix() {

        dDAreaPosterioreDX.value = matrice[INDICE_RIGA_1, INDICE_COLONNA_1];
        dDAreaAnterioreDX.value = matrice[INDICE_RIGA_1, INDICE_COLONNA_2];
        dDAreaPosterioreSX.value = matrice[INDICE_RIGA_2, INDICE_COLONNA_1];
        dDAreaAnterioreSX.value = matrice[INDICE_RIGA_2, INDICE_COLONNA_2];
    }

    void ResetMatrix() {
        matrice[0, 0] = 0;
        matrice[0, 1] = 0;
        matrice[1, 0] = 0;
        matrice[1, 1] = 0;
    }


    void DropdownValueChanged(Dropdown dDAree, int row, int column)
    {
        Debug.Log("AREA: " + dDAree + " ! VALORE: " + dDAree.value);
        MatrixColorExchange(row, column, dDAree.value);
        RedrawInterfaceMatrix();
    }

    void MainDropdownValueChanged(Dropdown dD) {
        Debug.Log("scelta opzione: " + dD.value);
        ResetMatrix();
        RedrawInterfaceMatrix();
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
