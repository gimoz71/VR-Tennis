using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class DMDrawManager {

    public static int VALORE_MATRICE_VUOTO = 0;
    public static int VALORE_MATRICE_TARGET = 1;
    public static int VALORE_MATRICE_DISTRATTORE = 2;

    public static int LIVELLO_1 = 1;
    public static int LIVELLO_2 = 2;
    public static int LIVELLO_3 = 3;
    public static int LIVELLO_4 = 4;

    private Dictionary<string, int[]> aree;

    int livello = 0;

    private bool isFirst;

    //MAPPA DEGLI SPRITE PER DISTRATTORI/TARGET
    // #TODO

    private int[,] matrice = new int[2, 2];
    private int[,] nextMatrice = new int[2, 2];

    private static DMDrawManager instance;
    private DMDrawManager() { }

    public static DMDrawManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DMDrawManager();
                instance.init();
            }

            return instance;
        }
    }

    private void init() {
        resetMatrix();
        aree = new Dictionary<string, int[]>();

        isFirst = true;
        Debug.Log("++++++++++ inizializzato il valore di isFirst a : " + isFirst);
    }

    public void resetMatrix() {
        matrice[0, 0] = VALORE_MATRICE_VUOTO;
        matrice[0, 1] = VALORE_MATRICE_VUOTO;
        matrice[1, 0] = VALORE_MATRICE_VUOTO;
        matrice[1, 1] = VALORE_MATRICE_VUOTO;
    }

    public int[,] generateRandomMatrix() {
        int[,] randomMatrix = new int[2, 2];
        System.Random rand = new System.Random();

        int random = 0;
        switch (livello)
        {
            case 0:
                random = rand.Next(0, 2);
                if (random.Equals(0))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[0, 1] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[0, 1] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 1] = VALORE_MATRICE_TARGET;
                }
                break;
            case 1:
                random = rand.Next(0, 2);
                if (random.Equals(0))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[0, 1] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 0] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[1, 1] = VALORE_MATRICE_DISTRATTORE;
                }
                else
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[0, 1] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[1, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 1] = VALORE_MATRICE_TARGET;
                }
                break;
            case 2:
                random = rand.Next(0, 4);
                if (random.Equals(0))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[0, 1] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else if (random.Equals(1))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[0, 1] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[1, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else if (random.Equals(2))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[0, 1] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 1] = VALORE_MATRICE_TARGET;
                }
                else if (random.Equals(3))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[0, 1] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 0] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[1, 1] = VALORE_MATRICE_DISTRATTORE;
                }
                break;
            case 3:
                random = rand.Next(0, 4);
                if (random.Equals(0))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[0, 1] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else if (random.Equals(1))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[0, 1] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[1, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else if (random.Equals(2))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[0, 1] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 1] = VALORE_MATRICE_TARGET;
                }
                else if (random.Equals(3))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[0, 1] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 0] = VALORE_MATRICE_DISTRATTORE;
                    randomMatrix[1, 1] = VALORE_MATRICE_DISTRATTORE;
                }
                break;
            default:
                random = rand.Next(0, 2);
                if (random.Equals(0))
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[0, 1] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else
                {
                    randomMatrix[0, 0] = VALORE_MATRICE_VUOTO;
                    randomMatrix[0, 1] = VALORE_MATRICE_VUOTO;
                    randomMatrix[1, 0] = VALORE_MATRICE_TARGET;
                    randomMatrix[1, 1] = VALORE_MATRICE_TARGET;
                }
                break;

        }

        return randomMatrix;
    }


    public int[,] getMarkersMatrix() {
        Debug.Log("--------------------------------------");
        Debug.Log("isFirst: " + isFirst);

        if (isFirst)
        {
            isFirst = false;
            matrice = generateRandomMatrix();
        }
        else {
            matrice = nextMatrice;
        }
        generateNextMatrix();
        Debug.Log("matrice_getMarkersMatrix: " + matrice);
        Debug.Log("--------------------------------------");
        return matrice;
    }

    public void generateNextMatrix() {
        nextMatrice = generateRandomMatrix();
    }

    public void setLivello(int level) {
        livello = level;
    }

    public int getLivello() {
        return livello;
    }

    public Dictionary<string, int[]> getAssociazioneDMArea() {
        return aree;
    }

    public void setAssociazioneDMArea(Dictionary<string, int[]> dic) {
        aree = dic;
    }

    public int [,] getCurrentMatrix()
    {
        return matrice;
    }

    public bool checkCombination(string zone, int[,] matriceAttuale) {

        int[] coordinate = aree[zone];
        /*Debug.LogError("zone: " + zone);
        Debug.LogError("aree: " + aree);
        Debug.LogError("coordinate: " + coordinate);
        Debug.LogError("matriceAttuale: " + matriceAttuale);*/
        int valoreArea = matriceAttuale[coordinate[0], coordinate[1]];

        Debug.Log("****************************");
        Debug.Log("zona_colpita: " + zone);
        Debug.Log("coordinate_zona_colpita: " + coordinate[0] +","+ coordinate[1]);
        Debug.Log("valore_area_colpita: " + valoreArea);
        Debug.Log("matrice: " + matrice);
        Debug.Log("matrice_next: " + nextMatrice);
        Debug.Log("livallo: " + livello);
        Debug.Log("****************************");

        switch (livello) {
            case 0:
                return (valoreArea.Equals(VALORE_MATRICE_TARGET)); 
            case 1:
                return (valoreArea.Equals(VALORE_MATRICE_TARGET));
            case 2:
                if (valoreArea.Equals(VALORE_MATRICE_TARGET)) {
                    return true;
                } else if (valoreArea.Equals(VALORE_MATRICE_DISTRATTORE)) {
                    return false;
                } else {
                    //scambio le coordinate e ottengo il valore dell'area opposta.
                    // IMPORTANTE!!!! CAMBIO SOLO LA PRIMA COORDINATA IN QUANTO I VALORI SONO UGUALI A COPPIA. 
                    // SE VIENE IMPLEMENTATO IL VALORE PER OGNI SINGOLA AREA QUESTO NON VALE PIU'!!!!
                    int coordinataScambiata = (coordinate[0].Equals(0) ? 1 : 0);
                    int valoreAreaOpposta = matriceAttuale[coordinataScambiata, coordinate[1]];
                    return (valoreAreaOpposta.Equals(VALORE_MATRICE_DISTRATTORE));
                }
            case 3:
                if (valoreArea.Equals(VALORE_MATRICE_TARGET))
                {
                    return true;
                }
                else if (valoreArea.Equals(VALORE_MATRICE_DISTRATTORE))
                {
                    return false;
                }
                else {
                    //scambio le coordinate e ottengo il valore dell'area opposta
                    int coordinataScambiata = (coordinate[0].Equals(0) ? 1 : 0);
                    int valoreAreaOpposta = matriceAttuale[coordinataScambiata, coordinate[1]];
                    return (valoreAreaOpposta.Equals(VALORE_MATRICE_DISTRATTORE));
                }
            default:
                return (valoreArea.Equals(VALORE_MATRICE_TARGET));
        }
    }

    public void setFirst() {
        isFirst = true;
    }
}
