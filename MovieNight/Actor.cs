using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight
{
    class Actor
    {
        private static int increment = 1;

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private int actorId = 1;


        public int ActorId
        {
            get { return actorId; }
            set { actorId = value; }
        }

        public Actor(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            ActorId = increment++;
        }

        public Actor(string firstName, string lastName, int actorId) : this(firstName, lastName)
        {
            ActorId = actorId;
        }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
