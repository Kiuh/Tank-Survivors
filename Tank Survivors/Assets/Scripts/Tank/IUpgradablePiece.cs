namespace Tank
{
    public struct PresentationInformation { }

    public interface IUpgradablePiece
    {
        public uint CurrentLevel { get; }
        public bool IsReachedMaxLevel { get; }
        public void ApplyUpgrade(TankImpl tank);
        public PresentationInformation GetPresentationInformation();
    }
}
