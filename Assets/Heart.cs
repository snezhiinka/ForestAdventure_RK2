// Lesson 6: Heart redefines hit effect to heal (+1 HP).
public class Heart : Hazard
{
    protected override void OnHitPlayer(Health player)
    {
        player.Heal(1);
    }
}
