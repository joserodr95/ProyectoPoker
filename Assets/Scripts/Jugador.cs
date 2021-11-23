using UnityEngine;

/// <summary>
/// Clase para guardar datos de los jugadores jugando.
/// </summary>
[System.Serializable]
public class Jugador {
    /// <summary>
    /// La posición en la que el jugador se va a situar en la mesa.
    /// Del 1 al 4, siendo el 1 el jugador en la parte baja y siguiendole 
    /// los siguiente jugadores en el sentido horario.
    /// </summary>
    internal int asiento;
    [SerializeField]
    internal ConjuntoCartas mano;
    internal Transform padreCartas;
    internal Vector3[] posicionesCartas = new Vector3[5];

    public Jugador(int asiento) {
        this.asiento = asiento;
        padreCartas = GameObject.Find($"Jugador0{asiento}").transform;
        switch (asiento) {
            case 1: {
                posicionesCartas = new Vector3[5]{
                    new Vector3(-2.5f, 0f, 0f),
                    new Vector3(-1.25f, 0f, 0f),
                    new Vector3(0.00f, 0f, 0f),
                    new Vector3(+2.5f, 0f, 0f),
                    new Vector3(+1.25f, 0f, 0f),
                };
                break;
            }


        }
    }
}
