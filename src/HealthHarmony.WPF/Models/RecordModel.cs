using HealthHarmony.WPF.ViewModels;
using System;

namespace HealthHarmony.WPF.Models;

public class RecordModel : RecordViewModelNotifyPropchanged
{
    private Guid id;
    private string title;
    private string text;
    private DateTime dateOfCreation;
    private DateTime dateOfModification;

    public Guid Id 
    {
        get { return id; }
        set
        {
            id = value;
            NotifyPropertyChanged("Id");
        }
    }

    public string Title 
    {
        get { return title; }
        set
        {
            title = value;
            NotifyPropertyChanged("Title");
        }
    }

    public string Text 
    {
        get { return text; }
        set
        {
            text = value;
            NotifyPropertyChanged("Text");
        }
    }

    public DateTime DateOfCreation 
    {
        get { return dateOfCreation; }
        set
        {
            dateOfCreation = value;
            NotifyPropertyChanged("DateOfCreation");
        }
    }

    public DateTime DateOfModification 
    {
        get { return dateOfModification; }
        set
        {
            dateOfModification = value;
            NotifyPropertyChanged("DateOfModification");
        }
    }
}