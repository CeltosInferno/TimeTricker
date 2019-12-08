using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFlashAndShake : MonoBehaviour
{
    //Contient une fonction de coroutine ajoutant un flash et un effet de shake à la caméra lors du début d'une vague

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;

    public IEnumerator FlashAndShake(float duration, float intensity)
    {
        if (VirtualCamera)
        {
            //On utilise l'attribut noise de la cinemachine virtual camera
            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
            Debug.Log("Tellement Shaky !");
            yield return new WaitForSeconds(duration);
            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        }

    }
}
