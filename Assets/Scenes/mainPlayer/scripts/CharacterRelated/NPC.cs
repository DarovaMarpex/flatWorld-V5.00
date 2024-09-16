using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the this function returns nothing and takes a health.... need to create a publc events
public delegate void HealthChanged(float health);

public delegate void CharacterRemove(NPC target);
public class NPC : Character
{
    [SerializeField]
    private Sprite portrait;

    public event HealthChanged healthChanged;

    public event CharacterRemove characterRemove;

    public Sprite MyPortrait
    {
        get
        {
            return portrait;
        }
    }
    public virtual void DeSelect()
    {
        healthChanged -= new HealthChanged(UiManager.MyInstance.UpdateTargetFrame);
        characterRemove -= new CharacterRemove(UiManager.MyInstance.HideTargetFrame);
    }
    public virtual Transform Select()
    {
        return hitBox;
    }
   
    //needs to triger the event
    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }

    public void OnCharacterRemove(NPC target)
    {
        if (characterRemove != null)
        {
            characterRemove(target);
        }

        Destroy(gameObject);
    }
}
