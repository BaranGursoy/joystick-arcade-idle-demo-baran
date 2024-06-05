using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerVirtualCamera;
    [SerializeField] private float shakeIntensity = 1f;

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        _cinemachineBasicMultiChannelPerlin =
            playerVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        GameActions.ShakeCamera += ShakeCamera;
        GameActions.StopShakingCamera += StopShakingCamera;
    }

    private void OnDestroy()
    {
        GameActions.ShakeCamera -= ShakeCamera;
        GameActions.StopShakingCamera -= StopShakingCamera;
    }

    private void ShakeCamera()
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
    }

    private void StopShakingCamera()
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
