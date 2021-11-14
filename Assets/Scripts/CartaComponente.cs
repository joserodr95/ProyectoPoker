using System.Collections;
using UnityEngine;

public class CartaComponente : MonoBehaviour {

    public string nombre;
    public EPalo palo;
    public ERango rango;
    public GameObject goOwner;
    //public int hileraNum;
    //public int posEnHilera;

    public void Inicializar(Carta carta) {
        this.palo = carta.palo;
        this.rango = carta.rango;
        this.nombre = carta.nombre;
        this.gameObject.name = this.nombre;
    }

    private void Start() {
        goOwner = this.gameObject;
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
