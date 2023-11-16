using UnityEngine;

public class ShootProjectile : Ability
{
    [SerializeField] private Projectile prefab;
    [SerializeField] private float damage;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float projectileSpeed;

    private ICaster _caster;
    private void Start()
    {
        _caster = GetComponentInParent<ICaster>();
    }
    public override void CastAbility(Transform target)
    {
        // Calculate rotation needed to hit target
        float dy = target.position.y - transform.position.y, dx = target.position.x - transform.position.x;
        var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        var rot = Quaternion.Euler(new Vector3(0, 0, angle));

        // Instantiate projectile and fire
        var proj = Instantiate(prefab, transform.position, rot);
        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * projectileSpeed;

        Destroy(proj.gameObject, projectileLifetime);
        StartCooldown();
    }
}
