using System;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace HistoryVS
{
    public class GenericEvent
    {
        #region Member variables
        RunningDocumentInfo rdi;
        //private string message;
        private DateTime start, end;
        private string docName;
        #endregion

        #region Constructors
        /// <summary>
        /// Base class for all other RDT event wrappers.  Each event wrapper 
        /// stores event-specific information and formats it for display
        /// in the Properties window.
        /// </summary>
        /// <param name="rdt">Running Document Table instance</param>
        public GenericEvent(RunningDocumentTable rdt, string message, uint cookie)
        {
            //this.message = message;
            if (rdt == null || cookie == 0) return;

            rdi = rdt.GetDocumentInfo(cookie);
            docName = rdi.Moniker;
        }
        #endregion
        #region Public properties
        [DisplayName("Event Name")]
        [Description("The name of the event.")]
        [Category("Basic")]
        public string EventTime
        {
            get { return (end - start).ToString(); }
        }
        static char[] slashDelim = { '\\' };
        [DisplayName("Doc Name, Short")]
        [Description("The short name of the document.")]
        [Category("Basic")]
        public string DocumentName
        {
            get { return docName; }
        }
        #endregion
    }
}