namespace MGDash.Sources.Model
{
    using System;

    public sealed class Category
    {
        public string display_name;
        public int id;
        public string name;

        public Category(int int_0, string string_0, string string_1)
        {
            this.id = int_0;
            this.name = string_0;
            this.display_name = string_1;
        }
    }
}

