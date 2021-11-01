using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Clase que recoge una lista de DataCarta y incluye métodos a usar sobre esta.
/// </summary>
public class ConjuntoCartas {
    public List<DataCarta> cartas = new List<DataCarta>();

    /// <summary>
    /// Rellena un ConjuntoCartas con el mazo básico de 52 naipes.
    /// </summary>
    public void RellenaMazoBasico() {

        DataCarta dc;
        foreach (EPalo p in Enum.GetValues(typeof(EPalo))) {
            foreach (ERango r in Enum.GetValues(typeof(ERango))) {
                dc = new DataCarta(p, r);
                this.cartas.Add(dc);
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
}