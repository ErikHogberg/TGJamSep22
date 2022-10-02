using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    private static SpellScript mainInstance = null;

    public Animator SpellObj;
    public float Range;
    public float Damage;
    public LayerMask HitLayer;

    void Awake()
    {
        mainInstance = this;
    }
    void OnDestroy()
    {
        mainInstance = null;
    }

    public float Cooldown;
    float timer = -1;

    readonly RaycastHit2D[] hits = new RaycastHit2D[10];

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                SpellObj.gameObject.SetActive(false);
                int hitCount = Physics2D.CircleCastNonAlloc(transform.position, Range, Vector2.zero, hits);
                for (int i = 0; i < hitCount; i++)
                {
                    if (hits[i].collider.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.currentHp -= Damage;
                    }
                }
            }
        }
    }

    public static void Cast(Vector2 Pos)
    {
        if (!mainInstance) return;

        mainInstance.SpellObj.gameObject.SetActive(true);
        mainInstance.SpellObj.transform.position = Pos;
    }

}
