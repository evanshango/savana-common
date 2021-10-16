using System.Runtime.Serialization;

namespace Savana.Common.Entities
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "PAYMENT_FAILED")] 
        PaymentFailed,

        [EnumMember(Value = "PAYMENT_PENDING")]
        PaymentPending,

        [EnumMember(Value = "PAYMENT_RECEIVED")]
        PaymentReceived,

        [EnumMember(Value = "PAYMENT_CANCELLED")]
        PaymentCancelled,
    }
}