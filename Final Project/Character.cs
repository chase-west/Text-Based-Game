using System;

namespace Final_Project
{
    internal class Character
    {
        // Private fields
        private string characterName;
        private string userName;
        private double health;
        public double originalHealth;
        public int originalMana;

        // Public properties with getters and setters
        public string CharacterName
        {
            get { return characterName; }
            set { characterName = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public double Health
        {
            get { return health; }
            set { health = value; }
        }
        public int Mana { get; set; }

        // Default constructor
        public Character()
        {
            // Initialize default values if needed
            characterName = "";
            userName = "";
            health = 0;
            Mana = 0;
            originalHealth = 0;
            originalMana = 0;
        }


        public Character(string characterName, string userName, double health, int mana)
        {
            this.characterName = characterName;
            this.userName = userName;
            this.health = health;
            this.Mana = mana;
            this.originalHealth = health;
            this.originalMana = mana;
        }

        // Method to reset the character's health and mana to their original values
        public void ResetHealthAndMana()
        {
            Health = originalHealth;
            Mana = originalMana;
        }
    }
}