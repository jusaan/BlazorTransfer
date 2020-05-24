using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace TransferBlazor
{
    public partial class BlazorTransfer<TItem> : ComponentBase
    {
        private bool allSourceKeysChecked;
        private bool allTargetKeysChecked;
        private string sourceSearch = string.Empty;
        private string targetSearch = string.Empty;
        private List<object> selectedSourceKeys = new List<object>();
        private List<object> selectedTargetKeys = new List<object>();
        private IEnumerable<object> dataSourceItems;
        private IEnumerable<object> targetKeysItems;

        private IEnumerable<TItem> _value = new List<TItem>();

        [Parameter] 
        public IEnumerable<TItem> Value 
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter] public EventCallback<IEnumerable<TItem>> ValueChanged { get; set; }
        [Parameter] public IEnumerable<object> DataSource { get; set; }
        [Parameter] public string TextProperty { get; set; }
        [Parameter] public string ValueProperty { get; set; }
        [Parameter] public bool ShowSearch { get; set; }
        [Parameter] public string SearchPlaceholder { get; set; } = "Search here";
        [Parameter] public string HeaderText { get; set; } = "items";

        private void SourceCheckboxClicked(ChangeEventArgs e, object value)
        {
            if ((bool)e.Value && !selectedSourceKeys.Contains(value))
            {
                selectedSourceKeys.Add(value);
            }
            else if (!(bool)e.Value && selectedSourceKeys.Contains(value))
            {
                selectedSourceKeys.Remove(value);
            }
        }

        private void TargetCheckboxClicked(ChangeEventArgs e, object value)
        {
            if ((bool)e.Value && !selectedTargetKeys.Contains(value))
            {
                selectedTargetKeys.Add(value);
            }
            else if (!(bool)e.Value && selectedTargetKeys.Contains(value))
            {
                selectedTargetKeys.Remove(value);
            }
        }

        private void SelectAllSourceCheckboxClicked(ChangeEventArgs e, object value)
        {
            if ((bool)e.Value)
            {
                selectedSourceKeys.Clear();
                allSourceKeysChecked = true;
                selectedSourceKeys.AddRange(((IEnumerable<object>)value)
                    .Select(x => x.GetType().GetProperty(ValueProperty).GetValue(x, null)));

            }
            else
            {
                selectedSourceKeys.Clear();
                allSourceKeysChecked = false;
            }
        }

        private void SelectAllTargetCheckboxClicked(ChangeEventArgs e, object value)
        {
            if ((bool)e.Value)
            {
                selectedTargetKeys.Clear();
                allTargetKeysChecked = true;
                selectedTargetKeys.AddRange(((IEnumerable<object>)value)
                    .Select(x => x.GetType().GetProperty(ValueProperty).GetValue(x, null)));
            }
            else
            {
                selectedTargetKeys.Clear();
                allTargetKeysChecked = false;
            }
        }

        private void AddTargetKeys()
        {
            Value = Value.Concat(selectedSourceKeys.Cast<TItem>()).ToList();
            selectedSourceKeys.Clear();
            allSourceKeysChecked = false;
        }

        private void RemoveTargetKeys()
        {
            Value = Value.Except(selectedTargetKeys.Cast<TItem>()).ToList();
            selectedTargetKeys.Clear();
            allTargetKeysChecked = false;
        }
    }
}
