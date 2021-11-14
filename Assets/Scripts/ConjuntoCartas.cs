using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Clase que recoge una lista de DataCarta y incluye métodos a usar sobre esta.
/// </summary>
[System.Serializable]
public class ConjuntoCartas : IEnumerable {

    public List<Carta> cartas = new List<Carta>();

    /// <summary>
    /// Rellena un ConjuntoCartas con el mazo básico de 52 naipes.
    /// </summary>
    public void RellenaMazoBasico() {

        Carta carta;
        foreach (EPalo p in Enum.GetValues(typeof(EPalo))) {
            foreach (ERango r in Enum.GetValues(typeof(ERango))) {
                carta = new Carta(p, r);
                this.cartas.Add(carta);
            }
        }
    }

    /// <summary>
    /// Mezcla las cartas.
    /// </summary>
    public void Barajea() {
        Random rng = new Random();
        this.cartas = this.cartas.OrderBy(_ => rng.Next()).ToList();
    }

    /// <summary>
    /// Elimina un número de cartas y lo devuelve
    /// </summary>
    /// <returns>Las cartas eliminadas como ConjuntoCartas</returns>
    public ConjuntoCartas RobaXCartas(ConjuntoCartas fuenteDeRobo, int numCartasARobar) {
        ConjuntoCartas cartasRobadas = new ConjuntoCartas();
        cartasRobadas.cartas = fuenteDeRobo.cartas.GetRange(0, numCartasARobar);
        fuenteDeRobo.cartas.RemoveRange(0, numCartasARobar);

        return cartasRobadas;

        //return fuenteDeRobo.cartas.Take(numCartasARobar);
    }

    public IEnumerator GetEnumerator() {
        return ((IEnumerable)cartas).GetEnumerator();
    }
}