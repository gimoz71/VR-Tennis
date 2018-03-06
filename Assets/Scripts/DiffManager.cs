using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffManager : MonoBehaviour {

    public void test()
    {
        string speed = "";
        string zone = "";

        Dictionary<string, int> indiciVelocita = new Dictionary<string, int>();
        indiciVelocita.Add("Lento", 0);
        indiciVelocita.Add("Medio", 1);
        indiciVelocita.Add("Veloce", 2);
        Dictionary<string, int> indiciZona = new Dictionary<string, int>();
        indiciZona.Add("FL", 0);
        indiciZona.Add("FR", 1);
        indiciZona.Add("BL", 2);
        indiciZona.Add("BR", 3);

        int[,] combinazioni = new int[,] { { 0, 1, 2, 3 }, { 0, 1, 2, 3 }, { 0, 1, 2, 3 } };

        int indiceVelocita = indiciVelocita[speed];
        int indiceZona = indiciZona[zone];

        //devo controllare che il punto colpito non sia nella riga o nella colonna del punto precedente
        //1. cercare l'indice di riga e colonna del precedente

        int indiceVelocitaPrecedente = 5;
        int indiceZonaPrecedente = 5;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (combinazioni[i, j].Equals(1))
                {
                    indiceVelocitaPrecedente = i;
                    indiceZonaPrecedente = j;
                    break;
                }
            }
        }

        if (indiceVelocita.Equals(indiceVelocitaPrecedente) || indiceZona.Equals(indiceZonaPrecedente))
        {
            //se uno dei due indici corrisponde allora l'attuale colpo è nella riga oppure nella colonna del colpo precedente ---> ERRORE

            //LANCIO L'ERRORE
        }

        //RESET DELLA MATRICE E
        //SEGNO IL NUOVO STATO DELLA MATRICE
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
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

    }
}
