using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSimulador : MonoBehaviour
{
    public MovimientoMRU movimientoMRU;
    public MovimientoMRUV movimientoMRUV;

    public void PausarSimulacion()
    {
        movimientoMRU.EstablecerMovimiento(false);
        movimientoMRUV.EstablecerMovimiento(false);
    }

    public void ReanudarSimulacion()
    {
        movimientoMRU.EstablecerMovimiento(true);
        movimientoMRUV.EstablecerMovimiento(true);
    }
    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
