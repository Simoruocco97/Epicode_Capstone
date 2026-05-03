using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerSlashPool slashPool;
    [SerializeField] private PlayerAnimationHandler animationHandler;

    [Header("Attack Infos")]
    [SerializeField] private int baseDamage = 30;
    [SerializeField] private float slashOffset = 1f;
    [SerializeField] private float slashOffsetY = 0.2f;
    [SerializeField] private float attackCooldown = 0.5f;
    private int damage;
    private float lastAttackTime;
    private float lastDir = 1f;

    private void Awake()
    {
        if (slashPool == null)
            slashPool = FindAnyObjectByType<PlayerSlashPool>();

        if (animationHandler == null)
            animationHandler = GetComponent<PlayerAnimationHandler>();

        damage = baseDamage;
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        if (moveX > 0)
            lastDir = 1f;
        else if (moveX < 0)
            lastDir = -1f;

        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Vector3 offSet = new(lastDir * slashOffset, slashOffsetY, 0f);
            Vector3 spawnPos = transform.position + offSet;

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXSound("PlayerAttack");

            slashPool.SpawnSlash(spawnPos, damage, lastDir);
        }
    }

    public void AddDamage(int amount) => damage += amount;

    public void ResetDamage() => damage = baseDamage;
}