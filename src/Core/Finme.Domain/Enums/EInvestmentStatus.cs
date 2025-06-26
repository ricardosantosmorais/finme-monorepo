using Finme.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Domain.Enums
{
    public enum ETransactionStatus
    {
        [StringValue("interest")]
        Interest,

        [StringValue("started")]
        ReportedValue,

        [StringValue("contract-accept")]
        ContractAccept,

        [StringValue("contract-signed")]
        ContractSigned,

        [StringValue("pending-funds")]
        PendingFunds,

        [StringValue("ted-send")]
        TedSent,

        [StringValue("confirmed")]
        ConfirmedValue,

        [StringValue("cancelled-by-client")]
        CancelledByClient,

        [StringValue("cancelled-manually")]
        CancelledManually,

        [StringValue("rescission-by-client")]
        RescissionByClient,

        [StringValue("processing-payment")]
        ProcessingPayment
    }
}
