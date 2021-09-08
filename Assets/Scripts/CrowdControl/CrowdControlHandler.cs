using System.Collections.Generic;

namespace CrowdControl
{
    public class CrowdControlHandler : ICrowdControl
    {
        private Dictionary<object, CrowdControlType> appliedCCs = new Dictionary<object, CrowdControlType>();

        public CrowdControlType CrowdControlState
        {
            get
            {
                var state = CrowdControlType.None;
                foreach (var crowdControlType in appliedCCs.Values)
                {
                    state |= crowdControlType;
                }

                return state;
            }
        }

        public void AddCrowdControl(CrowdControlType crowdControlType, object source)
        {
            if (appliedCCs.ContainsKey(source))
            {
                //add the new state to the current state afflicted by this source
                appliedCCs[source] |= crowdControlType;
            }
            else
            {
                appliedCCs.Add(source, crowdControlType);
            }
        }

        public void RemoveCrowdControl(CrowdControlType crowdControlType, object source)
        {
            appliedCCs[source] &= ~crowdControlType; // remove the specified flag from that sources current state
            if (appliedCCs[source] == CrowdControlType.None)
            {
                appliedCCs.Remove(source);
            }
        }
    }
}