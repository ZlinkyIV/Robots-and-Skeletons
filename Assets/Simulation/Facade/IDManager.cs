using System.Collections.Generic;

namespace Simulation.Facade
{
    public class IDManager
    {
        private List<uint> list = new();
        private uint previousID = 0;

        public uint GetNewID()
        {
            for (uint i = previousID + 1; i != previousID; i++)
                if (!list.Contains(i))
                {
                    previousID = i;
                    return i;
                }

            return ++previousID;
        }

        public void FreeID(uint ID)
        {
            list.Remove(ID);
        }
    }
}
