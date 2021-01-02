using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera VirtualCam; //On cree une variable de type CinemachineVirtualCam qu'on appelle VirtualCam
    private float ShakeTimer; //on cree une variable de type float

    private void Awake()
    {
        VirtualCam = GetComponent<CinemachineVirtualCamera>(); //on recupere le composant CinemachineVirtualCamera de l'objet auquel ce script est attache
    }

    /// <summary>
    /// on cree une fonction ShakeCamera publique car on va l'appler depuis d'autres scripts, qui prend en parametre deux variables de type float
    /// </summary>
    /// <param name="intensity"></param>
    /// <param name="time"></param>
    public void ShakeCamera(float intensity, float time)
    {
        //on cree une variable de type CinemachineBasicMultiChannelPerlin qu'on appelle CinemachineNoise
        //on recupere le composant CinemachineBasicMultiChannelPerlin de la Cinemachine VirtualCam
        //on associe ce composant à la variable CinemachineNoise
        CinemachineBasicMultiChannelPerlin CinemachineNoise = VirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        CinemachineNoise.m_AmplitudeGain = intensity; //on indique que le parametre AmplitudeGain du CinemachineBasicMultiChannelPerlin sera egal au parametre intensity declare quand la fonction sera appelee
        ShakeTimer = time; //on indique que la variable ShakeTimer sera egale au parametre time declare quand la fonction sera appelee
    }

    private void Update()
    {
        //on indique ce qui se passe si la valeur de la variable ShakeTime est superieure a 0
        if(ShakeTimer > 0)
        {
            ShakeTimer -= Time.deltaTime; //on fait diminuer cette valeur

            //si la valeur de la variable ShakeTimer devient inferieure ou egale a 0
            if (ShakeTimer <= 0f)
            {
                //on reucpere une nouvelle fois le composant CinemachineBasicMultiChannelPerlin de la variable CinemachineNoise
                CinemachineBasicMultiChannelPerlin CinemachineNoise = VirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                CinemachineNoise.m_AmplitudeGain = 0f; //On passe la valeur de ce composant a 0
            }
        }
    }
}
