using System;

namespace Final_Project
{
    internal class Weapon
    {
        public string WeaponName { get; set; }
        private double ability1Dmg;
        private double ability2Dmg;
        private double ability3Dmg;
        public int Ability1ManaCost { get; set; }
        public int Ability2ManaCost { get; set; }
        public int Ability3ManaCost { get; set; }
        public double Ability1Dmg
        {
            get => ability1Dmg;
            set
            {
                ability1Dmg = value;
                Ability1 = $"{Ability1Name} ({value} dmg)";
            }
        }

        public double Ability2Dmg
        {
            get => ability2Dmg;
            set
            {
                ability2Dmg = value;
                Ability2 = $"{Ability2Name} ({value} dmg)";
            }
        }

        public double Ability3Dmg
        {
            get => ability3Dmg;
            set
            {
                ability3Dmg = value;
                Ability3 = $"{Ability3Name} ({value} dmg)";
            }
        }

        public string Ability1Name { get; set; }
        public string Ability2Name { get; set; }
        public string Ability3Name { get; set; }

        public string Ability1 { get; private set; }
        public string Ability2 { get; private set; }
        public string Ability3 { get; private set; }

        public Weapon(string weaponName, string ability1Name, double ability1Dmg, int ability1ManaCost,
                     string ability2Name, double ability2Dmg, int ability2ManaCost,
                     string ability3Name, double ability3Dmg, int ability3ManaCost)
        {
            WeaponName = weaponName;
            Ability1Name = ability1Name;
            Ability2Name = ability2Name;
            Ability3Name = ability3Name;
            Ability1Dmg = ability1Dmg;
            Ability1ManaCost = ability1ManaCost;
            Ability2Dmg = ability2Dmg;
            Ability2ManaCost = ability2ManaCost;
            Ability3Dmg = ability3Dmg;
            Ability3ManaCost = ability3ManaCost;
        }
    }
}