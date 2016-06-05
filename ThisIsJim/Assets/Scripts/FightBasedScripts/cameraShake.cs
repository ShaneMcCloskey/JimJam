using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour {
    public float magnitude = 0.5f;
    public float duration = 0.25f;
    public Camera mainCamera;

    public void Shake()
    {
        StartCoroutine(ShakeCamera());
    }

    public void Shake(float shakeMultiplier)
    {
        StartCoroutine(ShakeCamera(shakeMultiplier));
    }

    public IEnumerator ShakeCamera()
    {
        //print("shakee");

        float elapsed = 0.0f;

        Vector3 originalCamPos = mainCamera.transform.position;

        while (elapsed < duration)
        {
            print("SHAKE");

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float z = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            z *= magnitude * damper;

            mainCamera.transform.position = new Vector3(x, originalCamPos.y, z);

            yield return null;
        }

        mainCamera.transform.position = originalCamPos;
    }

    public IEnumerator ShakeCamera(float shakeMultiplier)
    {
        print("shakee");

        float elapsed = 0.0f;
        float newDuration = (duration *(shakeMultiplier * 1.5f));

        Vector3 originalCamPos = mainCamera.transform.position;

        while (elapsed < duration)
        {
            //print("SHAKE");

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = (Random.value * 2.0f - 1.0f) * shakeMultiplier;
            float z = (Random.value * 2.0f - 1.0f) * shakeMultiplier;
            x *= magnitude * damper;
            z *= magnitude * damper;

            mainCamera.transform.position = new Vector3(x, originalCamPos.y, z);

            yield return null;
        }

        mainCamera.transform.position = originalCamPos;
    }

}
