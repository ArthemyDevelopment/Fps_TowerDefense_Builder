
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    public List<Transform> ShootingPoints;
    private int actShotingPoint;

    public int MaxHealth;
    public int _currHealth;

    public int CurrHealth
    {
        get => _currHealth;
        set
        {
            if (value <= 0)
                Death();
            else
            {
                _currHealth = value;
                UpdateHealthBar();
            }
            
        }

    }
   

    public Image HealthBar;

    private void Start()
    {
        CurrHealth = MaxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        HealthBar.fillAmount = ScriptsTools.MapValues(CurrHealth, 0, MaxHealth, 0, 1);
    }
    
    private void Death()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Screen.lockCursor= true;
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        GameObject temp = ObjectsPool.current.GetPlayerBullet();
        temp.transform.forward = ray.direction;
        SetBulletPosition(temp);
        temp.SetActive(true);


    }

    void SetBulletPosition(GameObject bullet)
    {
        if (actShotingPoint >= ShootingPoints.Count)
            actShotingPoint = 0;
        
        bullet.transform.position = ShootingPoints[actShotingPoint].position;
        actShotingPoint++;
    }
    
    
}
