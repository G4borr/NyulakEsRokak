namespace RokakEsNyulakLib
{
    public class Field
    {
        public Rabbit Rabbit { get; set; }
        public Fox Fox { get; set; }
        public int GrassState { get; set; } // 0: Nincs fű, 1: Zsenge fű, 2: Kifejlett fű

        public Field()
        {
            Rabbit = null;
            Fox = null;
            GrassState = 0; // Kezdetben nincs fű
        }

        // Fű növekedése
        public void GrowGrass()
        {
            if (GrassState < 2)
            {
                GrassState++; // Fű állapotának növelése
            }
        }
    }
}
