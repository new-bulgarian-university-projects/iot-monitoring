using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.Notifiers
{
    public class EmailAlert
    {
        // receiver
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }

        // content
        public string Subject { get; set; }

        public string TextContent { get; set; }
        public string HtmlContent { get; set; }
    }
}
