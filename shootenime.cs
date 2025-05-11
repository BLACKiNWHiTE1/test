using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ �������
    public Transform player; // �����
    public float shootingInterval = 2f; // �������� ����� ����������
    private float timeSinceLastShot = 0f;
    public float bulletSpeed = 10f; // �������� �������

    void Update()
    {
        // ������� ����� � ������� ������
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // ������� �������

        timeSinceLastShot += Time.deltaTime;

        // ���� ������ ������ ��� ��������
        if (timeSinceLastShot >= shootingInterval)
        {
            ShootAtPlayer();
            timeSinceLastShot = 0f;
        }
    }

    void ShootAtPlayer()
    {
        // ������������ ����������� �� ����� � ������
        Vector3 direction = (player.position - transform.position).normalized;

        // ������� ������
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);

        // ����������� ���� ��������� � ������ �����������
        bullet.transform.rotation = Quaternion.LookRotation(direction);

        // ���� ��� Y ��������� �����, � ��� Z ������ ���� ������, ������������ ����
        bullet.transform.Rotate(90, 0, 0); // ������������ ���� ���, ����� ��� Z ���� ������

        // ��������� �������� �������
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}

using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ �������
    public Transform player; // �����
    public float shootingInterval = 2f; // �������� ����� ����������
    private float timeSinceLastShot = 0f;
    public float bulletSpeed = 10f; // �������� �������
    public float shootingDistance = 15f; // ������������ ��������� ��� ��������

 using UnityEngine;
using UnityEngine.SceneManagement; // ��� �������� ����� �������
using UnityEngine.UI; // ��� ����������� UI

public class LevelManager : MonoBehaviour
{
    public int requiredRings = 10; // ���������� �����, ����������� ��� ���������� ������
    private int collectedRings = 0; // ������� ����� ��� �������
    public Text ringCounterText; // ������ �� ����� ��� ����������� ���������� �����

    public string nextSceneName; // ��� ��������� ����� ��� ��������

    void Start()
    {
        UpdateRingCounter(); // ��������� ��������� �����������
    }

    public void CollectRing()
    {
        collectedRings++; // ����������� ���������� ��������� �����
        UpdateRingCounter(); // ��������� ��������� �����������

        if (collectedRings >= requiredRings)
        {
            CompleteLevel(); // ��������� �������
        }
    }

    private void UpdateRingCounter()
    {
        // ��������� ��������� ����������� ���������� �����
        if (ringCounterText != null)
        {
            ringCounterText.text = $"Rings: {collectedRings}/{requiredRings}";
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("������� ��������!");
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // ��������� �� ��������� �����
        }
        else
        {
            Debug.LogWarning("��������� ����� �� �������!");
        }
    }
}






--������ �� ��������
local screenGui = script.Parent
local shopFrame = screenGui:WaitForChild("Frame")-- ����� ��������
local openButton = screenGui:WaitForChild("OpenButton")-- ������ ��������
local closeButton = shopFrame:WaitForChild("CloseButton")-- ������ ��������

-- �������� ��������
openButton.MouseButton1Click:Connect(function()
    shopFrame.Visible = true-- ���������� �������
end)

-- �������� ��������
closeButton.MouseButton1Click:Connect(function()
    shopFrame.Visible = false-- ������ �������
end)







// ����



    using UnityEngine;

public class Shoot : MonoBehaviour
{
    public ParticleSystem particle; // ������� ������ ��� �������
    public AudioSource shootSource; // ���� ��������

    private bool isShooting = false; // ���� ��� ������������ ��������� ��������
    private int maxBullets = 20; // ������������ ���������� ����
    private int currentBullets = 20; // ������� ���������� ����
    private float reloadTime = 3f; // ����� ����������� (� ��������)

    void Update()
    {
        // ��������� ������� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBullets > 0 && !isShooting)
            {
                // ���� ���� ������� � �� ��������, ���������� �������
                StartCoroutine(ShootDelay());
            }
            else if (currentBullets <= 0 && !isShooting)
            {
                // ���� ������� �����������, �������� �����������
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator ShootDelay()
    {
        isShooting = true; // ������������� ���� ��������
        currentBullets--; // ��������� ���������� ����

        // ����������� �������
        particle.Play();
        shootSource.Play();

        yield return new WaitForSeconds(0.5f); // �������� ����� ����������

        isShooting = false; // ���������� ���� ��������
    }

    IEnumerator Reload()
    {
        isShooting = true; // ������������� ���� �������� (��������� ����� ��������)
        Debug.Log("�����������...");

        // ���������� ��������� ��� �������� ����������� (�����������)
        yield return new WaitForSeconds(reloadTime); // �������� ������� �����������

        currentBullets = maxBullets; // ��������������� �������
        isShooting = false; // ��������� �������� �����

        Debug.Log("����������� ���������! ���������� ����: " + currentBullets);
    }
}