using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovimientoMRUV : MonoBehaviour
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

    void Update()
    {
        if (enMovimiento)
        {
            MoverHaciaObjetivo();
        }
    }

    void MoverHaciaObjetivo()
    {
        Vector3 direccion = (objetivos[objetivoActualIndex].position - transform.position).normalized;
        velocidadInicial = float.Parse(inputVelocidadInicial.text);
        aceleracion = float.Parse(inputAceleracion.text);
        velocidadInicial += aceleracion * Time.deltaTime;
        transform.Translate(direccion * velocidadInicial * Time.deltaTime);

        if (Vector3.Distance(transform.position, objetivos[objetivoActualIndex].position) < 0.1f)
        {
            CambiarObjetivo();
        }

        MostrarResultado();
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

    public void CambiarObjetivo()
    {
        objetivoActualIndex = (objetivoActualIndex + 1) % objetivos.Length;
    }
}
