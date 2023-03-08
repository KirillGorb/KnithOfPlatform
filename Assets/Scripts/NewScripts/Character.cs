public class Character : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int experienceNeeded = 100;
    public int maxLevel = 99;
    public int maxEnemies = 100;
    public float health = 100f;
    public float maxHealth = 100f;
    public float healthRegenRate = 0.2f;

    void Update()
    {
      if (health < maxHealth)
        {
            health += healthRegenRate * Time.deltaTime;
            health = Mathf.Clamp(health, 0f, maxHealth);
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;

        if (experience >= experienceNeeded)
        {
            level++;
            experience -= experienceNeeded;

            maxHealth += 10f;
            health = maxHealth;
            AddAbilities();

            experienceNeeded = Mathf.FloorToInt(experienceNeeded * 1.1f);
            experienceNeeded = Mathf.Clamp(experienceNeeded, 0, int.MaxValue);

            if (level > maxLevel)
            {
                level = maxLevel;
                experience = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (other.TryGetComponent(out EnemyMovement enemy))
        {
            health -= 10f;

            if (health <= 0f)
            {
                level = 1;
                experience = 0;
                health = maxHealth;
            }
        }
    }

    private void AddAbilities()
    {
        switch (level)
        {
            case 2:
                break;
            case 3:
                break;
               }
    }
}
