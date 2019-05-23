using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOverlap : MonoBehaviour
{
    public int priority = 0;
    public List<Collider2D> colliders = new List<Collider2D>();
    [SerializeField]public List<Collider2D> GetColliders() { return colliders; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12) Destroy(this.gameObject);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
        if (other.gameObject.tag == "Coin" || other.gameObject.tag == "Opal")
        {
            if (other.GetComponent<ObjectOverlap>().priority < this.priority) other.gameObject.SetActive(false);
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other);
    }
}
