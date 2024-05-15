using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovimientoMRU : MonoBehaviour
{
    public Transform[] objetivos; 
    public TMP_InputField inputVelocidadInicial;
    public TMP_InputField inputTiempo;
    public TMP_InputField inputAceleracion;
    public TextMeshProUGUI textoResultado;
    private bool enMovimiento = false;
    private float velocidadInicial;
    private float tiempo;
    private float aceleracion;
    private int objetivoActualIndex = 0; 
    private bool moviendoseHaciaAtras = false; 

    void Update()
    {
        if (enMovimiento)
        {
            MoverHaciaPuntoDeReferencia();
        }
    }

    void MoverHaciaPuntoDeReferencia()
    {
        Vector3 direccion = (objetivos[objetivoActualIndex].position - transform.position).normalized;
        velocidadInicial = float.Parse(inputVelocidadInicial.text);
        aceleracion = float.Parse(inputAceleracion.text);
        transform.Translate(direccion * velocidadInicial * Time.deltaTime);
        MostrarResultado();

        // Verifica si ha alcanzado el objetivo actual
        if (Vector3.Distance(transform.position, objetivos[objetivoActualIndex].position) < 0.1f)
        {
            if (moviendoseHaciaAtras && objetivoActualIndex == 0)
            {
                moviendoseHaciaAtras = false;
            }
            if (!moviendoseHaciaAtras)
            {
                objetivoActualIndex = (objetivoActualIndex + 1) % objetivos.Length;
            }
            else
            {
                objetivoActualIndex = (objetivoActualIndex - 1 + objetivos.Length) % objetivos.Length;
            }
            if (objetivoActualIndex == objetivos.Length - 1)
            {
                moviendoseHaciaAtras = true;

            }

        }
    }

    void MostrarResultado()
    {
        tiempo = float.Parse(inputTiempo.text);
        float distancia = Vector3.Distance(objetivos[objetivoActualIndex].position, transform.position);
        float resultado = distancia / tiempo;
        textoResultado.text = resultado.ToString("F4");
    }

    public void EstablecerMovimiento(bool estado)
    {
        enMovimiento = estado;
    }
}