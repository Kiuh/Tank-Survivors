namespace Tank.PickUps
{
    public interface IPickUp
    {
        public bool Grabbed { get; }
        public void Grab(TankImpl tank);
    }
}
