﻿namespace QuickLaunch.Data.Access.Abstractions.Interfaces.Model
{
    public interface ILaunchInformation
    {
        string Name { get; }
        string FileName { get; }
        string Arguments { get; }
        string Notes { get; }
        int LaunchCount { get; set; }
    }
}
