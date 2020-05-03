using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField]
    public UnityEvent spawnBehaviour;

    public Elements bossType;

    public int maxHealth = 750;
    int health;

    public int killCount = 0;

    public float dissolveTime = 0f;

    float dissolveSpeed = 0.2f;

    public Text id;
    public RawImage healthBar;
    
    [Range(-10f, 10f)]
    public float intensity = 4f;

    MeshRenderer rend;

    public bool dying = false;

    float startScaleBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        rend = GetComponentInChildren<MeshRenderer>();

        float factor = Mathf.Pow(2f, intensity);

        Color newCol = Elemental.colours[(int)bossType];

        rend.material.SetColor("DissolveColour", newCol * factor);
        rend.material.SetFloat("DissolveState", dissolveTime);

        id.color = newCol;
        healthBar.color = newCol;

        startScaleBar = healthBar.rectTransform.localScale.x;
    }

    private void re_enable()
    {
        killCount++;
        if (killCount >= 4)
        {
        
        }
        else
        {
            int newType = (int)bossType;

            spawnBehaviour.RemoveAllListeners();

            bossType = (Elements)(++newType % 4);

            dissolveSpeed /= 1.5f;
            dissolveTime = 0f;
            
            health = maxHealth;
            dying = false;

            float factor = Mathf.Pow(2, intensity);

            Color newCol = Elemental.colours[(int)bossType];

            rend.material.SetColor("DissolveColour", newCol * factor);
            rend.material.SetFloat("DissolveState", dissolveTime);

            id.color = newCol;
            healthBar.color = newCol;

            switch (bossType)
            {
                case Elements.Fire:
                    spawnBehaviour.AddListener(FlameSpawn);
                    break;
                case Elements.Water:
                    spawnBehaviour.AddListener(WaterRise);
                    break;
                case Elements.Lightning:
                    spawnBehaviour.AddListener(LightningStrikes);
                    break;
                case Elements.Wind:
                    spawnBehaviour.AddListener(TornadoSpawn)
                    break;
                default:
                    break;
            }
        }
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

        if (dying && dissolveTime > 0f)
        {
            dissolveTime -= Time.fixedDeltaTime * dissolveSpeed;
        }
        else if(dying && dissolveTime <= 0f)
        {
            re_enable();
        }
        else
        {
            //do nothing?
        }

        rend.material.SetFloat("DissolveState", dissolveTime);

        healthBar.transform.localScale = new Vector3(Mathf.Lerp(0f, startScaleBar, (float)health / maxHealth), healthBar.transform.localScale.y, 0f);
    }

    void BeginDeath()
    {
        if (!dying)
        {
            dying = true;
            dissolveSpeed *= 1.5f;
        }
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

    void FlameSpawn()
    {

    }
}
