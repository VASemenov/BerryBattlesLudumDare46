using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponObject weapon;
    private SpriteRenderer _spriteRenderer;
    public GameObject parent;
    public Vector3 standardPosition;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        parent = GetComponentInParent<SpriteRenderer>().gameObject;
        _spriteRenderer.sprite = weapon.weaponSprite;
    }

    public void SetWeapon(WeaponObject weaponObject)
    {
        weapon = weaponObject;
        _spriteRenderer.sprite = weapon.weaponSprite;
        
        gameObject.transform.position = new Vector2(
            parent.transform.position.x + 0.2f, 
            parent.transform.position.y + 0.5f);
        
    }
    
    
}
