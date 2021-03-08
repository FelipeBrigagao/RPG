using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    //Objeto Stat criado para esses valores porque eles terão modificarores aplicados à eles
    public Stat damage;
    public Stat armor;
    public Stat attackSpeed;

    private void Awake()
    {
        currentHealth = maxHealth;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }


    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " taking " + damage + " damage");

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        //Morre de maneiras diferentes dependendo do character
    }

}
