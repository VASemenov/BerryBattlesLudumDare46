namespace Structures
{
    public class SoldierFormation
    {
        public Soldier Soldier;
        public SoldierFormation Front;
        public SoldierFormation Back;

        public int Count()
        {
            var number = 0;
            var reference = this;
            while (Back != null)
            {
                number++;
                reference = reference.Back;
            }

            return number;
    }
}
}