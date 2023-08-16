using System;

namespace Final_Project
{
    internal class Program
    {
        //Create class level character selected variable for game reset
        private static bool characterSelected = false;
        static void Main(string[] args)
        {
            //Control start and end of game variables
            int choice = 0;
            Character character = null;


            //Create a monster object
            Monster monster1 = new Monster(60.8);

            do
            {
                //Display start menu and assign choice to variable
                if (choice != 2 || choice != 1)
                {
                    choice = startMenu();
                }

                switch (choice)
                {
                    case 1:
                        //Select character
                        Console.WriteLine("Choose a following character:");
                        Console.WriteLine("1) Wizard, 80 health, 92 mana");
                        Console.WriteLine("2) Goblin, 100 health, 80 mana");
                        Console.WriteLine("3) Swordman, 110 health, 75 mana");
                        int characterChoice = int.Parse(Console.ReadLine());

                        character = selectCharacter(characterChoice);

                        //Get username for character
                        Console.Write("Enter a name for your character: ");
                        character.UserName = Console.ReadLine();

                        //Show user what character they slected
                        Console.WriteLine($"Selected character: {character.CharacterName}, {character.UserName}");
                        characterSelected = true;
                        break;
                    case 2:

                        //Battle

                        //Verify's user selects a character before beginning game 
                        if (characterSelected)
                        {
                            //Checks if character completes Tavern Interaction before starting cave and weapon sequence
                            if (TavernQuestInteraction() == true)
                            {
                                //Gives user weapon choice
                                Weapon charcterWeapon = selectWeapon();

                                //Starts cave quest
                                CaveQuest(character, charcterWeapon, monster1);
                            }
                        }
                        else
                        {
                            //Forces user to select character before starting gameplay
                            Console.WriteLine("Please select a character before starting the game");
                        }
                        break;
                    case 3:
                        //Exit program
                        Console.WriteLine("Goodbye!");
                        break;
                }
                //Keeps menu running while user doesn't exit    
            } while (choice != 3);
        }

        //Method to display start menu
        private static int startMenu()
        {
            //Display start menu

            //Reset game over variable
            gameOver = false;

            // Reset cave exploration flags when starting a new game
            firstEnd = false;
            secondEnd = false;

            Console.WriteLine("Welcome to Cave Quest! \n");
            Console.WriteLine("1) Select character");
            Console.WriteLine("2) Start Game");
            Console.WriteLine("3) Exit");
            int choice = int.Parse(Console.ReadLine());
            return choice;
        }

        //Method to select character
        private static Character selectCharacter(int characterChoice)
        {
            // Initialize Character objects
            Wizard wizard = new Wizard("Wizard", " ", 80, 92);
            Goblin goblin = new Goblin("Goblin", " ", 100, 80);
            SwordMan swordMan = new SwordMan("SwordMan", " ", 110, 75);

            //Based on user input select character
            switch (characterChoice)
            {
                case 1:
                    return wizard;
                case 2:
                    return goblin;
                case 3:
                    return swordMan;
                default:
                    return null;
            }
        }

        //Method to initalize weapons
        private static Weapon[] InitializeWeapons()
        {
            //Initialize array of weapon objects
            Weapon[] weapons = new Weapon[]
            {
               new Weapon("Staff", "Radiant Burst, a channel of celestial energy", 15.4, 14, "Starlight Projection, project ethereal starlight orbs around enemy", 7.5, 13, "Astral blast, Blasts enemy with a beam of light", 24, 30),
               new Weapon("Dagger", "Shadowstep, teleport in front of the enemy and strike them with a deadly hit", 12.6, 8, "Crippling Venom, throw dagger tainted with a deadly poison at enemy", 5.8, 11, "Cloak of Shadows, envelope enemy in a dark shadow", 19, 23),
               new Weapon("Sword", "Arcane Slash, channel arcane energy into the tip of your blade and hit enemy", 15.9, 10, "Elemental Enchant, empowers sword with a dangerous element", 5, 8, "Eternal Ward, creates a dangerous wall that attacks opponent", 20, 26)

            };
            return weapons;
        }

        //Method that allows user to select a weapon 
        private static Weapon selectWeapon()
        {
            //Create array of weapons
            Weapon[] weapons = InitializeWeapons();

            //Offer user weapon choices
            Console.WriteLine("The man offers you three weapons and asks you to choose one.");
            Console.WriteLine($"1) Staff, (Average damage: {(weapons[0].Ability1Dmg + weapons[0].Ability2Dmg + weapons[0].Ability3Dmg) / 3}, Average mana use: {(weapons[0].Ability1ManaCost + weapons[0].Ability2ManaCost + weapons[0].Ability3ManaCost) / 3})");
            Console.WriteLine($"2) Dagger, (Average damage: {(weapons[1].Ability1Dmg + weapons[1].Ability2Dmg + weapons[1].Ability3Dmg) / 3}, Average mana use: {(weapons[1].Ability1ManaCost + weapons[1].Ability2ManaCost + weapons[1].Ability3ManaCost) / 3})");
            Console.WriteLine($"3) Sword, (Average damage: {(weapons[2].Ability1Dmg + weapons[2].Ability2Dmg + weapons[2].Ability3Dmg) / 3}, Average mana use: {(weapons[2].Ability1ManaCost + weapons[2].Ability2ManaCost + weapons[2].Ability3ManaCost) / 3})");

            //Store weapon choice
            int weaponChoice = int.Parse(Console.ReadLine());

            //Verify weapon is in range of allowed ones and assign weapon to choice
            if (weaponChoice >= 1 && weaponChoice <= weapons.Length)
            {
                Console.WriteLine("You chose the weapon: " + weapons[weaponChoice - 1].WeaponName);
                return weapons[weaponChoice - 1];
            }
            else
            {
                //If character chose out of range
                return null;
            }
        }

        //Create a static instance of the Random class
        private static Random random = new Random();

        //Method to get a random number between min and max 
        private static double GetRandomNumber(double min, double max)
        {
            //Generate a random double
            double randomDouble = random.NextDouble();

            //Shift the random double to the desired range
            double result = min + (randomDouble * (max - min));

            return result;
        }

        //Create variable to show game is over 
        private static bool gameOver = false;

        //Method to reset game
        public static void resetGame(Character character)
        {
            //Reset game so you can play again
            enemiesKilled = 0;
            totalDamageDone = 0;
            damageTaken = 0;
            character.ResetHealthAndMana();
            firstEnd = false;
            secondEnd = false;
            characterSelected = false;
            gameOver = true;
            character.Mana = character.originalMana;
        }

        //Declare class level variables that can be updated throughout game for final screen stats
        private static int enemiesKilled = 0;
        private static double totalDamageDone = 0.0;
        private static double damageTaken = 0;

        //Display death screen when character dies
        private static void FinalScreen(int enemiesKilled, Weapon weapon, Character character)
        {
            Console.WriteLine($"Game over, {character.UserName}!\n");
            Console.WriteLine($"You chose the character, {character.CharacterName} and the weapon, {weapon.WeaponName}");

            // Update the 2D array to display the correct information
            double[,] battleData = new double[,]
            {
                { enemiesKilled, totalDamageDone, damageTaken }, // Change the positions to display correct information
            };

            // Display the data in tabular format
            Console.WriteLine("Enemies Killed | Damage You Caused | Damage You Took");
            for (int i = 0; i < battleData.GetLength(0); i++)
            {
                Console.WriteLine($"{battleData[i, 0],-14} | {battleData[i, 1],-17} | {battleData[i, 2],-15}");
            }

            //Reset game
            resetGame(character);
        }


        //Method for every fight in the game
        private static void AttackSequence(Weapon weapon, Character character, Monster monster)
        {
            // Create variables needed for keeping track of attack turn and starting mana
            bool monsterTurn = false;
            int startMana = character.Mana;


            // Continue fighting while both monster and character are alive
            while (monster.MonsterHealth > 0 && character.Health > 0 && hasMana)
            {

                //Check if it's the monster's turn to attack
                if (monsterTurn)
                {
                    //Monster attacks the character
                    double monsterDamage = GetRandomNumber(10, 17.6);
                    character.Health -= monsterDamage;
                    damageTaken += monsterDamage;
                    Console.WriteLine("The monster attacks you and deals " + monsterDamage + " damage.\n");
                    monsterTurn = false;
                }
                else
                {
                    //Give the user choice for abilities
                    Console.WriteLine("Your remaining mana: " + character.Mana);
                    Console.WriteLine("Which ability would you like to use?");
                    Console.WriteLine("1) " + weapon.Ability1 + " (Mana cost: " + weapon.Ability1ManaCost + ")");
                    Console.WriteLine("2) " + weapon.Ability2 + " (Mana cost: " + weapon.Ability2ManaCost + ")");
                    Console.WriteLine("3) " + weapon.Ability3 + " (Mana cost: " + weapon.Ability3ManaCost + ")");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            if (character.Mana >= weapon.Ability1ManaCost)
                            {
                                double ability1Damage = weapon.Ability1Dmg;
                                monster.MonsterHealth -= ability1Damage;
                                totalDamageDone += ability1Damage;
                                character.Mana -= weapon.Ability1ManaCost; //Deduct mana cost
                                Console.WriteLine("You attack the monster!\n");
                            }
                            else
                            {
                                Console.WriteLine("Not enough mana to use this ability. Choose another.");
                                continue;
                            }
                            break;
                        case 2:
                            if (character.Mana >= weapon.Ability2ManaCost)
                            {
                                double ability2Damage = weapon.Ability2Dmg;
                                monster.MonsterHealth -= ability2Damage;
                                totalDamageDone += ability2Damage;
                                character.Mana -= weapon.Ability2ManaCost; //Deduct mana cost
                                Console.WriteLine("You attack the monster!\n");
                            }
                            else
                            {
                                Console.WriteLine("Not enough mana to use this ability. Choose another.");
                                continue;
                            }
                            break;
                        case 3:
                            if (character.Mana >= weapon.Ability3ManaCost)
                            {
                                double ability3Damage = weapon.Ability3Dmg;
                                monster.MonsterHealth -= ability3Damage;
                                totalDamageDone += ability3Damage;
                                character.Mana -= weapon.Ability3ManaCost; //Deduct mana cost
                                Console.WriteLine("You attack the monster!\n");
                            }
                            else
                            {
                                Console.WriteLine("Not enough mana to use this ability. Choose another.");
                                continue;
                            }
                            break;



                        default:
                            Console.WriteLine("Invalid choice. Please choose again.");
                            continue;
                    }
                    monsterTurn = true;
                    //Check if character still has mana
                    if (character.Mana < weapon.Ability1ManaCost && character.Mana < weapon.Ability2ManaCost && character.Mana < weapon.Ability3ManaCost)
                    {
                        hasMana = false;
                    }
                }
                //Display user and monster health if they are alive
                if (monster.MonsterHealth > 0 && character.Health > 0)
                {
                    Console.WriteLine($"Your current health is: {character.Health}, and the monster's health is {monster.MonsterHealth}\n");
                }

                //If monster is killed update kill count
                if (monster.MonsterHealth <= 0)
                {
                    enemiesKilled++;
                }

                //Check if the player won or lost the battle
                if (monster.MonsterHealth <= 0 && character.Health > 0)
                {
                    //If won award mana
                    Console.WriteLine("You have defeated the monster!");
                    Console.WriteLine("+45 Mana!\n\n");
                    character.Mana += 45;
                }

                // Check if the character is defeated
                if (character.Health <= 0 && !hasMana && monster.MonsterHealth > 0)
                {
                    Console.WriteLine("You have been defeated by the monster...");
                    gameOver = true; // Set gameOver flag to true
                    FinalScreen(enemiesKilled, weapon, character);
                }

                // Check if the character ran out of mana
                if (!hasMana && character.Health > 0 && monster.MonsterHealth > 0)
                {
                    Console.WriteLine("You ran out of mana and were slain!");
                    gameOver = true; // Set gameOver flag to true
                    FinalScreen(enemiesKilled, weapon, character);
                }

            }
            
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Method that trigger main story 

        //Create story variables that trigger different events
        private static bool firstEnd = false;
        private static bool secondEnd = false;

        //Method to trigger start of game 
        private static bool TavernQuestInteraction()
        {
            //Story
            Console.WriteLine("You wake up from a deep slumber in your local tavern. You find yourself surrounded by people drinking and having a good time. A young but strong-looking man comes up to you.");
            Console.WriteLine("Young Man: Hey there, traveler! Are you looking for an adventure? I've heard rumors of a dangerous cave nearby filled with treasures. You seem capable; do you want to join me in the quest?\n");
            Console.WriteLine("1) Accept the quest.");
            Console.WriteLine("2) Decline the offer.");
            int response = int.Parse(Console.ReadLine());

            //Verify user gives proper response
            while (response != 1 && response != 2)
            {
                Console.WriteLine("Invalid choice. The young man gives you a puzzled look and waits for another response.");
                response = int.Parse(Console.ReadLine());
            }
            //If user declines quest force them to accept
            while (response != 1)
            {
                Console.WriteLine("I encourage you to do this quest it'll be fun!");
                Console.WriteLine("1) Accept the quest.");
                Console.WriteLine("2) Decline the offer.");
                response = int.Parse(Console.ReadLine());
            }

            bool startQuest = true;
            return startQuest;
        }

        //Create class level bool to end game if needed
        private static bool hasMana = true;

        //Method to start cave quest
        private static void CaveQuest(Character character, Weapon weapon, Monster monster)
        {
            //Create bool to return to menu
            bool returnToMenu = false;

            do
            {
                // Set hasMana to true at the beginning of each new game
                hasMana = true;

                //Explore cave
                Console.WriteLine("1) Explore");

                //Check character health
                Console.WriteLine("2) Check health");

                //Exit to main menu
                Console.WriteLine("3) Exit to main menu");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    //Explore cave deeper
                    case 1:
                        //Check if first part of cave is explored
                        if (firstEnd == false)
                        {
                            insideCave1(weapon, character);
                            firstEnd = true;
                        }
                        //Check if second part of cave is explored
                        else if (secondEnd == false)
                        {
                            insideCave2(weapon, character);
                            secondEnd = true;
                            returnToMenu = true;
                        }
                        break;
                    //Display character health
                    case 2:
                        Console.WriteLine("Your heath is: " + character.Health);
                        break;
                    //Return to menu
                    case 3:
                        Console.WriteLine("Returning to menu... \n");
                        resetGame(character);
                        returnToMenu = true;
                        break;
                }
            }


            //Verify character is alive
            while (!gameOver && !returnToMenu);
        }

        private static void insideCave1(Weapon weapon, Character character)
        {
            //Create monsters for level
            Monster zombie = new Monster(60.4);
            Monster skeleton = new Monster(60.4);

            //Story
            Console.WriteLine("You enter the cave and see a mineshaft, do you go down it?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    //Story
                    Console.WriteLine("As you start walking down the mineshaft you hear a growl behind you...");
                    Console.WriteLine("A zombie! the young man says");
                    AttackSequence(weapon, character, zombie);
                    break;
                case 2:
                    //Story
                    Console.WriteLine("As you turn to walk away from the mineshaft a skeleton jumps out to attack you!");
                    AttackSequence(weapon, character, skeleton);
                    break;
            }
            firstEnd = true;
        }
        private static void insideCave2(Weapon weapon, Character character)
        {
            do
            {
                //Create monsters for level
                Monster vampire = new Monster(50.9);
                Monster dragon = new Monster(80.2);
                Monster golem = new Monster(90.3);
                Monster fairy = new Monster(120);

                //Story
                Console.WriteLine("As you venture deeper into the cave, you notice an eerie glow emanating from the walls.");
                Console.WriteLine("You follow the mysterious glow, and it leads you to a hidden chamber filled with ancient runes.");
                Console.WriteLine("1) Investigate the runes closely");
                Console.WriteLine("2) Keep moving forward");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    //Story
                    Console.WriteLine("As you approach the runes, you feel a surge of energy coursing through your body.");
                    Console.WriteLine("The runes react to your presence, and suddenly, you find yourself imbued with magical powers!");

                    //Update the character's abilities or stats based on the magical powers obtained.
                    character.Health += 60;   //Give character more health
                    weapon.Ability1Dmg += 9; //Increase ability 1 damage
                    weapon.Ability2Dmg += 9; //Increase ability 2 damage
                    weapon.Ability3Dmg += 9; //Increase ability 3 damage

                    //Story
                    Console.WriteLine("You feel more powerful than ever before! (Gained 9 dmg and 60 health)\n");
                    Console.WriteLine("While talking to the man about your new power, a vampire runs after you!");

                    //Change vampire health in this situation to balance game
                    vampire.MonsterHealth = 65.3;
                    AttackSequence(weapon, character, vampire);
                    if (!gameOver)
                    {
                        Console.WriteLine("You hear a rumbling and realize the cave is closed off!");
                    }
                }

                else if (choice == 2 && !gameOver)
                {
                    //Story
                    Console.WriteLine("As you cautiously move forward, you hear the sound of dripping water and echoes of faint whispers.");
                    Console.WriteLine("The cave seems to grow darker and narrower as you progress.");
                    Console.WriteLine("1) Keep moving forward");
                    Console.WriteLine("2) Turn back and return to the entrance");
                    int nextChoice = int.Parse(Console.ReadLine());

                    if (nextChoice == 1)
                    {
                        //Story
                        Console.WriteLine("You continue deeper into the cave, your heart pounding with every step.");
                        Console.WriteLine("Suddenly, you come face to face with a colossal, ancient dragon!");
                        Console.WriteLine("It awakens from its slumber, its eyes locking onto you, and it lets out a deafening roar!");
                        Console.WriteLine("Prepare for battle!");

                        //Initiate a boss battle with the dragon
                        AttackSequence(weapon, character, dragon);
                        if (!gameOver)
                        {
                            Console.WriteLine("The man points out a strange chest. Maybe you should open it?");
                            Console.WriteLine("1) Open Chest");
                            nextChoice = int.Parse(Console.ReadLine());
                            Console.WriteLine("In all the ruckus the dragon closed the entrance with a boulder!");
                            while (nextChoice != 1 && !gameOver)
                            {
                                //Story
                                Console.WriteLine("The man points out a strange chest. Maybe you should open it?");
                                Console.WriteLine("1) Open Chest");
                                nextChoice = int.Parse(Console.ReadLine());
                            }
                            //Update the character's abilities or stats based on the magical powers obtained.
                            character.Health += 60;  //Give character more health
                            weapon.Ability1Dmg += 9; //Increase ability 1 damage
                            weapon.Ability2Dmg += 9; //Increase ability 2 damage
                            weapon.Ability3Dmg += 9; //Increase ability 3 damage
                            Console.WriteLine("You feel more powerful than ever before! (Gained 9 dmg and 60 health)\n");
                        }
                    }
                    else if (nextChoice == 2 && !gameOver)
                    {
                        //Story
                        Console.WriteLine("You decide to turn back, feeling that going any further might be too dangerous.");
                        Console.WriteLine("You make your way back to the entrance of the cave, leaving the mysterious chamber behind.");
                        Console.WriteLine("As you approach the exit, a large boulder crashes down in front of you!");
                        Console.WriteLine("You hear a rumble behind you and turn to see the dragon about to attack!");
                        //Initiate a boss battle with the dragon
                        AttackSequence(weapon, character, dragon);
                        if (!gameOver)
                        {
                            Console.WriteLine("The man points out a strange chest. Maybe you should open it?");
                            Console.WriteLine("1) Open Chest");
                            nextChoice = int.Parse(Console.ReadLine());

                            while (nextChoice != 1)
                            {
                                //Story
                                Console.WriteLine("The man points out a strange chest. Maybe you should open it?");
                                Console.WriteLine("1) Open Chest");
                                nextChoice = int.Parse(Console.ReadLine());
                            }
                            //Update the character's abilities or stats based on the magical powers obtained.
                            character.Health += 60; //Give character more health
                            weapon.Ability1Dmg += 9;//Increase ability 1 damage
                            weapon.Ability2Dmg += 9;//Increase ability 2 damage
                            weapon.Ability3Dmg += 9;//Increase ability 3 damage
                            Console.WriteLine("You feel more powerful than ever before! (Gained 9 dmg and 60 health)\n");

                        }
                    }
                    else
                    {
                        //If user puts in invalid input
                        Console.WriteLine("Invalid choice. Please choose again.");
                    }

                }

                if (!gameOver)
                {
                    //Story
                    Console.WriteLine("You and the man rummage around, trying to find a new way out of the cave.");
                    Console.WriteLine("As you explore deeper into the cave, you find a hidden passage...");
                    Console.WriteLine("Crouching around the corner of a vast cavern you see a dangerous looking golem blocking an exit from the cave.");
                    Console.WriteLine("Before you fight the golem the man reveals his name is Jeff, worried that he might not make it.");
                    Console.WriteLine("The golem notices you...\n");
                    AttackSequence(weapon, character, golem);
                    if (!gameOver)
                    {
                        Console.WriteLine("As you glance away from the golem you see Jeff lying on the floor...");
                        Console.WriteLine("Jeff! You cry out. Please dont leave me!");
                        Console.WriteLine("As your mourning your new friend's death a fairy decends from the top of the cavern.");
                        Console.WriteLine("Fairy: If you beat me in a fight I'll revive your friend!");
                        Console.WriteLine("1) Accept fight");
                        Console.WriteLine("2) Escape Cave");
                        choice = int.Parse(Console.ReadLine());
                    }
                    if (choice == 1 && !gameOver)
                    {
                        //Story
                        Console.WriteLine("Your health and mana have been partialy restored.");
                        character.Health += 40;
                        character.Mana += 55;
                        AttackSequence(weapon, character, fairy);
                        Console.WriteLine("\nAs promised your friend will be revived!");
                        Console.WriteLine("Jeff: Thank you for saving me!");
                        Console.WriteLine("Lets leave now!");
                        Console.WriteLine("You both escape the cave.\n");
                        FinalScreen(enemiesKilled, weapon, character);
                    }
                    else if (choice == 2 && !gameOver)
                    {
                        Console.WriteLine("You escape the cave.\n");
                        FinalScreen(enemiesKilled, weapon, character);
                    }
                    else 
                    {
                        if (!gameOver)
                        {
                            Console.WriteLine("Invalid input");
                        }
                    }
                    secondEnd = true;
                }
            } while (!gameOver);
        }

    }
}

