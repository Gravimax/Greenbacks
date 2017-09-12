using System;

namespace Greenbacks
{
    public delegate void RecordsLoadedEventHandler(object sender, EventArgs e);
    public delegate void ScrollToRecordEventHandler(object sender, ScrollToRecordEventArgs e);
    public delegate void AddRecordEventHandler(object sender, RecordChangedEventArgs e);
    public delegate void UpdateRecordEventHandler(object sender, RecordChangedEventArgs e);

    public delegate void AccountChangedEventHandler(object sender, AccountChangedEventArgs e);
    public delegate void AccountTypeChangedEventHandler(object sender, AccountTypeChangedEventArgs e);
    public delegate void CategoryChangedEventHandler(object sender, CategoryChangedEventArgs e);
    public delegate void CategoryTypeChangedEventHandler(object sender, CategoryTypeChangedEventArgs e);
    public delegate void PayeeChangedEventHandler(object sender, PayeeChangedEventArgs e);
    public delegate void TransactionChangedEventHandler(object sender, TransactionChangedEventArgs e);

    public delegate void RemoveRecordEventHandler(object sender, RecordChangedEventArgs e);
    public delegate void RecordUpdatedEventHandler(object sender, TableChangedEventArgs e);
    public delegate void TableChangedEventHandler(object sender, TableChangedEventArgs e);
    public delegate void RecordsSavedEventHandler(object sender, RecordsSavedEventArgs e);
    public delegate void AddTabEventHandler(object sender, AddTabEventArgs e);
    public delegate void RemoveTabEventHandler(object sender, EventArgs e);
    public delegate void ExitApplicationEventHandler(object sender, EventArgs e);
}
