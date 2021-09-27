using System;
using System.Collections.Generic;
using System.Text;

namespace SFML_Camera
{
    public sealed class PRandom
    {
        private static PRandom instance = null;
        private static readonly object padlock = new object();

        private Random rand;
        PRandom() 
        {
            rand = new Random();
        }

        public static PRandom Instance
        {
            get 
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PRandom();
                    }
                    return instance;
                }
            }
        }

        public int RandomInt(int min, int max)
        {
            int randomInt = rand.Next(0, max - min + 1);
            return randomInt + min;
        }

        public float RandomFloat(float min, float max)
        {
            float randomFloat = (float)rand.NextDouble();
            return (randomFloat * (max - min)) + min;
        }
    }
}
