﻿namespace Example.WindowsFormsApp
{
    using System.Windows.Forms;

    public interface IApplicationPage
    {
        string Title { get; }

        bool CanBack { get; }

        FunctionKey[] FunctionKeys { get; }

        void OnFunctionKey(Keys key);

        void OnBack();
    }
}