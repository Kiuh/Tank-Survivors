namespace Tank.UpgradablePiece
{
    public struct UpgradeVariantInformation
    {
        private string guid;
        public string Guid => guid;

        private string upgradeInformation;
        public string UpgradeInformation => upgradeInformation;

        public UpgradeVariantInformation(string upgradeInformation, string guid)
        {
            this.guid = guid;
            this.upgradeInformation = upgradeInformation;
        }
    }
}
