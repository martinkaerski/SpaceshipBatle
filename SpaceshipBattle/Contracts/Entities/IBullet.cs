﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceshipBattle.Contracts
{
    public interface IBullet
    {
        int PositionX { get; set; }

        int PositionY { get; set; }
    }
}
