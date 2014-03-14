namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
   
    using System;
    using System.Collections.Generic;

    public abstract class DialogScreen : BaseClassDrawable, IControllerEvents
    {
        protected bool cancel;
        protected bool bool_5;
        protected Color color_0;
        protected Color color_1;
        protected Color color_2;
        public float float_0;
        protected static DialogScreen dialogBase;
        protected int buttonListIndex;
        public int int_1;
        protected int int_2;
        protected int y_position;
        protected List<ButtonControl> buttons;
        private string _title;
        private string _text;
        protected Vector2 vector2_0;
        protected Vector2 vector2_1;

        public DialogScreen(Game game_0) : base(game_0)
        {
            this.buttons = new List<ButtonControl>();
            base.bool_2 = false;
            this._title = "";
            this._text = "";
            this.int_2 = 0;
            this.buttonListIndex = 0;
            this.int_1 = 0;
            this.float_0 = 1f;
            base.bool_3 = true;
            this.bool_5 = false;
            dialogBase = null;
        }

        public override void CalculateBounds(bool bool_6)
        {
            if (bool_6)
            {
                this.method_5();
            }
        }

        public override void CalculateColors()
        {
            this.color_0 = Color.FromNonPremultiplied(0xff, 0xff, 0xff, this.int_2);
            this.color_1 = Color.FromNonPremultiplied(200, 200, 200, this.int_2);
            this.color_2 = Color.FromNonPremultiplied(70, 70, 70, this.int_2);
        }

        public override void Draw(GameTime gameTime)
        {
            if (base.spriteBatch != null)
            {
                base.spriteBatch.Draw(base.DashBoard.BlurBackground.BluredBackground, base.DashBoard.rectangle, null, this.color_2, 0f, Vector2.Zero, SpriteEffects.None, Depth.float_11);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this._title, this.vector2_0, this.color_0, 0f, Vector2.Zero, (float) (1f * this.float_0), SpriteEffects.None, Depth.float_7);
                base.spriteBatch.DrawString(base.getFont("TextsLight"), this._text, this.vector2_1, this.color_1, 0f, Vector2.Zero, (float) (0.9f * this.float_0), SpriteEffects.None, Depth.float_7);
                foreach (ButtonControl button in this.buttons)
                {
                    button.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        public virtual int GetPriority()
        {
            return Priority.int_11;
        }

        public override void Hide()
        {
            dialogBase = null;
            base.DashBoard.controllerEvents.method_6(this);
            base.DashBoard.controllerEvents.method_8();
            base.Hide();
            this.cancel = false;
        }

        public void append(string ButtonText, ButtonControl.Options _options, Func<bool> _bindfunction)
        {
            ButtonControl item = new ButtonControl(base.DashBoard, this, _options);
            if (!ButtonText.Equals(""))
            {
                item.Text = ButtonText;
            }
            if (_bindfunction != null)
            {
                item.bindFunction = _bindfunction;
            }
            this.buttons.Insert(0, item);
            this.int_1 = this.buttons.Count - 1;
        }

        protected void method_5()
        {
            int heigth = ButtonControl.backgroundheigth;
            int margin = ButtonControl.margin;
            int padding = ButtonControl.padding;
            int backgroundpadding = ((this.buttons.Count - 1) * margin) + ((this.buttons.Count * padding) * 2);
            foreach (ButtonControl button in this.buttons)
            {
                backgroundpadding += (int) (base.getFont("TextsLight").MeasureString(button.Text).X * 0.75f);
            }
            int x_position = 0;
            int y_position = this.y_position;
            foreach (ButtonControl button in this.buttons)
            {
                int width = ((int) (base.getFont("TextsLight").MeasureString(button.Text).X * 0.75f)) + (padding * 2);
                button.background.X = ((base.DashBoard.width / 2) - (backgroundpadding / 2)) + x_position;
                button.background.Y = y_position;
                button.background.Width = width;
                button.background.Height = heigth;
                x_position += width + margin;
            }
        }

        public bool Back()
        {
            this.cancel = true;
            return true;
        }

        public virtual bool OnControllerAccept()
        {
            if ((!this.cancel && (this.buttons.Count > 0)) && ((this.buttons[this.buttonListIndex] != null) && (this.buttons[this.buttonListIndex].bindFunction != null)))
            {
                if ((!this.bool_5 && (this.buttons[this.buttonListIndex].Option != ButtonControl.Options.Back)) && (this.buttons[this.buttonListIndex].Option != ButtonControl.Options.Cancel))
                {
                    this.Hide();
                }
                else
                {
                    this.cancel = true;
                }
                this.buttons[this.buttonListIndex].bindFunction();
            }
            return true;
        }

        public virtual bool OnControllerBack()
        {
            this.cancel = true;
            return true;
        }

        public virtual bool OnControllerDetails()
        {
            return true;
        }

        public virtual bool OnControllerDown()
        {
            return true;
        }

        public virtual bool OnControllerLeft()
        {
            if (this.buttonListIndex > 0)
            {
                this.buttonListIndex--;
                foreach (ButtonControl class2 in this.buttons)
                {
                    class2.bool_1 = true;
                }
            }
            return true;
        }

        public virtual bool OnControllerRight()
        {
            if (this.buttonListIndex < (this.buttons.Count - 1))
            {
                this.buttonListIndex++;
                foreach (ButtonControl dialogEvent in this.buttons)
                {
                    dialogEvent.bool_1 = true;
                }
            }
            return true;
        }

        public virtual bool OnControllerSort()
        {
            return true;
        }

        public virtual bool OnControllerUp()
        {
            return true;
        }

        public override void Show()
        {
            if (!this.cancel)
            {
                if (dialogBase != null)
                {
                    dialogBase.Hide();
                }
                this.int_2 = 0;
                this.buttonListIndex = this.int_1;
                base.DashBoard.controllerEvents.addControllerEvent(this);
                base.DashBoard.controllerEvents.method_7(this);
                foreach (ButtonControl class2 in this.buttons)
                {
                    class2.bool_4 = false;
                    class2.int_0 = 0;
                    class2.CalculateBounds(true);
                }
                dialogBase = this;
                base.Show();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.cancel)
            {
                if (this.int_2 > 0)
                {
                    this.int_2 -= 15;
                    base.bool_1 = true;
                    foreach (ButtonControl class2 in this.buttons)
                    {
                        class2.bool_1 = true;
                    }
                    if (this.int_2 == 0)
                    {
                        this.Hide();
                    }
                }
            }
            else if (this.int_2 < 0xff)
            {
                this.int_2 += 15;
                base.bool_1 = true;
                foreach (ButtonControl class3 in this.buttons)
                {
                    class3.bool_1 = true;
                }
            }
            foreach (ButtonControl _event in this.buttons)
            {
                _event.int_0 = this.int_2;
                _event.bool_4 = false;
            }
            if (this.buttons.Count > 0)
            {
                this.buttons[this.buttonListIndex].bool_4 = true;
            }
            foreach (ButtonControl _event in this.buttons)
            {
                _event.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                base.setTitle = true;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                base.setTitle = true;
            }
        }
    }
}

