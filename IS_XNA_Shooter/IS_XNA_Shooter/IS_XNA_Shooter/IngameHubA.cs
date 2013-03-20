using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class IngameHubA : IngameHub
    {
        /* ------------------- ATTRIBUTES ------------------- */
        private Texture2D textureBase;
        private Rectangle sorceRecLeft, sorceRecCenter1, sorceRecCenter2, sorceRecRight;
        private Sprite left, right;
        private List<Sprite> center;

        /* ------------------- CONSTRUCTORS ------------------- */
        public IngameHubA(Texture2D textureBase, int playerLifes)
            : base(playerLifes)
        {
            this.textureBase = textureBase;

            sorceRecLeft = new Rectangle(0, 100, 27, 67);
            sorceRecCenter1 = new Rectangle(27, 100, 57, 67);
            sorceRecCenter2 = new Rectangle(84, 100, 57, 67);
            sorceRecRight = new Rectangle(141, 100, 28, 67);

            left = new Sprite(false, new Vector2((SuperGame.screenWidth / 2) - 27 - 57 * playerLifes/2, 0),
                0, textureBase, sorceRecLeft);
            right = new Sprite(false, new Vector2((SuperGame.screenWidth / 2) + 57 * playerLifes/2, 0),
                0, textureBase, sorceRecRight);

            center = new List<Sprite>();
            Sprite sprite;
            for (int i = 0; i < playerLifes; i++)
            {
                sprite = new Sprite(false, new Vector2((SuperGame.screenWidth / 2) - 57 * playerLifes / 2 + 57 * i, 0),
                    0, textureBase, sorceRecCenter1);
                center.Add(sprite);
            }
        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            left.DrawRectangle(spriteBatch);

            foreach (Sprite s in center)
                s.DrawRectangle(spriteBatch);

            right.DrawRectangle(spriteBatch);
        }

        public override void PlayerLosesLive()
        {
            base.PlayerLosesLive();

            for (int i = lifesActual; i < center.Count(); i++)
            {
                center[i].SetRectangle(sorceRecCenter2);
            }
        }

        public override void PlayerEarnsLife()
        {
            base.PlayerEarnsLife();

            if (lifesActual > center.Count())
            {
                center.Clear();
                Sprite sprite;
                for (int i = 0; i < lifesBase; i++)
                {
                    sprite = new Sprite(false, new Vector2((SuperGame.screenWidth / 2) - 57 * lifesBase / 2 + 57 * i, 0),
                        0, textureBase, sorceRecCenter1);
                    center.Add(sprite);
                }
                left = new Sprite(false, new Vector2((SuperGame.screenWidth / 2) - 27 - 57 * lifesBase / 2, 0),
                0, textureBase, sorceRecLeft);
                right = new Sprite(false, new Vector2((SuperGame.screenWidth / 2) + 57 * lifesBase / 2, 0),
                    0, textureBase, sorceRecRight);
            }
        }

    } // class IngameHubA
}
