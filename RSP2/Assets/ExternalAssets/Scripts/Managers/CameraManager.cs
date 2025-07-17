using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        private InGameManager gameManager;
        private Camera mainCamera;
        private CinemachineVirtualCamera playerCamera;
        private CinemachineBasicMultiChannelPerlin playerNoise;

        private HashSet<CinemachineVirtualCamera> virtualCameras;

        private Coroutine cameraShakeCoroutine;

        [field: SerializeField] private float playerCameraPriority { get; set; }
        [field: SerializeField] private Vector2Int nPCCameraPriority { get; set; }

        [field: SerializeField] private CinemachineVirtualCamera currentCamera { get; set; }
        [field: SerializeField] private Vector2 maxShakingValue { get; set; }
        [field: SerializeField] private float maxShakingTime { get; set; }

        public void Initialize(InGameManager _gameManager)
        {
            gameManager = _gameManager;

            virtualCameras = new HashSet<CinemachineVirtualCamera>();
            if (SearchPlayerCamera())
            {
                currentCamera = playerCamera;
            }

        }

        public void AddCamera(CinemachineVirtualCamera newCamera)
        {
            if (!virtualCameras.Contains(newCamera))
            {
                virtualCameras.Add(newCamera);
                newCamera.Priority = nPCCameraPriority.x;
            }
        }

        public void RemoveCamera(CinemachineVirtualCamera targetCamera)
        {
            if (virtualCameras.Contains(targetCamera))
            {
                virtualCameras.Remove(targetCamera);
            }
        }

        public void CallCameraSwitching(CinemachineVirtualCamera targetCamera)
        {
            if (targetCamera == null)
            {
                ResetToPlayerCamera();
                return;
            }

            if (!virtualCameras.Contains(targetCamera))
            {
                virtualCameras.Add(targetCamera);
            }

            SwitchToNewCamera(targetCamera);
        }

        private bool SearchPlayerCamera()
        {
            mainCamera = Camera.main;
            playerCamera = FindObjectOfType<CameraZoomer>().transform.GetComponent<CinemachineVirtualCamera>();

            if (playerCamera == null) return false;

            playerNoise = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            playerNoise.enabled = true;
            playerNoise.m_AmplitudeGain = 0;
            playerNoise.m_FrequencyGain = 0;
            return true;

        }

        private void SwitchToNewCamera(CinemachineVirtualCamera newCamera)
        {
            if (currentCamera != playerCamera)
            {
                currentCamera.Priority = nPCCameraPriority.x;
            }

            newCamera.Priority = nPCCameraPriority.y;
            currentCamera = newCamera;
        }

        private void ResetToPlayerCamera()
        {
            if (currentCamera == playerCamera) return;

            currentCamera.Priority = nPCCameraPriority.x;

            currentCamera = playerCamera;
        }

        public void CallCameraShakeByHit(float intensity)
        {
            if (cameraShakeCoroutine != null)
            {
                StopCoroutine(cameraShakeCoroutine);
            }

            cameraShakeCoroutine = StartCoroutine(ShakeCameraByHit(intensity));
        }

        private IEnumerator ShakeCameraByHit(float intensity)
        {
            playerNoise.m_AmplitudeGain = intensity * maxShakingValue.x;
            playerNoise.m_FrequencyGain = maxShakingValue.y;
            yield return new WaitForSeconds(intensity * maxShakingTime);

            playerNoise.m_AmplitudeGain = 0;
            playerNoise.m_FrequencyGain = 0;

            yield return null;
        }

    }
}
