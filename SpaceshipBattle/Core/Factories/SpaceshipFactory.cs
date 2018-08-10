﻿using SpaceshipBattle.Contracts.Factories;
using SpaceshipBattle.Entities.Spaceships;
using SpaceshipBattle.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceshipBattle.Core.Factories
{
    class SpaceshipFactory : ISpaceshipFactory
    {
        public ISpaceship CreateSpaceship(string model, IEngine engine, IArmour armour, IWeapon weapon)
        {
            switch (model)
            {
                case "Dross-Mashup Spaceship":
                    return new DrossMashupSpaceship(engine, armour, weapon );
                case "Futuristic Spaceship":
                    return new FuturisticSpaceship(engine, armour, weapon);

                default: throw new ArgumentException("There is no such spaceship!");
            }
        }
    }
}
