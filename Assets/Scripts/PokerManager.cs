using System;
using System.Collections.Generic;
using UnityEngine;

public class PokerManager : MonoBehaviour
{
    [field: SerializeField]
    public GameObject CartaPrefab { get; set; }

    [SerializeField]
    public ConjuntoCartas baraja = new ConjuntoCartas();
    [SerializeField]
    public ConjuntoCartas cartasDescartadas = new ConjuntoCartas();
    [SerializeField]
    public ConjuntoCartas cartasJug01 = new ConjuntoCartas();
    [SerializeField]
    public ConjuntoCartas cartasJug02 = new ConjuntoCartas();
    [SerializeField]
    public ConjuntoCartas cartasJug03 = new ConjuntoCartas();
    [SerializeField]
    public ConjuntoCartas cartasJug04 = new ConjuntoCartas();

    private Transform[][] posicionesCartasJugadores = new Transform[4][];
    [SerializeField]
    public Transform[] posicionesCartasJugador01 = new Transform[5];
    [SerializeField]
    public Transform[] posicionesCartasJugador02 = new Transform[5];
    [SerializeField]
    public Transform[] posicionesCartasJugador03 = new Transform[5];
    [SerializeField]
    public Transform[] posicionesCartasJugador04 = new Transform[5];

    private void Start() {
        posicionesCartasJugadores = new Transform[][] { posicionesCartasJugador01, posicionesCartasJugador02, posicionesCartasJugador03, posicionesCartasJugador04 };

        baraja.RellenaMazoBasico();
        baraja.Barajea();
        ReparteCartas();
    }

    private void ReparteCartas() {
        cartasJug01 = baraja.RobaXCartas(baraja, 5);
        for (int i = 0; i < cartasJug01.cartas.Count; i++) {
            GameObject cartaGO = Instantiate(CartaPrefab,
                                                posicionesCartasJugador01[i].position,
                                                Quaternion.identity,
                                                posicionesCartasJugador01[i].parent);
            CartaComponente cartaComponent = cartaGO.GetComponent<CartaComponente>();
            cartaComponent.Inicializar(cartasJug01.cartas[i]);
            cartaGO.GetComponent<Selectable>().FaceUp = true;
        }
    }
    
}
