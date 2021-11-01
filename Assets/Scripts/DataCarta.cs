using System.Collections;
using UnityEngine;

public class DataCarta : MonoBehaviour {

    public string name;
    public EPalo palo;
    public ERango rango;

    public DataCarta() { }

    public DataCarta(EPalo palo, ERango rango) {
        this.palo = palo;
        this.rango = rango;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public override string ToString() {
        name = "";

        switch (rango) {
            case ERango.DOS:
                name += 2;
                break;
            case ERango.TRES:
                name += 3;
                break;
            case ERango.CUATRO:
                name += 4;
                break;
            case ERango.CINCO:
                name += 5;
                break;
            case ERango.SEIS:
                name += 6;
                break;
            case ERango.SIETE:
                name += 7;
                break;
            case ERango.OCHO:
                name += 8;
                break;
            case ERango.NUEVE:
                name += 9;
                break;
            case ERango.DIEZ:
                name += 10;
                break;
            case ERango.JOTA:
                name += 11;
                break;
            case ERango.REINA:
                name += 12;
                break;
            case ERango.REY:
                name += 13;
                break;
            case ERango.AS:
                name += 14;
                break;
            default:
                Debug.LogError("Rango de carta no implementado.");
                break;
        }

        switch (palo) {
            case EPalo.CORAZONES:
                name += "CO";
                break;
            case EPalo.DIAMANTES:
                name += "DI";
                break;
            case EPalo.PICAS:
                name += "PI";
                break;
            case EPalo.TREBOLES:
                name += "TR";
                break;
            default:
                Debug.LogError("Palo de carta no implementado.");
                break;
        }

        return name;
    }
}

public enum EPalo {
    CORAZONES = 1,
    DIAMANTES = 2,
    PICAS = 3,
    TREBOLES = 4
}

public enum ERango {
    DOS = 2,
    TRES = 3,
    CUATRO = 4,
    CINCO = 5,
    SEIS = 6,
    SIETE = 7,
    OCHO = 8,
    NUEVE = 9,
    DIEZ = 10,
    JOTA = 11,
    REINA = 12,
    REY = 13,
    AS = 14
}