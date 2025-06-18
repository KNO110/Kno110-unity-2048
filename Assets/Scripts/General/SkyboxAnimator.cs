using UnityEngine;

public class SkyboxAnimator : MonoBehaviour
{
    [Tooltip("„астота качани€")]
    public float speed = 0.5f;
    [Tooltip("”гол отклонени€")]
    public float angle = 10f;

    Material skyMat;

    void Start()
    {
        // создаЄм уникальный экземпл€р, чтобы не мен€ть оригинал
        skyMat = RenderSettings.skybox = Instantiate(RenderSettings.skybox);
    }

    void Update()
    {
        // синус дл€ плавного Ђтуда-сюдаї
        float rot = Mathf.Sin(Time.time * speed) * angle;
        skyMat.SetFloat("_Rotation", rot);
    }
}
