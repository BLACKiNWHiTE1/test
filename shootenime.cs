using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб снаряда
    public Transform player; // Игрок
    public float shootingInterval = 2f; // Интервал между выстрелами
    private float timeSinceLastShot = 0f;
    public float bulletSpeed = 10f; // Скорость снаряда

    void Update()
    {
        // Вращаем врага в сторону игрока
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // Плавный поворот

        timeSinceLastShot += Time.deltaTime;

        // Если настал момент для выстрела
        if (timeSinceLastShot >= shootingInterval)
        {
            ShootAtPlayer();
            timeSinceLastShot = 0f;
        }
    }

    void ShootAtPlayer()
    {
        // Рассчитываем направление от врага к игроку
        Vector3 direction = (player.position - transform.position).normalized;

        // Создаем снаряд
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);

        // Ориентируем пулю правильно в нужном направлении
        bullet.transform.rotation = Quaternion.LookRotation(direction);

        // Если ось Y указывает вверх, а ось Z должна быть вперед, поворачиваем пулю
        bullet.transform.Rotate(90, 0, 0); // Поворачиваем пулю так, чтобы ось Z была вперед

        // Добавляем скорость снаряду
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}

using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб снаряда
    public Transform player; // Игрок
    public float shootingInterval = 2f; // Интервал между выстрелами
    private float timeSinceLastShot = 0f;
    public float bulletSpeed = 10f; // Скорость снаряда
    public float shootingDistance = 15f; // Максимальная дистанция для стрельбы

 using UnityEngine;
using UnityEngine.SceneManagement; // Для перехода между сценами
using UnityEngine.UI; // Для отображения UI

public class LevelManager : MonoBehaviour
{
    public int requiredRings = 10; // Количество колец, необходимых для завершения уровня
    private int collectedRings = 0; // Сколько колец уже собрано
    public Text ringCounterText; // Ссылка на текст для отображения количества колец

    public string nextSceneName; // Имя следующей сцены для перехода

    void Start()
    {
        UpdateRingCounter(); // Обновляем текстовое отображение
    }

    public void CollectRing()
    {
        collectedRings++; // Увеличиваем количество собранных колец
        UpdateRingCounter(); // Обновляем текстовое отображение

        if (collectedRings >= requiredRings)
        {
            CompleteLevel(); // Завершаем уровень
        }
    }

    private void UpdateRingCounter()
    {
        // Обновляем текстовое отображение количества колец
        if (ringCounterText != null)
        {
            ringCounterText.text = $"Rings: {collectedRings}/{requiredRings}";
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Уровень завершён!");
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // Переходим на следующую сцену
        }
        else
        {
            Debug.LogWarning("Следующая сцена не указана!");
        }
    }
}






--Ссылки на элементы
local screenGui = script.Parent
local shopFrame = screenGui:WaitForChild("Frame")-- Фрейм магазина
local openButton = screenGui:WaitForChild("OpenButton")-- Кнопка открытия
local closeButton = shopFrame:WaitForChild("CloseButton")-- Кнопка закрытия

-- Открытие магазина
openButton.MouseButton1Click:Connect(function()
    shopFrame.Visible = true-- Показываем магазин
end)

-- Закрытие магазина
closeButton.MouseButton1Click:Connect(function()
    shopFrame.Visible = false-- Прячем магазин
end)







// Ника



    using UnityEngine;

public class Shoot : MonoBehaviour
{
    public ParticleSystem particle; // Система частиц для вспышки
    public AudioSource shootSource; // Звук выстрела

    private bool isShooting = false; // Флаг для отслеживания состояния стрельбы
    private int maxBullets = 20; // Максимальное количество пуль
    private int currentBullets = 20; // Текущее количество пуль
    private float reloadTime = 3f; // Время перезарядки (в секундах)

    void Update()
    {
        // Проверяем нажатие кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBullets > 0 && !isShooting)
            {
                // Если есть патроны и не стреляем, производим выстрел
                StartCoroutine(ShootDelay());
            }
            else if (currentBullets <= 0 && !isShooting)
            {
                // Если патроны закончились, начинаем перезарядку
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator ShootDelay()
    {
        isShooting = true; // Устанавливаем флаг стрельбы
        currentBullets--; // Уменьшаем количество пуль

        // Проигрываем эффекты
        particle.Play();
        shootSource.Play();

        yield return new WaitForSeconds(0.5f); // Задержка между выстрелами

        isShooting = false; // Сбрасываем флаг стрельбы
    }

    IEnumerator Reload()
    {
        isShooting = true; // Устанавливаем флаг стрельбы (блокируем новые выстрелы)
        Debug.Log("Перезарядка...");

        // Отображаем сообщение или анимацию перезарядки (опционально)
        yield return new WaitForSeconds(reloadTime); // Ожидание времени перезарядки

        currentBullets = maxBullets; // Восстанавливаем патроны
        isShooting = false; // Разрешаем стрелять снова

        Debug.Log("Перезарядка завершена! Количество пуль: " + currentBullets);
    }
}