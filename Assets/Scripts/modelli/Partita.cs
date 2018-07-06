using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.modelli
{
    public class Partita
    {
        //public string Data { get; set; };
        string dataValue = "";
        public string Data {
            get { return this.dataValue; }
            set { this.dataValue = value; }
        }

        string protocolloValue = "";
        public string Protocollo
        {
            get { return this.protocolloValue; }
            set { this.protocolloValue = value; }
        }
        int numeroPalleValue = -1;
        public int NumeroPalle
        {
            get { return this.numeroPalleValue; }
            set { this.numeroPalleValue = value; }
        }
        int tempoTraLePalleValue = -1;
        public int TempoTraLePalle
        {
            get { return this.tempoTraLePalleValue; }
            set { this.tempoTraLePalleValue = value; }
        }
        int colpiCorrettiValue = -1;
        public int ColpiCorretti
        {
            get { return this.colpiCorrettiValue; }
            set { this.colpiCorrettiValue = value; }
        }
        int colpiSbagliatiValue = -1;
        public int ColpiSbagliati
        {
            get { return this.colpiSbagliatiValue; }
            set { this.colpiSbagliatiValue = value; }
        }
        int colpiFuoriValue = -1;
        public int ColpiFuori
        {
            get { return this.colpiFuoriValue; }
            set { this.colpiFuoriValue = value; }
        }
        string accuratezzaValue = "";
        public string Accuratezza
        {
            get { return this.accuratezzaValue; }
            set { this.accuratezzaValue = value; }
        }
        float mediaDalCentroRacchettaValue = -1f;
        public float MediaDalCentroRacchetta
        {
            get { return this.mediaDalCentroRacchettaValue; }
            set { this.mediaDalCentroRacchettaValue = value; }
        }
        int livelloVelocitaValue = -1;
        public int LivelloVelocita
        {
            get { return this.livelloVelocitaValue; }
            set { this.livelloVelocitaValue = value; }
        }
        int numColoriMulticoloreValue = -1;
        public int NumColoriMulticolore
        {
            get { return this.numColoriMulticoloreValue; }
            set { this.numColoriMulticoloreValue = value; }
        }
        int numSimboliMultisimboloValue = -1;
        public int NumSimboliMultisimbolo
        {
            get { return this.numSimboliMultisimboloValue; }
            set { this.numSimboliMultisimboloValue = value; }
        }
        int livelloDecisionMakingValue = -1;
        public int LivelloDecisionMaking
        {
            get { return this.livelloDecisionMakingValue; }
            set { this.livelloDecisionMakingValue = value; }
        }
        int livelloDifferenziazioneValue = -1;
        public int LivelloDifferenziazione
        {
            get { return this.livelloDifferenziazioneValue; }
            set { this.livelloDifferenziazioneValue = value; }
        }
        float frequenzaStroboValue = -1f;
        public float FrequenzaStrobo
        {
            get { return this.frequenzaStroboValue; }
            set { this.frequenzaStroboValue = value; }
        }
        string variabilitaTraiettoriaValue = "";
        public string VariabilitaTraiettoria
        {
            get { return this.variabilitaTraiettoriaValue; }
            set { this.variabilitaTraiettoriaValue = value; }
        }
        string variabilitaPartenzaValue = "";
        public string VariabilitaPartenza
        {
            get { return this.variabilitaPartenzaValue; }
            set { this.variabilitaPartenzaValue = value; }
        }
        string percentualeBuioStroboValue = "";
        public string PercentualeBuioStrobo
        {
            get { return this.percentualeBuioStroboValue; }
            set { this.percentualeBuioStroboValue = value; }
        }
        string lenteChiusaValue = "";
        public string LenteChiusa
        {
            get { return this.lenteChiusaValue; }
            set { this.lenteChiusaValue = value; }
        }
        string alternanzaValue = "";
        public string Alternanza
        {
            get { return this.alternanzaValue; }
            set { this.alternanzaValue = value; }
        }
        string percentualeHeadEyeMovementValue = "";
        public string PercentualeHeadEyeMovement
        {
            get { return this.percentualeHeadEyeMovementValue; }
            set { this.percentualeHeadEyeMovementValue = value; }
        }
    }
}
