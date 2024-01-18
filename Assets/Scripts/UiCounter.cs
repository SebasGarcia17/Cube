using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{   //tiempo que ha transcurrido
    private float myTime = 0;
    

    //tiempos
    private int minutes,seconds,cents;

    //referencia al TPM_text
    [SerializeField] private TMP_Text counterText;
        
    void Update()
    {

        //calcular el tiempo que ha transcurrido
        myTime += Time.deltaTime;

        //divide la cantidad de tiempo que ha pasado dividido 60 para obtener los minutos;
        minutes = (int)(myTime / 60f);
        //le resta la cantidad de minutos y los multiplica por 60 tomando solo la parte entera;
        seconds = (int)(myTime - minutes * 60f);

        //resta la parte entera del tiempo transcurrido y la multiplica por 100 tomando solo la parte entera;
        cents = (int)((myTime - (int)myTime) * 100f);

        //se toma el countertext y se la asigna su propiedad text y se utiliza el string.format para asignarle el formato que tendra;
        counterText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);

    }
    
}
