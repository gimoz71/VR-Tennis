using System;
using System.Collections.Generic;
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

    //MAPPA DEGLI SPRITE PER DISTRATTORI/TARGET
    // #TODO

    private int[,] matrice = new int[2,2];

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
        matrice[0, 0] = VALORE_MATRICE_VUOTO;
        matrice[0, 1] = VALORE_MATRICE_VUOTO;
        matrice[1, 0] = VALORE_MATRICE_VUOTO;
        matrice[1, 1] = VALORE_MATRICE_VUOTO;

        aree = new Dictionary<string, int[]>();
    }

    public void randomizeMatrix(int level) {
        //valori a coppie per differenziare solo dx e sx
        System.Random rand = new System.Random();
        int random = 0;
        switch (level) {
            case 0:
                random = rand.Next(0,2);
                if (random == 0)
                {
                    matrice[0, 0] = VALORE_MATRICE_TARGET;
                    matrice[0, 1] = VALORE_MATRICE_TARGET;
                    matrice[1, 0] = VALORE_MATRICE_VUOTO;
                    matrice[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else
                {
                    matrice[0, 0] = VALORE_MATRICE_VUOTO;
                    matrice[0, 1] = VALORE_MATRICE_VUOTO;
                    matrice[1, 0] = VALORE_MATRICE_TARGET;
                    matrice[1, 1] = VALORE_MATRICE_TARGET;
                }
                break;
            case 1:
                random = rand.Next(0,2);
                if (random == 0)
                {
                    matrice[0, 0] = VALORE_MATRICE_TARGET;
                    matrice[0, 1] = VALORE_MATRICE_TARGET;
                    matrice[1, 0] = VALORE_MATRICE_DISTRATTORE;
                    matrice[1, 1] = VALORE_MATRICE_DISTRATTORE;
                }
                else
                {
                    matrice[0, 0] = VALORE_MATRICE_DISTRATTORE;
                    matrice[0, 1] = VALORE_MATRICE_DISTRATTORE;
                    matrice[1, 0] = VALORE_MATRICE_TARGET;
                    matrice[1, 1] = VALORE_MATRICE_TARGET;
                }
                break;
            case 2:
                random = rand.Next(0,4);
                if (random == 0)
                {
                    matrice[0, 0] = VALORE_MATRICE_TARGET;
                    matrice[0, 1] = VALORE_MATRICE_TARGET;
                    matrice[1, 0] = VALORE_MATRICE_VUOTO;
                    matrice[1, 1] = VALORE_MATRICE_VUOTO;
                } else if(random == 1)
                {
                    matrice[0, 0] = VALORE_MATRICE_DISTRATTORE;
                    matrice[0, 1] = VALORE_MATRICE_DISTRATTORE;
                    matrice[1, 0] = VALORE_MATRICE_VUOTO;
                    matrice[1, 1] = VALORE_MATRICE_VUOTO;
                } else if (random == 2)
                {
                    matrice[0, 0] = VALORE_MATRICE_VUOTO;
                    matrice[0, 1] = VALORE_MATRICE_VUOTO;
                    matrice[1, 0] = VALORE_MATRICE_TARGET;
                    matrice[1, 1] = VALORE_MATRICE_TARGET;
                } else if (random == 3)
                {
                    matrice[0, 0] = VALORE_MATRICE_VUOTO;
                    matrice[0, 1] = VALORE_MATRICE_VUOTO;
                    matrice[1, 0] = VALORE_MATRICE_DISTRATTORE;
                    matrice[1, 1] = VALORE_MATRICE_DISTRATTORE;
                }
                break;
            case 3:
                random = rand.Next(0,4);
                if (random == 0)
                {
                    matrice[0, 0] = VALORE_MATRICE_TARGET;
                    matrice[0, 1] = VALORE_MATRICE_TARGET;
                    matrice[1, 0] = VALORE_MATRICE_VUOTO;
                    matrice[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else if (random == 1)
                {
                    matrice[0, 0] = VALORE_MATRICE_DISTRATTORE;
                    matrice[0, 1] = VALORE_MATRICE_DISTRATTORE;
                    matrice[1, 0] = VALORE_MATRICE_VUOTO;
                    matrice[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else if (random == 2)
                {
                    matrice[0, 0] = VALORE_MATRICE_VUOTO;
                    matrice[0, 1] = VALORE_MATRICE_VUOTO;
                    matrice[1, 0] = VALORE_MATRICE_TARGET;
                    matrice[1, 1] = VALORE_MATRICE_TARGET;
                }
                else if (random == 3)
                {
                    matrice[0, 0] = VALORE_MATRICE_VUOTO;
                    matrice[0, 1] = VALORE_MATRICE_VUOTO;
                    matrice[1, 0] = VALORE_MATRICE_DISTRATTORE;
                    matrice[1, 1] = VALORE_MATRICE_DISTRATTORE;
                }
                break;
            default:
                random = rand.Next(0, 2);
                if (random == 0)
                {
                    matrice[0, 0] = VALORE_MATRICE_TARGET;
                    matrice[0, 1] = VALORE_MATRICE_TARGET;
                    matrice[1, 0] = VALORE_MATRICE_VUOTO;
                    matrice[1, 1] = VALORE_MATRICE_VUOTO;
                }
                else
                {
                    matrice[0, 0] = VALORE_MATRICE_VUOTO;
                    matrice[0, 1] = VALORE_MATRICE_VUOTO;
                    matrice[1, 0] = VALORE_MATRICE_TARGET;
                    matrice[1, 1] = VALORE_MATRICE_TARGET;
                }
                break;

        }
    }

    public int[,] getMarkersMatrix() {
        randomizeMatrix(livello);
        return matrice;
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

    public bool checkCombination(string zone) {

        int[] coordinate = aree[zone];
        int valoreArea = matrice[coordinate[0], coordinate[1]];

        bool correct = false;

        switch (livello) {
            case 0:
                correct = (valoreArea == VALORE_MATRICE_TARGET); 
                break;
            case 1:
                correct = (valoreArea == VALORE_MATRICE_TARGET);
                break;
            case 2:
                if (valoreArea == VALORE_MATRICE_TARGET) {
                    correct = true;
                } else if (valoreArea == VALORE_MATRICE_DISTRATTORE) {
                    correct = false;
                } else {
                    //scambio le coordinate e ottengo il valore dell'area opposta.
                    // IMPORTANTE!!!! CAMBIO SOLO LA PRIMA COORDINATA IN QUANTO I VALORI SONO UGUALI A COPPIA. 
                    // SE VIENE IMPLEMENTATO IL VALORE PER OGNI SINGOLA AREA QUESTO NON VALE PIU'!!!!
                    int coordinataScambiata = (coordinate[0] == 0 ? 1 : 0);
                    int valoreAreaOpposta = matrice[coordinataScambiata, coordinate[1]];
                    correct = (valoreAreaOpposta == VALORE_MATRICE_DISTRATTORE);
                }
                break;
            case 3:
                if (valoreArea == VALORE_MATRICE_TARGET)
                {
                    correct = true;
                }
                else if (valoreArea == VALORE_MATRICE_DISTRATTORE)
                {
                    correct = false;
                }
                else {
                    //scambio le coordinate e ottengo il valore dell'area opposta
                    int coordinataScambiata = (coordinate[0] == 0 ? 1 : 0);
                    int valoreAreaOpposta = matrice[coordinataScambiata, coordinate[1]];
                    correct = (valoreAreaOpposta == VALORE_MATRICE_DISTRATTORE);
                }
                break;
            default:
                correct = (valoreArea == VALORE_MATRICE_TARGET);
                break;
        }

        return correct;
    }
}
