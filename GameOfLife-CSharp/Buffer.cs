using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife_CSharp
{
    class Buffer
    {
        private byte[,] cells;

        public Buffer()
        {
            cells = new byte[Config.getWidth(), Config.getHeight()];
        }

        public void FillRandom()
        {
            Random rnd = new Random();
			double seed = Config.getSeed()/10.0;

            for (int i = 0; i < Config.getWidth(); i++)
			{
                for (int j = 0; j < Config.getHeight(); j++)
				{
					cells[i,j] = (byte)Math.Floor(rnd.NextDouble() + seed);
				}
			}
        }

        public bool is_live(int x, int y)
        {
            if (cells[x, y] == 1)
                return true;
            else
                return false;
        }

        public void born(int x, int y)
        {
            cells[x, y] = 1;
        }

        public void die(int x, int y)
        {
            cells[x, y] = 0;
        }

    }
}
