﻿using SpaceshipBattle.Contracts.Providers;
using SpaceshipBattle.DataBase;
using SpaceshipBattle.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceshipBattle.Core.Registration
{
    public class SelectingSpaceship : ISelectingSpaceship
    {
        private readonly IApplicationInterface applicationInterface;
        private readonly IDataBase dataBase;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IMenu menu;
        private int positionRow;
        private int positionCol;
        private int rowOffset = 4;
        private int colOffset = 0;
        private Dictionary<string, string> parameters;
        private int playerAvailableМoney;

        public SelectingSpaceship(
            IApplicationInterface applicationInterface,
            IDataBase dataBase, 
            IWriter writer, 
            IReader reader,
            IMenu menu)
        {
            this.applicationInterface = applicationInterface;
            this.dataBase = dataBase;
            this.writer = writer;
            this.reader = reader;
            this.menu = menu;
        }

        public int PlayerAvailableМoney
        {
            get => this.playerAvailableМoney;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Player can not hold a loan!");
                }
                this.playerAvailableМoney = value;
            }
        }

        public Dictionary<string, string> ChooseSpaceShip()
        {

            // Arrange
            this.parameters = new Dictionary<string, string>();
            playerAvailableМoney = 10000;

            this.positionRow = (applicationInterface.WindowHeight / 2) - rowOffset - (this.dataBase.SpaceshipNames.Length / 2);
            this.positionCol = (applicationInterface.WindowWidth / 2) - colOffset;
            int focusPosition = 0;

            var componentList = this.dataBase.SpaceshipNames;
            int lengthOfElements = this.dataBase.SpaceshipNames.Length;

            
            while (true)
            {
                if (reader.KeyAvailable())
                {
                   ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (focusPosition > 0)
                        {
                            focusPosition--;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (focusPosition < lengthOfElements - 1)
                        {
                            focusPosition++;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        parameters.Add("ship", this.dataBase.SpaceshipNames[focusPosition]);
                        writer.ClearScreen();
                        return parameters.AppendDictionary(ChooseComponent());
                    }
                }

                // write info message
                this.writer.WriteTextCenter(this.positionCol, this.positionRow - 2, "Choose your Spaceship:");
                this.writer.WriteTextCenter(this.positionCol, this.positionRow - 1, "__________________");

                menu.DrawMenu(componentList, this.positionCol, this.positionRow, focusPosition);

            }

        }

        public Dictionary<string, string> ChooseComponent()
        {
            this.positionRow = (applicationInterface.WindowHeight / 2) - rowOffset - (this.dataBase.ComponentsInSpaceship.Length / 2);
            this.positionCol = (applicationInterface.WindowWidth / 2) - colOffset;
            int focusPosition = 0;

            List<string> componentList = new List<string>(this.dataBase.ComponentsInSpaceship);

            while (true)
            {
                if (reader.KeyAvailable())
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (focusPosition > 0)
                        {
                            focusPosition--;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (focusPosition < componentList.Count - 1)
                        {
                            focusPosition++;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        string componet = componentList[focusPosition].ToLower();
                        switch (componet)
                        {
                            case "weapon":
                                parameters.Add(componet, ChooseWeapon());
                                componentList.Remove(componentList[focusPosition]);
                                focusPosition = 0;
                                break;

                            case "engine":
                                parameters.Add(componet, ChooseEngine());
                                componentList.Remove(componentList[focusPosition]);
                                focusPosition = 0;
                                break;
                            case "armour":
                                parameters.Add(componet, ChooseArmour());
                                componentList.Remove(componentList[focusPosition]);
                                focusPosition = 0;

                                break;
                        }

                        this.writer.ClearScreen();
                    }
                }

                if (componentList.Count == 0)
                {
                    this.writer.ClearScreen();
                    return parameters;
                }
                else
                {
                    // info message
                    writer.WriteTextCenter(this.positionCol, this.positionRow - 2, "Choose component for your ship:");
                    writer.WriteTextCenter(this.positionCol, this.positionRow - 1, "_____________________");

                    // Draw Menu for ship components
                    menu.DrawMenu(componentList, this.positionCol, this.positionRow, focusPosition);
                }
            }
        }

        private string ChooseWeapon()
        {
            Dictionary<string, int> weapons = SelectElementsByShipType("weapon");

            this.positionRow = (applicationInterface.WindowHeight / 2) - rowOffset - (weapons.Count / 2);
            this.positionCol = (applicationInterface.WindowWidth / 2) - colOffset;

            int focusPosition = 0;

            while (true)
            {
                if (reader.KeyAvailable())
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (focusPosition > 0)
                        {
                            focusPosition--;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (focusPosition < weapons.Count - 1)
                        {
                            focusPosition++;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        playerAvailableМoney -= weapons.Values.ElementAt(focusPosition);
                        return weapons.Keys.ElementAt(focusPosition);
                    }
                }

                // info message
                this.writer.WriteTextCenter(this.positionCol, this.positionRow - 2, "Choose your weapon of destruction:");
                this.writer.WriteTextCenter(this.positionCol, this.positionRow - 1, "_________________________");

                // Draw weapon menu
                menu.DrawMenu(weapons, this.positionCol, this.positionRow, focusPosition);
            }
        }

        private string ChooseEngine()
        {
            Dictionary<string, int> engines = SelectElementsByShipType("engine");

            this.positionRow = (applicationInterface.WindowHeight / 2) - rowOffset - (engines.Count / 2);
            this.positionCol = (applicationInterface.WindowWidth / 2) - colOffset;

            int focusPosition = 0;

            while (true)
            {
                if (reader.KeyAvailable())
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (focusPosition > 0)
                        {
                            focusPosition--;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (focusPosition < engines.Count - 1)
                        {
                            focusPosition++;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        playerAvailableМoney -= engines.Values.ElementAt(focusPosition);
                        return engines.Keys.ElementAt(focusPosition);
                    }
                }

                // info message
                writer.WriteTextCenter(this.positionCol, this.positionRow - 2, "Choose your flying power (engine):");
                writer.WriteTextCenter(this.positionCol, this.positionRow - 1, "___________________________");

                // Draw engine menu
                menu.DrawMenu(engines, this.positionCol, this.positionRow, focusPosition);

            }
        }

        private string ChooseArmour()
        {
            Dictionary<string, int> armours = SelectElementsByShipType("armour");

            this.positionRow = (applicationInterface.WindowHeight / 2) - rowOffset - (armours.Count / 2);
            this.positionCol = (applicationInterface.WindowWidth / 2) - colOffset;

            int focusPosition = 0;

            while (true)
            {
                if (reader.KeyAvailable())
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (focusPosition > 0)
                        {
                            focusPosition--;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (focusPosition < armours.Count - 1)
                        {
                            focusPosition++;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        playerAvailableМoney -= armours.Values.ElementAt(focusPosition);
                        return armours.Keys.ElementAt(focusPosition);
                    }
                }

                // info message
                this.writer.WriteTextCenter(this.positionCol, this.positionRow - 2, "Choose your impenetrable skin (armour):");
                this.writer.WriteTextCenter(this.positionCol, this.positionRow - 1, "___________________________");

                // Draw armour menu
                menu.DrawMenu(armours, this.positionCol, this.positionRow, focusPosition);
            }
        }

        private Dictionary<string, int> SelectElementsByShipType(string element)
        {
            if (this.parameters.Keys.Contains("ship"))
            {
                switch (this.parameters["ship"])
                {
                    case "Dross-Mashup Spaceship":
                        switch (element)
                        {
                            case "armour":
                                return SelectElementsByPrice(this.dataBase.DrossMashupArmours);
                            case "weapon":
                                return SelectElementsByPrice(this.dataBase.DrossMashupWeapons);
                            case "engine":
                                return SelectElementsByPrice(this.dataBase.DrossMashupEngines);
                            default:
                                return null;
                        }
                    case "Futuristic Spaceship":
                        switch (element)
                        {
                            case "armour":
                                return SelectElementsByPrice(this.dataBase.FuturisticArmours);
                            case "weapon":
                                return SelectElementsByPrice(this.dataBase.FuturisticWeapons);
                            case "engine":
                                return SelectElementsByPrice(this.dataBase.FuturisticEngines);
                            default:
                                return null;
                        }
                    default:
                        return null;
                }
            }
            else
            {
                throw new ArgumentNullException("The ship is not selected!");
            }
        }

        private Dictionary<string, int> SelectElementsByPrice(Dictionary<string, int> shipType)
        {
            Dictionary<string, int> elements = new Dictionary<string, int>();

            foreach (var element in shipType)
            {
                if (playerAvailableМoney - element.Value >= 0)
                {
                    elements.Add(element.Key, element.Value);
                }
            }
            return new Dictionary<string, int>(elements);
        }
    }
}
