namespace MGDash
{
    using Microsoft.Xna.Framework;
    
    using System;
    using System.Collections.Generic;

    public sealed class InitializeComponents
    {
        public Components intro_Components;
        public Components menu_Components;
        public Components games_Components;
        public Components options_Items;
        public Components model_Components;
        public TitleBar titleBar;
        private DashBoard dashBoard;
       

        public InitializeComponents(Game _dashBoard)
        {
            this.dashBoard = (DashBoard) _dashBoard;
            this.titleBar = new TitleBar(_dashBoard);
            
            this.intro_Components = new Components(this.dashBoard);
            this.menu_Components = new Components(this.dashBoard);
            this.games_Components = new Components(this.dashBoard);
            this.options_Items = new Components(this.dashBoard);
            this.model_Components = new Components(this.dashBoard);

            this.dashBoard.DashboardComponents = new List<Components>();
            //this.dashBoard.ModelComponents = new List<Components>(); 
            this.dashBoard.DashboardComponents.Add(this.intro_Components);
            this.dashBoard.DashboardComponents.Add(this.menu_Components);
            this.dashBoard.DashboardComponents.Add(this.games_Components);
            this.dashBoard.DashboardComponents.Add(this.options_Items);
            //this.dashBoard.DashboardComponents.Add(this.model_Components);
        }

        public void start()
        {
            //this.addmodelComponents();
            this.addIntroComponents();
            this.addMenuComponents();
            this.addGamesComponents();
            this.addOptionsItems();
            this.intro_Components.Show();
        }

        private void addmodelComponents()
        {
            //Case360 case360 = new Case360(this.dashBoard);
            //this.model_Components.DrawableComponents.Add("case", case360);
            //this.games_Components.ControllerEventlist.Add(Games.gameslist);

        }

        private void addIntroComponents()
        {
            this.intro_Components.bool_4 = true;
            this.intro_Components.double_0 = 200.0;
            this.intro_Components.components = this.menu_Components;

            //this.intro_Components.components = this.model_Components;

            Intro opening = new Intro(this.dashBoard);
            this.intro_Components.DrawableComponents.Add("opening", opening);
            this.intro_Components.ControllerEventlist.Add(opening);
        }

        private void addMenuComponents()
        {
            Menu menu = new Menu(this.dashBoard);
            this.menu_Components.DrawableComponents.Add("menu", menu);
            this.menu_Components.DrawableComponents.Add("titlebar", this.titleBar);
            
            foreach (MenuItem MItem in menu.list_0)
            {
                this.menu_Components.DrawableComponents.Add("btn_" + MItem.videos, MItem);
            }
            this.menu_Components.ControllerEventlist.Add(menu);
        }

        private void addGamesComponents()
        {
            GamesSection Games = new GamesSection(this.dashBoard);

            this.games_Components.DrawableComponents.Add("collection", Games);
            this.games_Components.DrawableComponents.Add("gamelist", Games.Coverflow);
            this.games_Components.DrawableComponents.Add("categories_selector", Games.Coverflow.CategorySlide);
            int num = 0;
            
            foreach (Box box in Games.Coverflow.CoverFlowItems)
            {
                this.games_Components.DrawableComponents.Add("box_" + num++, box);
                this.games_Components.DrawableComponents.Add("case_" + num++, box.Case);

            }
            this.games_Components.ControllerEventlist.Add(Games.Coverflow);
        }

        public void addOptionsItems()
        {
            OptionsSection options = new OptionsSection(this.dashBoard);
            this.options_Items.DrawableComponents.Add("options_menu", options);
            this.options_Items.ControllerEventlist.Add(options);
        }

        
    }
}

