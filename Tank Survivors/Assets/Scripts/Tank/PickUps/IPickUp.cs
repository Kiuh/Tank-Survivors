namespace Tank.PickUps
{
    public interface IPickUp
    {
        public string PickupName { get; }
        public bool Grabbed { get; }
        public void Grab(TankImpl tank);
    }
}
