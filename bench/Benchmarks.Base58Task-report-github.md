``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.300
  [Host]     : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Job-GTTTHC : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Runtime=Core  Server=True  

```
|             Method |                   text |         Mean |      Error |     StdDev | Rank |    Gen 0 | Gen 1 | Gen 2 |  Allocated |
|------------------- |----------------------- |-------------:|-----------:|-----------:|-----:|---------:|------:|------:|-----------:|
| Decode_Base58BtcV2 |   zUXE7(...)SEBPe [35] |     4.665 us |  0.0930 us |  0.0913 us |    1 |   0.2365 |     - |     - |    5.63 KB |
|   Decode_Base58Btc |   zUXE7(...)SEBPe [35] |     7.003 us |  0.1235 us |  0.1095 us |    2 |   0.2747 |     - |     - |    6.38 KB |
| Decode_Base58BtcV2 |   UT5UZ(...)UEhxL [78] |    11.476 us |  0.2457 us |  0.2051 us |    3 |   0.7477 |     - |     - |   17.63 KB |
|   Decode_Base58Btc |   UT5UZ(...)UEhxL [78] |    16.128 us |  0.3151 us |  0.2631 us |    4 |   0.7629 |     - |     - |   18.49 KB |
| Decode_Base58BtcV2 | 5VLCA(...)AL3rP [2154] | 1,806.415 us | 31.2426 us | 24.3921 us |    5 | 294.9219 |     - |     - | 6886.91 KB |
|   Decode_Base58Btc | 5VLCA(...)AL3rP [2154] | 1,900.608 us | 30.3448 us | 25.3393 us |    6 | 296.8750 |     - |     - | 6892.88 KB |
