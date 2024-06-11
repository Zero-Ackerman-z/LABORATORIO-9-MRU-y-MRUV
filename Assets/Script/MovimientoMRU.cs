using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MovimientoMRU : MonoBehaviour
{
    public Transform[] objetivos; // Puntos de referencia
    public TMP_InputField inputVelocidadInicial;
    public TMP_InputField inputTiempo;
    public TMP_InputField inputAceleracion;
    public TextMeshProUGUI textoResultado;
    public AnimationCurve curvaMovimiento; // Curva de animación 
    private bool enMovimiento = false;
    private float velocidadInicial;
    private float tiempo;
    private float aceleracion;
    private int objetivoActualIndex = 0; 
    private bool moviendoseHaciaAtras = false; 
    private Tween movimientoTween; // Tween para el movimiento actual

    void Update()
    {
        if (enMovimiento)
        {
            MoverHaciaPuntoDeReferencia();
        }
    }

    void MoverHaciaPuntoDeReferencia()
    {
        // Obtener y limitar los valores de entrada
        velocidadInicial = Mathf.Clamp(float.Parse(inputVelocidadInicial.text), 0.1f, 100f);
        aceleracion = Mathf.Clamp(float.Parse(inputAceleracion.text), -50f, 50f);
        tiempo = Mathf.Clamp(float.Parse(inputTiempo.text), 0.1f, 10f);

        // Calcular  velocidad actual
        float velocidadActual = velocidadInicial + aceleracion * Time.deltaTime;

        // Calcular  distancia al objetivo
        Vector3 objetivoPosicion = objetivos[objetivoActualIndex].position;
        float distancia = Vector3.Distance(transform.position, objetivoPosicion);

        // Cancelar cualquier tween de movimiento previo
        if (movimientoTween != null && movimientoTween.IsActive())
        {
            movimientoTween.Kill();
        }

        // Crear un tween 
        movimientoTween = transform.DOMove(objetivoPosicion, distancia / velocidadActual)
            .SetEase(curvaMovimiento) // Aplicar  curva de animación
            .OnUpdate(MostrarResultado) 
            .OnComplete(CambiarObjetivo); 

        Vector3 direccion = (objetivoPosicion - transform.position).normalized;
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.DORotateQuaternion(rotacionObjetivo, 1f).SetEase(curvaMovimiento);
    }

    void CambiarObjetivo()
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

    void MostrarResultado()
    {
        float tiempoClamped = Mathf.Max(tiempo, 0.1f);

        float distancia = Vector3.Distance(objetivos[objetivoActualIndex].position, transform.position);
        float resultado = distancia / tiempoClamped;

        textoResultado.text = (Mathf.Round(resultado * 1000) / 1000f).ToString();
    }

    public void EstablecerMovimiento(bool estado)
    {
        enMovimiento = estado;
    }
}
