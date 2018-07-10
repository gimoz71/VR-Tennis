using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using Assets.Scripts.modelli;

// SINGLETON DEI PUNTEGGI (centralizzato)
public class ScoreManager {

    public static string HEADER_LINEA_1 = ";";
    public static string HEADER_LINEA_2 = "NOME ATLETA,NOME_ATLETA;";
    public static string HEADER_LINEA_3 = "DATA DI NASCITA,DATA_DI_NASCITA;";
    public static string HEADER_LINEA_4 = ";";
    public static string HEADER_LINEA_5 = ",DATA"
        +",PROTOCOLLO"
        +",NUMERO PALLE"
        +",TEMPO TRA LE PALLE (secondi)"
        +",COLPI CORRETTI"
        +",COLPI SBAGLIATI"
        +",COLPI FUORI"
        +",ACCURATEZZA"
        +",MEDIA DAL CENTRO RACCHETTA"
        +",LIVELLO VELOCITA'"
        +",VARIABILITA' TRAIETTORIA"
        +",VARIABILITA' PARTENZA"
        +",NUMERO COLORI MULTICOLORE"
        +",NUMERO SIMBOLI MULTISIMBOLO"
        +",LIVELLO DECISION MAKING"
        +",LIVELLO DIFFERENZIAZIONE"
        +",FREQUENZA STROBO"
        +",PERCENTUALE BUIO STROBO"
        +",LENTE CHIUSA"
        +",ALTERNANZA LENTI"
        +",PERCENTUALE HEAD EYE MOVEMENT";

    public static string RIGA_NUOVA_SESSIONE = "NUOVA SESSIONE ->;";

    public static int COLUMN_INDEX_DATA = 1;
    public static int COLUMN_INDEX_PROTOCOLLO = 2;
    public static int COLUMN_INDEX_NUM_PALLE = 3;
    public static int COLUMN_INDEX_TEMPO_TRA_PALLE = 4;
    public static int COLUMN_INDEX_COLPI_CORRETTI = 5;
    public static int COLUMN_INDEX_COLPI_SBAGLIATI = 6;
    public static int COLUMN_INDEX_COLPI_FUORI = 7;
    public static int COLUMN_INDEX_ACCURATEZZA = 8;
    public static int COLUMN_INDEX_MEDIA_CENTRO_RACCHETTA = 9;
    public static int COLUMN_INDEX_LIVELLO_VELOCITA = 10;
    public static int COLUMN_INDEX_VARIABILITA_TRAIETTORIA = 11;
    public static int COLUMN_INDEX_VARIABILITA_PARTENZA = 12;
    public static int COLUMN_INDEX_NUMERO_COLORI_MULTICOLORE = 13;
    public static int COLUMN_INDEX_NUMERO_SIMBOLI_MULTISIMBOLO = 14;
    public static int COLUMN_INDEX_LIVELLO_DECISION_MAKING = 15;
    public static int COLUMN_INDEX_LIVELLO_DIFFERENZIAZIONE = 16;
    public static int COLUMN_INDEX_FREQUENZA_STROBO = 17;
    public static int COLUMN_INDEX_PERCENTUALE_BUIO_STROBO = 18;
    public static int COLUMN_INDEX_LENTE_CHIUSA = 19;
    public static int COLUMN_INDEX_ALTERNANZA_LENTI = 20;
    public static int COLUMN_INDEX_PERCENTUALE_HEAD_EYE_MOVEMENT = 21;

    public static string FILE_PATH = "D:/";
    
    private string fileName = "";

    private Sessione Sessione { get; set; }
    public Partita PartitaTemporanea { get; set; }

    private static ScoreManager instance;
    private ScoreManager() { }

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreManager();
                instance.initManager();
            }
            
            return instance;
        }
    }

    public void initManager() {
        PartitaTemporanea = new Partita();
    }
    
    public void setUtente(string nome, string dataDiNascita) {
        fileName = nome.ToLower().Replace(" ", "_");
        Sessione = new Sessione();
        Sessione.NomeUtente = nome;
        Sessione.DataDiNascita = dataDiNascita;
    }

    public void salvaPartita() {
        if (Sessione != null) {
            //creo la riga da scrivere sul file
            string riga = "," + PartitaTemporanea.Data
                + "," + PartitaTemporanea.Protocollo
                + "," + (PartitaTemporanea.NumeroPalle == -1 ? "" : PartitaTemporanea.NumeroPalle + "")
                + "," + (PartitaTemporanea.TempoTraLePalle == -1 ? "" : PartitaTemporanea.TempoTraLePalle + "")
                + "," + (PartitaTemporanea.ColpiCorretti == -1 ? "" : PartitaTemporanea.ColpiCorretti + "")
                + "," + (PartitaTemporanea.ColpiSbagliati == -1 ? "" : PartitaTemporanea.ColpiSbagliati + "")
                + "," + (PartitaTemporanea.ColpiFuori == -1 ? "" : PartitaTemporanea.ColpiFuori + "")
                + "," + PartitaTemporanea.Accuratezza
                + "," + (PartitaTemporanea.MediaDalCentroRacchetta == -1F ? "" : PartitaTemporanea.MediaDalCentroRacchetta + "")
                + "," + (PartitaTemporanea.LivelloVelocita == -1 ? "" : PartitaTemporanea.LivelloVelocita + "")
                + "," + PartitaTemporanea.VariabilitaTraiettoria
                + "," + PartitaTemporanea.VariabilitaPartenza
                + "," + (PartitaTemporanea.NumColoriMulticolore == -1 ? "" : PartitaTemporanea.NumColoriMulticolore + "")
                + "," + (PartitaTemporanea.NumSimboliMultisimbolo == -1 ? "" : PartitaTemporanea.NumSimboliMultisimbolo + "")
                + "," + (PartitaTemporanea.LivelloDecisionMaking == -1 ? "" : PartitaTemporanea.LivelloDecisionMaking + "")
                + "," + (PartitaTemporanea.LivelloDifferenziazione == -1 ? "" : PartitaTemporanea.LivelloDifferenziazione + "")
                + "," + (PartitaTemporanea.FrequenzaStrobo == -1 ? "" : PartitaTemporanea.FrequenzaStrobo + "")
                + "," + PartitaTemporanea.PercentualeBuioStrobo
                + "," + PartitaTemporanea.LenteChiusa
                + "," + PartitaTemporanea.Alternanza
                + "," + PartitaTemporanea.PercentualeHeadEyeMovement
                + ";";
            //scrivere su file
            scriviRigaSuFile(riga);
        }
    }

    public void scriviRigaSuFile(string riga) {
        //il file esiste?
        string fileCompleteName = ScoreManager.FILE_PATH + fileName + ".csv";
        if (!File.Exists(fileCompleteName)) {
            //se non esiste lo creo
            StreamWriter outStreamHead = System.IO.File.CreateText(fileCompleteName);

            outStreamHead.WriteLine(ScoreManager.HEADER_LINEA_1);
            outStreamHead.WriteLine(ScoreManager.HEADER_LINEA_2.Replace("NOME_ATLETA",Sessione.NomeUtente));
            outStreamHead.WriteLine(ScoreManager.HEADER_LINEA_3.Replace("DATA_DI_NASCITA", Sessione.DataDiNascita));
            outStreamHead.WriteLine(ScoreManager.HEADER_LINEA_4);
            outStreamHead.WriteLine(ScoreManager.HEADER_LINEA_5);

            outStreamHead.Close();
        }

        //scrivo la riga
        StreamWriter outStream = new StreamWriter(fileCompleteName, append: true);
        outStream.WriteLine(riga);
        outStream.Close();
    }

    public void creaNuovaSessione() {
        scriviRigaSuFile(ScoreManager.RIGA_NUOVA_SESSIONE);
    }

    public void annullaSessione() {
        Sessione = null;
    }

}
