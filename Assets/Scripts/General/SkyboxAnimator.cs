using UnityEngine;

public class SkyboxAnimator : MonoBehaviour
{
    [Tooltip("������� �������")]
    public float speed = 0.5f;
    [Tooltip("���� ����������")]
    public float angle = 10f;

    Material skyMat;

    void Start()
    {
        // ������ ���������� ���������, ����� �� ������ ��������
        skyMat = RenderSettings.skybox = Instantiate(RenderSettings.skybox);
    }

    void Update()
    {
        // ����� ��� �������� �����-����
        float rot = Mathf.Sin(Time.time * speed) * angle;
        skyMat.SetFloat("_Rotation", rot);
    }
}
