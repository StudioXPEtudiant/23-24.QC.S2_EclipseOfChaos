using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int emptyHandAttackForce = 1;
    [SerializeField] private int emptyHandHarvestLevel = 1;

    [SerializeField] private int size = 4;
    private int _currentSLot = 0;
    private GameObject[] _slots;

    public static InventorySystem Instance { get; private set;}

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _slots = new GameObject[size];
    }

    public bool TrySetItemInEmptySlot(GameObject item)
    {
        if(_slots[_currentSLot])
            return false;
        _slots[_currentSLot] = item;
        return true;
    }
       
    public void RemoveCurrentSlot()
    {
        if(!_slots[_currentSLot])
            return;
        GameObject item = _slots[_currentSLot];
        _slots[_currentSLot] = null;
    }

    public GameObject GetCurrentItem()
    {
        return _slots[_currentSLot];
    }

    public void Select(int slot)
    {
        if(slot == _currentSLot)
        {
            return;
        }

        GameObject currentInteractable = GetCurrentItem();
        if(currentInteractable)
            currentInteractable.SetActive(false);
        
        _currentSLot = slot;

        currentInteractable = GetCurrentItem();
        if(currentInteractable)
            currentInteractable.SetActive(true);
        
        Debug.Log("Current slot = " + _currentSLot);
        
    }

    public void NextSlot()
    {
        if(_currentSLot < size)
        {
            Select(_currentSLot + 1);
        }
    }

    public void PrivousSlot()
    {
        if(_currentSLot > 0)
        {
            Select(_currentSLot - 1);
        }
    }

    public int GetPlayerAttackForce()
    {
        var _pickableInHand = GetComponentInChildren<PickebleFunction>();

        if(!_pickableInHand)
        {
            return emptyHandAttackForce;
        }
        return _pickableInHand.AttackForce;
    }

    public int GetPlayerHarvestLevel()
    {
        var _pickableInHand = GetComponentInChildren<PickebleFunction>();

        if(!_pickableInHand)
        {
            return emptyHandHarvestLevel;
        }
        return _pickableInHand.HarvestLevel;
    }

    public string[] GetPlayerHarvestCategories()
    {
        var _pickableInHand = GetComponentInChildren<PickebleFunction>();

        if(!_pickableInHand)
        {
            return null;
        }
        return  _pickableInHand.HarvestCategories;
    }
}
