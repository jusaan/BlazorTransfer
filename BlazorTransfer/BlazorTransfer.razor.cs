using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;

namespace TransferBlazor
{
    public partial class BlazorTransfer
    {
        private bool allSourceKeysChecked;
        private bool allTargetKeysChecked;
        private string sourceSearch = string.Empty;
        private string targetSearch = string.Empty;
        private List<string> selectedSourceKeys = new List<string>();
        private List<string> selectedTargetKeys = new List<string>();
        private IEnumerable<object> dataSourceItems;
        private IEnumerable<object> targetKeysItems;

        [Parameter] public IEnumerable<object> DataSource { get; set; }
        [Parameter] public IEnumerable<string> TargetKeys { get; set; } = new List<string>();
        [Parameter] public string TextProperty { get; set; }
        [Parameter] public string ValueProperty { get; set; }
        [Parameter] public bool ShowSearch { get; set; }

        private void SourceCheckboxClicked(ChangeEventArgs e, object value)
        {
            if ((bool)e.Value && !selectedSourceKeys.Contains(value.ToString()))
            {
                selectedSourceKeys.Add(value.ToString());
            }
            else if (!(bool)e.Value && selectedSourceKeys.Contains(value.ToString()))
            {
                selectedSourceKeys.Remove(value.ToString());
            }
        }

        private void TargetCheckboxClicked(ChangeEventArgs e, object value)
        {
            if ((bool)e.Value && !selectedTargetKeys.Contains(value.ToString()))
            {
                selectedTargetKeys.Add(value.ToString());
            }
            else if (!(bool)e.Value && selectedTargetKeys.Contains(value.ToString()))
            {
                selectedTargetKeys.Remove(value.ToString());
            }
        }

        private void SelectAllSourceCheckboxClicked(ChangeEventArgs e, object value)
        {
            if ((bool)e.Value)
            {
                selectedSourceKeys.Clear();
                allSourceKeysChecked = true;
                selectedSourceKeys.AddRange(((IEnumerable<object>)value)
                    .Select(x => x.GetType().GetProperty(ValueProperty).GetValue(x, null).ToString()));

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
                    .Select(x => x.GetType().GetProperty(ValueProperty).GetValue(x, null).ToString()));
            }
            else
            {
                selectedTargetKeys.Clear();
                allTargetKeysChecked = false;
            }
        }

        private void AddTargetKeys()
        {
            TargetKeys = TargetKeys.Concat(selectedSourceKeys).ToList();
            selectedSourceKeys.Clear();
            allSourceKeysChecked = false;
        }

        private void RemoveTargetKeys()
        {
            TargetKeys = TargetKeys.Except(selectedTargetKeys).ToList();
            selectedTargetKeys.Clear();
            allTargetKeysChecked = false;
        }
    }
}
