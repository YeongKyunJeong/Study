using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class Bread : PooledObject
    {
        bool isFirstInitialization = false;

        private Rigidbody rigidbd;

        [Header("For Bread On Tray")]
        private Vector3 defaultPos;
        private Quaternion defaultRot;
        private Vector3 spawnLocalPos;

        private int myIndex;
        private Coroutine movingCoroutine;
        [field: SerializeField] private static Vector3 startForce = 5 * Vector3.back;



        public void SetBreadToBasket(Vector3 pos)
        {
            if (rigidbd == null) rigidbd = GetComponent<Rigidbody>();

            transform.position = pos;
            transform.rotation = Quaternion.Euler(0, 180, 0);

            if (movingCoroutine != null) StopCoroutine(movingCoroutine);
            movingCoroutine = StartCoroutine(ShootToBasket());

        }

        public void InitializeOnTray()
        {
            defaultPos = transform.localPosition;
            defaultRot = transform.localRotation;
            spawnLocalPos = defaultPos + new Vector3(0, 1, 1);
            gameObject.SetActive(false);
        }

        public void InitializeOnStall()
        {
            defaultPos = transform.position;
            defaultRot = transform.rotation;
            gameObject.SetActive(false);
        }


        public void MoveToTray(Transform from, float duration = 0.05f)
        {
            if (movingCoroutine != null) StopCoroutine(movingCoroutine);
            gameObject.SetActive(true);
            movingCoroutine = StartCoroutine(MoveToTrayAnimation(from, duration));
        }

        public void MoveToStall(Transform from, float duration = 0.05f)
        {
            if (movingCoroutine != null) StopCoroutine(movingCoroutine);
            gameObject.SetActive(true);
            movingCoroutine = StartCoroutine(MoveToStallAnimation(from, duration));
        }

        public IEnumerator ShootToBasket()
        {
            rigidbd.useGravity = false;
            transform.rotation = Quaternion.identity;
            yield return new WaitForSeconds(1f);

            rigidbd.AddForce(Random.Range(-0.1f, 0.1f) * Vector3.right, ForceMode.VelocityChange);
            rigidbd.AddForce(startForce, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.125f);
            rigidbd.useGravity = true;
            yield return null;
        }

        private IEnumerator MoveToTrayAnimation(Transform startTransform, float duration)
        {
            float t0 = Time.time;
            transform.localPosition = spawnLocalPos;
            Quaternion startLocalRot = startTransform.rotation;
            transform.localRotation = startLocalRot;
            while (true)
            {
                float u = (Time.time - t0) / duration;
                if (u >= 1f) break;
                transform.localPosition = Vector3.Lerp(spawnLocalPos, defaultPos, u);
                transform.localRotation = Quaternion.Lerp(startLocalRot, defaultRot, u);
                yield return null;
            }
            transform.localPosition = defaultPos;
            transform.localRotation = defaultRot;
        }

        private IEnumerator MoveToStallAnimation(Transform startTransform, float duration)
        {
            float t0 = Time.time;
            Vector3 startPos = startTransform.position;
            Quaternion startRot = startTransform.rotation;
            transform.position = startPos;
            transform.rotation = startRot;
            while (true)
            {
                float u = (Time.time - t0) / duration;
                if (u >= 1f) break;
                transform.position = Vector3.Lerp(startPos, defaultPos, u);
                transform.rotation = Quaternion.Lerp(startRot, defaultRot, u);
                yield return null;
            }
            transform.position = defaultPos;
            transform.rotation = defaultRot;
        }
    }
}
