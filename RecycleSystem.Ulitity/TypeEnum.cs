using System;
using System.Collections.Generic;
using System.Text;

namespace RecycleSystem.Ulitity
{
    public class TypeEnum
    {
        public enum UserType
        {
            isGeneralUser=1,
            isEnterpriseUser
        }
        public enum DemendOrderStatus
        {
            unAccept=1,
            Accepted,
            Finished
        }
        public enum PressStatus
        {
            unPress,
            Pressed
        }
        public enum OrderStatus
        {
            Running,
            Finished
        }
    }
}
