using UnityEngine;

public class InteractableObject : Player,Iinteractable
{
    

    [SerializeField] public GameObject thisObject;
    public bool activateInteraction=false;
    public float distance;

 

    public void InteractionActivated() //need to add buffer in player controls
    {
        activateInteraction= true;
        
    }

    public void CalcDistance()
    {
        
        distance=Vector2.Distance(player.transform.position,thisObject.transform.position);
        if (distance <= 2.5f)
        {
            InteractionActivated();
        }
        else
        {
            activateInteraction= false;
        }
    }
}
