﻿using System;
using System.Diagnostics;
using System.Text;
using QuickLaunch.Data.Model;

namespace QuickLaunch.Services
{
    public class ProcessRunner: IProcessRunner
    {
        public void Run(ILaunchInformation launchInformation)
        {
            var psi = new ProcessStartInfo
            {
                FileName = launchInformation.FileName,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
