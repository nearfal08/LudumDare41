using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Player
{
    private int stamina;
    private int health;
    private int hunger;
    private int thirst;
    private int tempature;

    public Player()
    {

    }

    public int Stamina
    {
        get { return this.stamina; }
        set { this.stamina = value; }
    }

    public int Health
    {
        get { return this.health; }
        set { this.health = value; }
    }

    public int Hunger
    {
        get { return this.hunger; }
        set { this.hunger = value; }
    }

    public int Thirst
    {
        get { return this.thirst; }
        set { this.thirst = value; }
    }

    public int Tempature
    {
        get { return this.tempature; }
        set { this.tempature = value; }
    }

}
