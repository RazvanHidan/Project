using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using EnvDTE;

namespace HistoryProj
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(HisoryProj.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]

    public sealed class HisoryProj : Package
    {

        public const string PackageGuidString = "af27d302-67b5-4c9e-85dd-5b7d4da534eb";
        private DTE dte;
        private string location;
        private string project;
        private DateTime start;
        private DateTime end;

        public HisoryProj()
        {
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            dte = (DTE)ServiceProvider.GlobalProvider.GetService(typeof(WindowPane));
            dte.Events.WindowEvents.WindowActivated += WindowEvents_WindowActivated;
        }

        private void WindowEvents_WindowActivated(Window GotFocus, Window LostFocus)
        {
            if (GotFocus != null)
            {
                if (start != default(DateTime))
                {
                    end = DateTime.UtcNow;
                    /////{{{}}}////
                    start = default(DateTime);
                }
                else
                {
                    start = DateTime.UtcNow;
                }
                    project = dte.FileName;
                    location = dte.Name;
            }
        }
    }
}
