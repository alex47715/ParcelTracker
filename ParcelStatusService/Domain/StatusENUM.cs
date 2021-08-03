using System.Runtime.Serialization;

namespace ParcelStatusService
{
    public enum StatusENUM
    {
        [EnumMember(Value = "WaitingSender")]
        WaitingSender = 0,
        [EnumMember(Value = "InSenderOffice")]
        InSenderOffice = 1,
        [EnumMember(Value = "ReadyToShip")]
        ReadyToShip = 2,
        [EnumMember(Value = "OnTheWay")]
        OnTheWay = 3,
        [EnumMember(Value = "InRecipientOffice")]
        InRecipientOffice = 4,
        [EnumMember(Value = "Received")]
        Received = 5,
        [EnumMember(Value = "ReceivedBySender")]
        ReceivedBySender = 6
    }
}
