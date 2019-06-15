``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.300
  [Host]     : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Job-BYFYES : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Runtime=Core  Server=True  

```
|             Method |                   text |         Mean |      Error |     StdDev | Rank |    Gen 0 | Gen 1 | Gen 2 |  Allocated |
|------------------- |----------------------- |-------------:|-----------:|-----------:|-----:|---------:|------:|------:|-----------:|
| Decode_Base58BtcV2 |   zUXE7(...)SEBPe [35] |     4.872 us |  0.0970 us |  0.1566 us |    1 |   0.2289 |     - |     - |    5.44 KB |
|   Decode_Base58Btc |   zUXE7(...)SEBPe [35] |     7.392 us |  0.1475 us |  0.3078 us |    2 |   0.2747 |     - |     - |    6.38 KB |
| Decode_Base58BtcV2 |   UT5UZ(...)UEhxL [78] |    12.134 us |  0.2426 us |  0.5325 us |    3 |   0.7477 |     - |     - |   17.44 KB |
|   Decode_Base58Btc |   UT5UZ(...)UEhxL [78] |    16.992 us |  0.3542 us |  0.9933 us |    4 |   0.7629 |     - |     - |   18.49 KB |
| Decode_Base58BtcV2 | 5VLCA(...)AL3rP [2154] | 1,931.706 us | 38.4403 us | 89.0912 us |    5 | 292.9688 |     - |     - | 6886.72 KB |
|   Decode_Base58Btc | 5VLCA(...)AL3rP [2154] | 2,007.453 us | 39.7605 us | 93.7203 us |    6 | 292.9688 |     - |     - | 6892.88 KB |
