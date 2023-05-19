﻿using System;
using System.Collections.Generic;

namespace P03PlayersWebAPI.Models;

public partial class VolleyballPlayer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int Number { get; set; }
}