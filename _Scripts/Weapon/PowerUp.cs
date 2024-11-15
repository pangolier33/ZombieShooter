using System;
using System.Collections;
using _Scripts.Weapon;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _delayMultiplier = 0.5f;
    [SerializeField] private float _duration = 10f;
    [SerializeField] private float _cooldown = 15f;

    [SerializeField] private Transform[] _spawnPoints;

    public float DelayMultiplier => _delayMultiplier;

    private void OnTriggerEnter(Collider other)
    {
        var playerWeapon = other.GetComponent<PlayerWeapon>();
        if (playerWeapon)
        {
            playerWeapon.AddPowerUp(this);
            StartCoroutine(DisableAfterDelay(playerWeapon));
            SpawnPowerUp(false);
        }
    }

    private IEnumerator DisableAfterDelay(PlayerWeapon playerWeapon)
    {
        yield return new WaitForSeconds(_duration);
        playerWeapon.RemovePowerUp(this);
        yield return new WaitForSeconds(_cooldown);

        int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        transform.position = _spawnPoints[randomIndex].position;
        SpawnPowerUp(true);
    }

    private void SpawnPowerUp(bool available)
    {
        GetComponent<Collider>().enabled = available;
        GetComponent<Renderer>().enabled = available;
    }
}
