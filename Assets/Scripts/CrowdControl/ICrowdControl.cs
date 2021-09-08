namespace CrowdControl
{
    public interface ICrowdControl
    {
        CrowdControlType CrowdControlState { get; }

        //TODO find proper type for source
        void AddCrowdControl(CrowdControlType crowdControlType, object source);
        void RemoveCrowdControl(CrowdControlType crowdControlType, object source);
    }
}