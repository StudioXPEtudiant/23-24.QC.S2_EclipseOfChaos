using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickebleFunction : MonoBehaviour
{
    [SerializeField] private Vector3 pickupPosition;
    [SerializeField] private Quaternion pickupRotation;
    [SerializeField] private UnityEvent onPickedUp;

    private InventorySystem _inventory;
    private Rigidbody _rigidbody;

    [SerializeField] private int attackForce = 3;
    public int AttackForce => attackForce;

    [SerializeField] private string[] harvestCategories;
    public string[] HarvestCategories => harvestCategories;

    [SerializeField] private int harvestLevel =3;
    public int HarvestLevel => harvestLevel;

    void Start()
    {
        _inventory = InventorySystem.Instance;
        _rigidbody = GetComponent<Rigidbody>();
    }

    [ContextMenu("Ramasser")]
    public void Pickup()
    {
        if(!_inventory.TrySetItemInEmptySlot(gameObject))
            return;

        onPickedUp.Invoke();

        transform.parent = _inventory.GetComponent<Transform>();
        transform.localRotation = pickupRotation;
        transform.localPosition = pickupPosition;
        transform.localScale = Vector3.one;

        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;

        foreach (var col in GetComponents<Collider>())
        {
            col.enabled = false;
        }
    }

    public void Drop()
    {
        _inventory.RemoveCurrentSlot();

        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;

        foreach (var col in GetComponents<Collider>())
        {
            col.enabled = true;
        }
        transform.parent = null;
    }

}
