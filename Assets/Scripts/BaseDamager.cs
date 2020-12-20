using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BaseDamager : MonoBehaviour
{
    [SerializeField]
    private BaseTile baseTile;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        var otherUnit = contact.otherCollider.gameObject.GetComponentInChildren<Unit>();

        // Ignore collisions with anything but enemy units
        if (otherUnit == null || !otherUnit.alignment.CanHarm(baseTile.alignment))
        {
            return;
        }

        baseTile.TakeDamage(otherUnit.GetBaseDamage());
        Destroy(contact.otherCollider.gameObject);
    }
}
