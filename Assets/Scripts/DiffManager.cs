using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffManager {

    public Dictionary<string, int> indiciVelocita = new Dictionary<string, int>();
    public Dictionary<string, int> indiciZona = new Dictionary<string, int>();
    public int[,] combinazioni;
    public int livelloPrecedente;

    private static DiffManager instance;

    private DiffManager() { }

    public static DiffManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DiffManager();
                instance.Start();
            }

            return instance;
        }
    }

    public void Start()
    {
        Debug.Log("Start su istanza del DiffManager");

        indiciVelocita.Add("Lento", 0);
        indiciVelocita.Add("Medio", 1);
        indiciVelocita.Add("Veloce", 2);

        indiciZona.Add("AreaAnterioreSX", 0);
        indiciZona.Add("AreaAnterioreDX", 1);
        indiciZona.Add("AreaPosterioreSX", 2);
        indiciZona.Add("AreaPosterioreDX", 3);

        combinazioni = new int[,] { { 0, 0, 0, 0 } };

        livelloPrecedente = -1;
    }

    public void setLivello1()
    {
        if(livelloPrecedente != 1)
        {
            /*Debug.Log("Livello Precedente PRIMA: " + livelloPrecedente);*/
            combinazioni = new int[,] { { 0, 0, 0, 0 } }; 
            livelloPrecedente = 1;
            /*Debug.Log("Livello Precedente DOPO: " + livelloPrecedente);*/
        }
    }

    public void setLivello2()
    {
        if (livelloPrecedente != 2)
        {
            combinazioni = new int[,] { { 0 }, { 0 }, { 0 } };
            livelloPrecedente = 2;
        }
    }

    public void setLivello3()
    {
        if (livelloPrecedente != 3)
        {
            combinazioni = new int[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            livelloPrecedente = 3;
        }
    }

    public int getLivello() {
        return livelloPrecedente;
    }

    public bool checkCombination(string speed, string zone) {

        /* Debug.Log("************** INIZIO ******************");
         stampaCombinazioni();
         Debug.Log("********************************");*/

        Debug.Log("*******SPEED: " + speed);

        int indiceVelocita = (livelloPrecedente == 1 ? 0 : indiciVelocita[speed]);
        int indiceZona = (livelloPrecedente == 2 ? 0 : indiciZona[zone]);

        /*Debug.Log("indiceVelocita: " + indiceVelocita);
        Debug.Log("indiceZona " + indiceZona);*/

        int indiceVelocitaPrecedente = -1;
        int indiceZonaPrecedente = -1;

        int righe = combinazioni.GetLength(0);
        int colonne = combinazioni.GetLength(1);

        /*Debug.Log("righe " + righe);
        Debug.Log("colonne " + colonne);*/

        for (int i = 0; i < righe; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                if (combinazioni[i, j].Equals(1))
                {
                    indiceVelocitaPrecedente = (livelloPrecedente == 1 ? -1 : i);
                    indiceZonaPrecedente = (livelloPrecedente == 2 ? -1 : j);
                    break;
                }
            }
        }

        /*Debug.Log("indiceVelocitaPrecedente " + indiceVelocitaPrecedente);
        Debug.Log("indiceZonaPrecedente " + indiceZonaPrecedente);*/

        //reset della matrice + metto a 1 la cella della matrice corrispondente al colpo attuale
        for (int i = 0; i < righe; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                if (i.Equals(indiceVelocita) && j.Equals(indiceZona))
                {
                    combinazioni[i, j] = 1;
                }
                else {
                    combinazioni[i, j] = 0;
                }
            }
        }

        /*Debug.Log("************** FINE ******************");
        stampaCombinazioni();
        Debug.Log("********************************");*/

        if (indiceVelocitaPrecedente == -1 && indiceZonaPrecedente == -1) {

            //siamo nel primo colpo 
            return true;
        }
        
        if (indiceVelocita.Equals(indiceVelocitaPrecedente) || indiceZona.Equals(indiceZonaPrecedente))
        {
            //ho colpito nella stessa riga o nella stessa colonna della matrice rispetto al colpo precedente
            return false;
        }

        return true;
    }

    private void stampaCombinazioni() {
        int righe = combinazioni.GetLength(0);
        int colonne = combinazioni.GetLength(1);

        for (int i = 0; i < righe; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                Debug.Log("i: " + i + " j: " + j + " value: " + combinazioni[i,j]);
            }
        }
    }
}
