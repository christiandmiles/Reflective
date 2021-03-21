# Reflective

Basic reflection caching library and reflection powered object visitor.

## Usage

Before:

```csharp
var properties = target.GetType().GetProperties()
	.Where(p => p.CanRead
		&& !p.PropertyType.IsPointer
		&& p.GetIndexParameters().Length == 0);

foreach (var prop in properties)
{
	var val = prop.GetValue(target);
}
```

```csharp
var _reflectiveCache = new ReflectiveCache();
var properties = _reflectiveCache.Get(target.GetType()).GetProperties()
	.Where(p => p.CanRead
		&& !p.PropertyType.IsPointer
		&& p.GetIndexParameters().Length == 0);

foreach (var prop in properties)
{
	var val = prop.GetValue(target);
}
```

## Reflection Methods Currently Implemented

 - Type.GetProperties()
 - PropertyInfo.GetValue()
 - PropertyInfo.SetValue()
 - PropertyInfo.GetIndexParameters()

All other methods just use the underlying version of the code so provide no performance improvement.

## Additional Functionality

This library also includes a `ReflectivePropertyVisitor` useful for traversing an objects parameters and applying a function on matching types.

The following example will execute the provided func on every `DataStatusItem` hit during tree traversal.

```csharp
var visitor = new ReflectivePropertyVisitor<DataStatusItem>(x =>
{
    // do something
    return Task.CompletedTask;
});
await visitor.VisitAsync(testObject);
```

See `Reflective.Tests/ReflectivePropertyVisitorTests.cs` for an example.

## Motivation

The reason for this library is to provide performance improvements traversing an object using reflection - as such not all methods are cached, just those used in `ReflectivePropertyVisitor`. Further functionality may be added in the future.