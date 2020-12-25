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
            Finished,
            ApplyingCancel,
            ForbinCancel,
            Canceled
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
        public enum WorkFlowType
        {
            SpecialWithdrew=1
        }
        public enum WorkFlowStatus
        {
            Applying=1,
            Withdrew,
            UnAccept,
            Allow
        }
    }
}
