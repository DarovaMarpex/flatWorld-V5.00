using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
   public class Spell : IUseable
    {
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPreFab;

    [SerializeField]
    private Color barColor;

    public string MyName
    {
        get
        {
            return name;
        }
    }
        public int MyDamage
    {
        get
        {
            return damage;
        }
    }
    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }
    public float MySpeed
    {
        get
        {
            return speed;
        }
    }
    public float MyCastTime
    {
        get
        {
            return castTime;
        }
    }
    public GameObject MySpellPrefab
    {
        get
        {
            return spellPreFab;
        }
    }
    public Color MyBarColor
    {
        get
        {
            return barColor;
        }
    }

    public void Use()
    {
        Player.MyInstance.castSpell(MyName);
    }
}

