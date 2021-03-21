# Benchmarks

To run the benchmarks use the following code:

```powershell
dotnet run -c Release --project Reflective.Benchmarks -- --runtimes netcoreapp50 --filter ReflectivePropertyVisitorBenchmarks
```

or for just a quick test add `--job short` as follows:

```powershell
dotnet run -c Release --project Reflective.Benchmarks -- --job short --runtimes netcoreapp50 --filter ReflectivePropertyVisitorBenchmarks
```

## Example Results

|       Method |     Mean |     Error |    StdDev |
|------------- |---------:|----------:|----------:|
|    WithCache | 4.862 us | 0.0970 us | 0.1647 us |
| WithoutCache | 7.896 us | 0.1698 us | 0.4926 us |