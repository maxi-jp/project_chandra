using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS_XNA_Shooter
{
    public class Player
    {
        private int totalScore;     // points earned in the actual game
        private int actualScore;    // points earned in the actual level
        private int lifes;
		private int nada;

        public Player(int lifes)
        {
            this.lifes = lifes;
        }

        public int GetTotalScore()
        {
            return totalScore;
        }

        public int GetActualScore()
        {
            return actualScore;
        }

        public void EarnPoints(int i)
        {
            actualScore += i;
            totalScore += i;
        }

        public int GetLife()
        {
            return lifes;
        }

        public void LoseLife()
        {
            lifes--;
        }

        public void LoseLife(int i)
        {
            lifes -= i;
        }

        public void EarnLife()
        {
            lifes++;
        }

        public void EarnLife(int i)
        {
            lifes += i;
        }

    } // class Player
} // namespace IS_XNA_Shooter
