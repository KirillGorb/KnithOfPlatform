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

    // Update is called once per frame
    void Update()
    {
        // Health regeneration
        if (health < maxHealth)
        {
            health += healthRegenRate * Time.deltaTime;
            health = Mathf.Clamp(health, 0f, maxHealth);
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;

        // Check if level up
        if (experience >= experienceNeeded)
        {
            level++;
            experience -= experienceNeeded;

            // Increase health and add new abilities
            maxHealth += 10f;
            health = maxHealth;
            AddAbilities();

            // Increase experience needed for next level
            experienceNeeded = Mathf.FloorToInt(experienceNeeded * 1.1f);
            experienceNeeded = Mathf.Clamp(experienceNeeded, 0, int.MaxValue);

            // Check if max level reached
            if (level > maxLevel)
            {
                level = maxLevel;
                experience = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Reduce health when touching an enemy
            health -= 10f;

            if (health <= 0f)
            {
                // Reset level and experience when character dies
                level = 1;
                experience = 0;
                health = maxHealth;
            }
        }
    }

    private void AddAbilities()
    {
        // Add new abilities for each level
        switch (level)
        {
            case 2:
                // Add new ability for level 2
                break;
            case 3:
                // Add new ability for level 3
                break;
                // Add more cases for additional levels and abilities
        }
    }
}
