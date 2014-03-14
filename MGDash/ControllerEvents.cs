namespace MGDash
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
   
    using Nuclex.Input;
    using Nuclex.Input.Devices;
    using System;
    using System.Collections.Generic;

    public sealed class ControllerEvents : IControllerEvents, IControllerEventsMenu {
        private bool[] bool_0;
        private bool[] bool_1;
        private bool[] bool_2;
        private bool bool_3;
        private double double_0;
        private Func<bool>[] func_0;
        private IControllerEvents controllerEvent;
        public IControllerEventsMenu ginterface1_0;
        public InputManager inputManager_0;
        private int[] int_0;
        private int int_1;
        private Keys[] keys_0;
        private List<IControllerEvents> ControllerEvent_list0;
        private List<IControllerEvents> ControllerEvent_list1;
        private SortedList<int, IControllerEvents> sortedList_0;
        private Stack<IControllerEvents> stack_0;

        public ControllerEvents(DashBoard _dashBoard) {
            this.inputManager_0 = new InputManager(_dashBoard.Services, _dashBoard.Window.Handle);
            _dashBoard.Components.Add(this.inputManager_0);
            this.sortedList_0 = new SortedList<int, IControllerEvents>();
            this.ControllerEvent_list0 = new List<IControllerEvents>();
            this.ControllerEvent_list1 = new List<IControllerEvents>();
            this.double_0 = 0.0;
            this.func_0 = new Func<bool>[] { new Func<bool>(this.OnControllerLeft), new Func<bool>(this.OnControllerRight), new Func<bool>(this.OnControllerUp), new Func<bool>(this.OnControllerDown), new Func<bool>(this.OnControllerAccept), new Func<bool>(this.OnControllerBack), new Func<bool>(this.OnControllerDetails), new Func<bool>(this.OnControllerSort), new Func<bool>(this.OnControllerMenu) };
            this.keys_0 = new Keys[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Enter, Keys.Back, Keys.F1, Keys.F2, Keys.Escape };
            this.bool_0 = new bool[9];
            this.bool_1 = new bool[9];
            this.bool_2 = new bool[9];
            this.int_0 = new int[9];
            this.stack_0 = new Stack<IControllerEvents>();
            this.int_1 = 0;
            this.controllerEvent = null;
            this.bool_3 = true;
        }

        public int GetPriority() {
            return Priority.int_11;
        }

        public void method_0() {
            this.bool_3 = true;
        }

        public bool method_1() {
            return this.bool_3;
        }

        public bool method_2(IControllerEvents ICEvents) {
            return this.sortedList_0.ContainsValue(ICEvents);
        }

        public void method_3(GameTime gameTime_0)
        {
            if (this.bool_3) {

                foreach (IControllerEvents interface2 in this.ControllerEvent_list1) {
                    this.sortedList_0.Remove(interface2.GetPriority());
                }
                
                this.ControllerEvent_list1.Clear();
                
                foreach (IControllerEvents ICEvent in this.ControllerEvent_list0) {
                    this.sortedList_0.Add(ICEvent.GetPriority(), ICEvent);
                }
                this.ControllerEvent_list0.Clear();
                this.double_0 += gameTime_0.ElapsedGameTime.Milliseconds;
                List<Keys> list = new List<Keys>();
                bool[] flagArray = new bool[0];
                
                if (this.GamePadAtached()) {
                    this.inputManager_0.Update();
                    GamePadState state = this.inputManager_0.GamePads[this.inputManager_0.GamePads[0].IsAttached ? 0 : 4].GetState();
                    flagArray = new bool[] { (state.ThumbSticks.Left.X < -0.25) || (state.DPad.Left == ButtonState.Pressed), (state.ThumbSticks.Left.X > 0.25) || (state.DPad.Right == ButtonState.Pressed), (state.ThumbSticks.Left.Y > 0.25) || (state.DPad.Up == ButtonState.Pressed), (state.ThumbSticks.Left.Y < -0.25) || (state.DPad.Down == ButtonState.Pressed), state.Buttons.A == ButtonState.Pressed, state.Buttons.B == ButtonState.Pressed, state.Buttons.Y == ButtonState.Pressed, state.Buttons.X == ButtonState.Pressed, state.Buttons.Start == ButtonState.Pressed };
                }
                else {
                    this.inputManager_0.Update();
                    list.AddRange(this.inputManager_0.GetKeyboard().GetState().GetPressedKeys());
                }
                for (int i = 0; i < this.keys_0.Length; i++) {

                    if (this.GamePadAtached() ? flagArray[i] : list.Contains(this.keys_0[i]))
                    {
                        this.bool_0[i] = true;
                        if (this.int_0[i] < 0x10)
                        {
                            this.int_0[i]++;
                        }
                        else
                        {
                            this.bool_2[i] = i < 4;
                        }
                    }
                    else if (!this.bool_0[i]) {
                        this.bool_2[i] = false;
                        this.int_0[i] = 0;
                        if (this.bool_1[i])
                        {
                            this.bool_1[i] = false;
                        }
                    }
                }
                if (this.double_0 >= 40.0) {

                    for (int j = 0; j < this.bool_0.Length; j++)
                    {
                        if ((this.bool_0[j] && !this.bool_1[j]) || this.bool_2[j])
                        {
                            this.func_0[j]();
                            this.bool_1[j] = !this.bool_2[j];
                        }
                        this.bool_0[j] = false;
                    }
                    this.double_0 = 0.0;
                }
                if (this.int_1 > 0)
                {
                    while (this.stack_0.Count > 0)
                    {
                        if (this.int_1 <= 0)
                        {
                            break;
                        }
                        this.stack_0.Pop();
                        this.int_1--;
                    }
                }
                if (this.controllerEvent != null) {

                    this.stack_0.Push(this.controllerEvent);
                    this.controllerEvent = null;
                }
            }
        }

        public bool GamePadAtached() {

            if (!this.inputManager_0.GamePads[0].IsAttached) {
                return this.inputManager_0.GamePads[4].IsAttached;
            }
            return true;
        }

        public void addControllerEvent(IControllerEvents controllerEvent) {
            this.ControllerEvent_list0.Add(controllerEvent);
        }

        public void method_6(IControllerEvents ginterface0_1) {
            this.ControllerEvent_list1.Add(ginterface0_1);
        }

        public void method_7(IControllerEvents ginterface0_1)
        {
            this.controllerEvent = ginterface0_1;
        }

        public void method_8()
        {
            this.int_1++;
        }

        public bool OnControllerAccept()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values) {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent)) {
                    ICEvent.OnControllerAccept();
                }
            }
            return true;
        }

        public bool OnControllerBack()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values)
            {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent))
                {
                    ICEvent.OnControllerBack();
                }
            }
            return true;
        }

        public bool OnControllerDetails()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values)
            {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent))
                {
                    ICEvent.OnControllerDetails();
                }
            }
            return true;
        }

        public bool OnControllerDown()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values)
            {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent))
                {
                    ICEvent.OnControllerDown();
                }
            }
            return true;
        }

        public bool OnControllerLeft()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values)
            {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent))
                {
                    ICEvent.OnControllerLeft();
                }
            }
            return true;
        }

        public bool OnControllerMenu()
        {
            this.ginterface1_0.OnControllerMenu();
            return true;
        }

        public bool OnControllerRight()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values)
            {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent))
                {
                    ICEvent.OnControllerRight();
                }
            }
            return true;
        }

        public bool OnControllerSort()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values)
            {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent))
                {
                    ICEvent.OnControllerSort();
                }
            }
            return true;
        }

        public bool OnControllerUp()
        {
            foreach (IControllerEvents ICEvent in this.sortedList_0.Values)
            {
                if ((this.stack_0.Count == 0) || this.stack_0.Peek().Equals(ICEvent))
                {
                    ICEvent.OnControllerUp();
                }
            }
            return true;
        }
    }
}

