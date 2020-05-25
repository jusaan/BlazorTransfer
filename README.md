# BlazorTransfer
A JavaScript free Transfer (Dual List) library for [Blazor](https://blazor.net) applications

![gif of component in action](screenshot.gif)

## Getting Setup
You can add them searching *BlazorTransfer* in the Nuget Package Manager or from command line by running *dotnet add package BlazorTransfer*

### 1. Add Imports
Add the following to your *_Imports.razor*

```csharp
@using TransferBlazor
```
### 2. Add reference to style sheet(s)
Add the following line to the `head` tag of your `_Host.cshtml` (Blazor Server app) or `index.html` (Blazor WebAssembly).

```
<link rel="stylesheet" href="_content/BlazorTransfer/blazor-transfer.css" />
```

## Wiki

- `DataSource` (required) - The source of data.
- `ValueProperty` (required) - The name of the property that contains the value.
- `TextProperty` (required) - The name of the property that contains the display text.
- `@bind-Value` (required) - To bind a list where the `ValueProperty` of the items of the target table will go.
- `ShowSearch` (optional) - Show a field to search. (Default value: *false*)
- `SearchPlaceholder` (optional) - Change the placeholder text in the search field. (Default value: *Type to search*)
- `HeaderText` (optional) - Change the header text. (Default value: *items*)

## Example

```html
@page "/fetchdata"
@inject WeatherForecastService ForecastService

@if (forecasts == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <BlazorTransfer @bind-Value="selectedItems" DataSource="forecasts" ValueProperty="TemperatureC" TextProperty="Summary" ShowSearch="true" />
}

@code 
{
    private WeatherForecast[] forecasts;

    private IEnumerable<int> selectedItems = new List<int> { 1, 2, 3 }; // Is int because TemperatureC (ValueProperty) is int too

    protected override async Task OnInitializedAsync()
    {
        forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
    }
}
```
