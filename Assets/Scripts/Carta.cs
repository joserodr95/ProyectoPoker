using System.Collections;
using UnityEngine;

[System.Serializable]
public class Carta {

    public string nombre;
    public EPalo palo;
    public ERango rango;

    public Carta() { }

    public Carta(EPalo palo, ERango rango) {
        this.palo = palo;
        this.rango = rango;
        this.nombre = this.ToString();
    }

    public override string ToString() {
        nombre = "";

        switch (rango) {
            case ERango.DOS:
                nombre += 2;
                break;
            case ERango.TRES:
                nombre += 3;
                break;
            case ERango.CUATRO:
                nombre += 4;
                break;
            case ERango.CINCO:
                nombre += 5;
                break;
            case ERango.SEIS:
                nombre += 6;
                break;
            case ERango.SIETE:
                nombre += 7;
                break;
            case ERango.OCHO:
                nombre += 8;
                break;
            case ERango.NUEVE:
                nombre += 9;
                break;
            case ERango.DIEZ:
                nombre += 10;
                break;
            case ERango.JOTA:
                nombre += 11;
                break;
            case ERango.REINA:
                nombre += 12;
                break;
            case ERango.REY:
                nombre += 13;
                break;
            case ERango.AS:
                nombre += 14;
                break;
            default:
                Debug.LogError("Rango de carta no implementado.");
                break;
        }

        switch (palo) {
            case EPalo.CORAZONES:
                nombre += "CO";
                break;
            case EPalo.DIAMANTES:
                nombre += "DI";
                break;
            case EPalo.PICAS:
                nombre += "PI";
                break;
            case EPalo.TREBOLES:
                nombre += "TR";
                break;
            default:
                Debug.LogError("Palo de carta no implementado.");
                break;
        }

        return nombre;
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
