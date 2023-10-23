using Common;

namespace Tank.Upgrades
{
    public struct PresentationInformation { }

    public interface IUpgrade
    {
        public uint CurrentLevel { get; }
        public bool IsReachedMaxLevel { get; }
        public PresentationInformation GetPresentationInformation();
    }

    [InterfaceEditor]
    public interface ITankUpgrade : IUpgrade
    {
        public void ApplyUpgrade(TankImpl tank);
    }
}
