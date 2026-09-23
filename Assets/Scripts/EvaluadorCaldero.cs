using System.Collections.Generic;
using UnityEngine;

public class EvaluadorCaldero : MonoBehaviour
{
    [Tooltip("Arrastrar el Animator del Caldero aquí")]
    public Animator Felipe_Animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            ProcesarReceta(player.MochilaMochila);
        }
    }
    // Método que recibe el inventario del jugador tras activar el Trigger
    public void ProcesarReceta(List<string> MochilaMochila)
    {
        // 1. NEGACIÓN (NOT): Regla estricta de pureza
        // Equivalente lógico: \+ contiene(veneno).
        if (MochilaMochila.Contains("NutriLeche"))
        {
            Debug.Log("Fracaso Inmediato: Mezcla tóxica.");
            //Felipe_Animator.SetTrigger("TrFalloExplosion");
            return; // Actúa como el operador de corte (!), deteniendo el flujo
        }

        // 2. DISYUNCIÓN (OR) Y VERIFICACIÓN BÁSICA
        // Equivalente lógico: contiene(agua) AND (contiene(hierba) OR contiene(hongo))
        bool tieneBase = MochilaMochila.Contains("DelawarePunch");
        bool tieneActivo = MochilaMochila.Contains("Comino") || MochilaMochila.Contains("Hongo");

        // 3. CONJUNCIÓN (AND) E IMPLICACIÓN FINAL
        // Si la premisa compleja es verdadera, detonar la consecuencia
        if (tieneBase && tieneActivo)
        {
            Debug.Log("Deducción exitosa: Poción de vida creada.");
            Felipe_Animator.SetTrigger("TrPocionExito");
        }
        else
        {
            // Fallo por defecto bajo la Hipótesis del Mundo Cerrado
            Debug.Log("Fallo de inferencia: Los ingredientes no reaccionan de forma útil.");
            //Felipe_Animator.SetTrigger("TrFalloExplosion");
        }
    }
}
