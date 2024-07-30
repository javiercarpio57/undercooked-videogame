using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public MovableAnchor movableAnchor;
    MovableObject movableObject;
    public Queue<MovableObject> queue;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<MovableObject>();
        movableObject = movableAnchor.GetComponentInChildren<MovableObject>();
    }

    public bool hasMovable()
    {
        return movableObject != null;
    }
    
    public MovableObject GetMovableObjet()
    {
        return movableObject;
    }

    public void removeMovable()
    {
        movableObject = null;
        if(queue.Count > 0)
        {
            movableObject = queue.Dequeue();
            setMovable(movableObject);
        }
    }

    public void setMovable(MovableObject move)
    {
        movableObject = move;
        move.gameObject.transform.SetParent(movableAnchor.transform);
        move.transform.localPosition = new Vector3(0, 0, 0);
    }
}
