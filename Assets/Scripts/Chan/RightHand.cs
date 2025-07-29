using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Timeline;

public class RightHand : MonoBehaviour
{
    public float Detec = 0.3f;
    private SpriteRenderer spriter;
    public GameObject equipPoint;
    bool isPicking = false;

    private PlayerMouse parentMouse;
    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        parentMouse = transform.parent.GetComponent<PlayerMouse>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isReverse = PlayerManager.Instance.isLeft;
        spriter.sortingOrder = isReverse ? 4 : 7;
        spriter.flipX = isReverse;

        if (!PlayerManager.Instance.isLeftClick && isPicking)
            Drop();
    }

    public void Pickup(GameObject item)
    {
        item.transform.SetParent(equipPoint.transform);
        item.transform.localPosition = Vector2.zero;
        item.transform.rotation = new Quaternion(0, 0, 0, 0);

        transform.GetChild(1).gameObject.SetActive(false);

        SetEquip(item, true);
        isPicking = true;
    }
    void Drop()
    {
        Rigidbody2D itemRigid = equipPoint.GetComponentInChildren<Rigidbody2D>();
        SetEquip(itemRigid.gameObject, false);

        equipPoint.transform.DetachChildren();

        itemRigid.gravityScale = 0.5f;
        Vector2 throwVector = new Vector2(parentMouse.PreVector.normalized.x, parentMouse.PreVector.normalized.y);
        itemRigid.AddForce(throwVector * 10f, ForceMode2D.Impulse); 

        transform.GetChild(1).gameObject.SetActive(true);

        isPicking = false;
    }
    void SetEquip(GameObject item, bool isEquip)
    {
        Collider2D itemCollider = item.GetComponent<Collider2D>();
        Rigidbody2D itemRigid = item.GetComponent<Rigidbody2D>();

        itemCollider.enabled = !isEquip;

        itemRigid.isKinematic = isEquip;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isPicking && PlayerManager.Instance.isLeftClick)
        {
            if (other.tag == "ThrowingObject")
            {
                Pickup(other.gameObject);
            }   
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        //줍기 아이템 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, Detec);
    }

}
