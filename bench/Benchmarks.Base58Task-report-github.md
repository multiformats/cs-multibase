``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.300
  [Host]     : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Job-WLOEEC : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Runtime=Core  Server=True  

```
|                      Method |                   text |          Mean |       Error |        StdDev |        Median | Rank |    Gen 0 | Gen 1 | Gen 2 |  Allocated |
|---------------------------- |----------------------- |--------------:|------------:|--------------:|--------------:|-----:|---------:|------:|------:|-----------:|
|            **Encode_Base58Btc** | **5VLCA(...)AL3rP [2154]** | **23,929.828 us** | **818.1862 us** | **2,412.4403 us** | **24,172.491 us** |   **16** | **656.2500** |     **-** |     **-** | **16073896 B** |
|          Encode_Base58BtcV2 | 5VLCA(...)AL3rP [2154] | 18,012.818 us | 222.0683 us |   185.4371 us | 18,096.431 us |   15 |        - |     - |     - |     8096 B |
| Encode_Base58Btc_SimpleBase | 5VLCA(...)AL3rP [2154] |  8,375.942 us | 163.4444 us |   167.8453 us |  8,350.053 us |   14 |        - |     - |     - |    11096 B |
|            Decode_Base58Btc | 5VLCA(...)AL3rP [2154] |  2,286.929 us | 122.9767 us |   360.6694 us |  2,222.245 us |   11 | 294.9219 |     - |     - |  7058304 B |
|          Decode_Base58BtcV2 | 5VLCA(...)AL3rP [2154] |  3,058.658 us |  71.6978 us |   136.4125 us |  3,013.892 us |   12 |        - |     - |     - |     1608 B |
| Decode_Base58Btc_SimpleBase | 5VLCA(...)AL3rP [2154] |  5,591.871 us | 111.5083 us |   295.7041 us |  5,488.316 us |   13 |        - |     - |     - |     3216 B |
|            **Encode_Base58Btc** |   **UT5UZ(...)UEhxL [78]** |     **61.175 us** |   **3.1170 us** |     **9.0430 us** |     **59.552 us** |   **10** |   **1.5869** |     **-** |     **-** |    **36920 B** |
|          Encode_Base58BtcV2 |   UT5UZ(...)UEhxL [78] |     25.507 us |   0.5077 us |     1.3813 us |     24.933 us |    9 |        - |     - |     - |      344 B |
| Encode_Base58Btc_SimpleBase |   UT5UZ(...)UEhxL [78] |     12.010 us |   0.2406 us |     0.2363 us |     11.981 us |    7 |   0.0153 |     - |     - |      480 B |
|            Decode_Base58Btc |   UT5UZ(...)UEhxL [78] |     15.881 us |   0.5342 us |     0.4461 us |     15.743 us |    8 |   0.8240 |     - |     - |    18928 B |
|          Decode_Base58BtcV2 |   UT5UZ(...)UEhxL [78] |      4.839 us |   0.0855 us |     0.0758 us |      4.832 us |    4 |        - |     - |     - |       88 B |
| Decode_Base58Btc_SimpleBase |   UT5UZ(...)UEhxL [78] |      6.885 us |   0.1307 us |     0.1556 us |      6.863 us |    6 |   0.0076 |     - |     - |      176 B |
|            **Encode_Base58Btc** |   **zUXE7(...)SEBPe [35]** |     **15.689 us** |   **0.3098 us** |     **0.2898 us** |     **15.657 us** |    **8** |   **0.4883** |     **-** |     **-** |    **11536 B** |
|          Encode_Base58BtcV2 |   zUXE7(...)SEBPe [35] |      5.027 us |   0.0736 us |     0.0653 us |      5.023 us |    5 |   0.0076 |     - |     - |      192 B |
| Encode_Base58Btc_SimpleBase |   zUXE7(...)SEBPe [35] |      2.511 us |   0.0429 us |     0.0381 us |      2.499 us |    3 |   0.0114 |     - |     - |      272 B |
|            Decode_Base58Btc |   zUXE7(...)SEBPe [35] |      6.903 us |   0.1213 us |     0.1013 us |      6.912 us |    6 |   0.2899 |     - |     - |     6520 B |
|          Decode_Base58BtcV2 |   zUXE7(...)SEBPe [35] |      1.018 us |   0.0191 us |     0.0204 us |      1.021 us |    1 |   0.0019 |     - |     - |       56 B |
| Decode_Base58Btc_SimpleBase |   zUXE7(...)SEBPe [35] |      1.380 us |   0.0270 us |     0.0239 us |      1.371 us |    2 |   0.0038 |     - |     - |      112 B |
