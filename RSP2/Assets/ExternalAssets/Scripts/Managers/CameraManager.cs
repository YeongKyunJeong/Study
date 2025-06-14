using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        private GameManager gameManager;
        private Camera mainCamera;
        private CinemachineVirtualCamera playerCamera;
        private HashSet<CinemachineVirtualCamera> virtualCameras;

        [field: SerializeField] private float playerCameraPriority { get; set; }
        [field: SerializeField] private Vector2Int nPCCameraPriority { get; set; }

        [field: SerializeField] private CinemachineVirtualCamera currentCamera { get; set; }

        public void Initialize(GameManager _gameManager)
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
            if(targetCamera == null)
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
            return playerCamera != null;
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

    }
}
