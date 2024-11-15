namespace ShapeScape
{
    public class FastMorph : IMorph
    {
        public void ApplyMorph(Player player)
        {
            player.velocity.x *= 5;  
            player.jumpHeight = 25;  
        }

        public void RevertMorph(Player player)
        {
            player.velocity.x /= 2;  
            player.jumpHeight = 15;  
        }
    }
}
