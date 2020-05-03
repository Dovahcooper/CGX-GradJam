using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Elements bossType;

    public int health = 1000;

    public float dissolveTime = 0f;

    float dissolveSpeed = 0.2f;
    
    [Range(-10f, 10f)]
    public float intensity = 4f;

    MeshRenderer rend;

    bool dying;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<MeshRenderer>();

        float factor = Mathf.Pow(2f, intensity);

        rend.material.SetColor("DissolveColour", Elemental.colours[(int)bossType] * factor);
        rend.material.SetFloat("DissolveState", dissolveTime);
    }

    private void OnEnable()
    {
        dissolveTime = 0f;

        float factor = Mathf.Pow(2, intensity);

        rend.material.SetColor("DissolveColour", Elemental.colours[(int)bossType] * factor);
        rend.material.SetFloat("DissolveState", dissolveTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dissolveTime < 1f && !dying)
            dissolveTime += Time.fixedDeltaTime * dissolveSpeed;

        if(health <= 0)
        {
            BeginDeath();
        }

        if (dying)
        {
            dissolveTime -= Time.fixedDeltaTime * dissolveSpeed;
        }

        rend.material.SetFloat("DissolveState", dissolveTime);
    }

    void BeginDeath()
    {
        dying = true;
        dissolveSpeed *= 2f;
    }

    public void Damage(Elements incoming)
    {
        int dmg;

        switch (incoming)
        {
            case Elements.Fire:
                switch (bossType)
                {
                    case Elements.Water:
                    case Elements.Fire:
                        dmg = 1;
                        break;
                    case Elements.Wind:
                        dmg = 4;
                        break;
                    case Elements.Lightning:
                    default:
                        dmg = 2;
                        break;
                }
                break;
            case Elements.Water:
                switch (bossType)
                {
                    case Elements.Water:
                    case Elements.Lightning:
                        dmg = 1;
                        break;
                    case Elements.Fire:
                        dmg = 4;
                        break;
                    case Elements.Wind:
                    default:
                        dmg = 2;
                        break;
                }
                break;
            case Elements.Lightning:
                switch (bossType)
                {
                    case Elements.Lightning:
                    case Elements.Wind:
                        dmg = 1;
                        break;
                    case Elements.Water:
                        dmg = 4;
                        break;
                    case Elements.Fire:
                    default:
                        dmg = 2;
                        break;
                }
                break;
            case Elements.Wind:
                switch (bossType)
                {
                    case Elements.Wind:
                    case Elements.Fire:
                        dmg = 1;
                        break;
                    case Elements.Lightning:
                        dmg = 4;
                        break;
                    case Elements.Water:
                    default:
                        dmg = 2;
                        break;
                }
                break;
            default:
                dmg = 2;
                break;
        }

        if (dissolveTime >= 1f)
        {
            health -= dmg;
        }
        else
        {
            //do nothing
            //I just enjoy putting else statements
            //blame my high school teachers
        }
    }
}
