using System.Collections.Generic;
using UnityEngine;

// 1. Estructura de la arista del grafo
public class Relacion
{
    public string Sujeto;
    public string Predicado;
    public string Objeto;
}

public class MotorInferencia : MonoBehaviour
{
    [Header("Conexión con el Físico del NPC")]
    [Tooltip("Arrastra aquí el Animator del NPC")]
    public Animator npcAnimator;

    // Base de conocimiento (El Grafo)
    private List<Relacion> baseConocimiento = new List<Relacion>();

    void Start()
    {
        // 2. Ontología: Cómo reacciona la especie
        baseConocimiento.Add(new Relacion { Sujeto = "NPC", Predicado = "Teme", Objeto = "Fuego" });
        baseConocimiento.Add(new Relacion { Sujeto = "NPC", Predicado = "Desea", Objeto = "Comida" });
        baseConocimiento.Add(new Relacion { Sujeto = "NPC", Predicado = "Odia", Objeto = "Arma" });

        // 3. Hechos: Qué cosas existen en el mundo
        baseConocimiento.Add(new Relacion { Sujeto = "Antorcha", Predicado = "EsUn", Objeto = "Fuego" });
        baseConocimiento.Add(new Relacion { Sujeto = "Manzana", Predicado = "EsUn", Objeto = "Comida" });
        baseConocimiento.Add(new Relacion { Sujeto = "Espada", Predicado = "EsUn", Objeto = "Arma" });
        baseConocimiento.Add(new Relacion { Sujeto = "Lanzallamas", Predicado = "EsUn", Objeto = "Fuego" });
    }

    // 4. Inferencia: Método ejecutado por los botones de la UI
    public void AnalizarObjetoJugador(string objetoJugador)
    {
        string categoriaObjeto = "";

        // Paso A: ¿Qué es este objeto? (Búsqueda de la relación "EsUn")
        foreach (var rel in baseConocimiento)
        {
            if (rel.Sujeto == objetoJugador && rel.Predicado == "EsUn")
            {
                categoriaObjeto = rel.Objeto;
                break;
            }
        }

        // Paso B: ¿Cómo reacciona el Orco a esta categoría? (Deducción y Animación)
        foreach (var rel in baseConocimiento)
        {
            if (rel.Sujeto == "NPC" && rel.Objeto == categoriaObjeto)
            {
                // Disparamos los Triggers físicos en lugar de usar sentencias IF rígidas
                if (rel.Predicado == "Teme") npcAnimator.SetTrigger("TrHuir");
                if (rel.Predicado == "Desea") npcAnimator.SetTrigger("TrComer");
                if (rel.Predicado == "Odia") npcAnimator.SetTrigger("TrAtacar");

                Debug.Log($"Inferencia: {objetoJugador} es {categoriaObjeto}. Acción: {rel.Predicado}");
                break;
            }
        }
    }
}