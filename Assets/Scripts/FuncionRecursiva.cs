using System.Collections.Generic;
using UnityEngine;

public class FuncionRecursiva : MonoBehaviour
{
    [Tooltip("Arrastrar el Animator del Guardian aqui")]
    public Animator Guard_Animator;

    public GameObject espada;
    public GameObject escudo;
    public GameObject salvoconducto;

    void Start()
    {
        espada.SetActive(false);
        escudo.SetActive(false);
        salvoconducto.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            ProcesarTrueque(player.Trueque);
        }
    }
    
    public void ProcesarTrueque(List<string> Trueque)
    {
        

        if (Trueque.Contains("Manzana") && !Trueque.Contains("Espada"))
        {
            Debug.Log("GUARDIAN: OH~ traes una manzana~ continua~");
            espada.SetActive(true);
            return; 
        }

        if (Trueque.Contains("Manzana") && Trueque.Contains("Espada") && !Trueque.Contains("Escudo"))
        {
            Debug.Log("GUARDIAN: OH~ Ya casi lo completas~ continua~");
            escudo.SetActive(true);
        }

        if (Trueque.Contains("Manzana") && Trueque.Contains("Espada") && Trueque.Contains("Escudo"))
        {
            Debug.Log("GUARDIAN: OH~ Felicidades~ Ahora wacha esto");
            salvoconducto.SetActive(true);
            Guard_Animator.SetTrigger("TrTruequeSi");
        }

        else
        {
            Debug.Log("GUARDIAN: Tu puedes pedazo de basura");
        }
    }
}