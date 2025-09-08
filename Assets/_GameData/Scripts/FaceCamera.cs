using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        // Her seferinde Camera.main demek yavaş olabilir, en başta referansını alıyoruz.
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        // Her frame, objenin yönünü kameranın yönüyle aynı yap.
        transform.rotation = mainCameraTransform.rotation;
    }
}