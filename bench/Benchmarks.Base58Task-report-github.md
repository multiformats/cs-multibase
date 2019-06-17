``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7600U CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.2.300
  [Host]     : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Job-SMSTZM : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Runtime=Core  Server=True  

```
|                      Method |                   text |          Mean |       Error |        StdDev |        Median | Rank |    Gen 0 | Gen 1 | Gen 2 |  Allocated |
|---------------------------- |----------------------- |--------------:|------------:|--------------:|--------------:|-----:|---------:|------:|------:|-----------:|
|            **Encode_Base58Btc** | **5VLCA(...)AL3rP [2154]** | **23,733.929 us** | **932.0684 us** | **2,748.2246 us** | **22,874.845 us** |   **17** | **656.2500** |     **-** |     **-** | **16073896 B** |
|          Encode_Base58BtcV2 | 5VLCA(...)AL3rP [2154] | 18,216.214 us | 311.0522 us |   259.7427 us | 18,212.975 us |   16 |        - |     - |     - |    14120 B |
| Encode_Base58Btc_SimpleBase | 5VLCA(...)AL3rP [2154] |  8,447.515 us | 172.7508 us |   212.1535 us |  8,396.938 us |   15 |        - |     - |     - |    11096 B |
|            Decode_Base58Btc | 5VLCA(...)AL3rP [2154] |  1,916.859 us |  36.6521 us |    32.4912 us |  1,920.711 us |   12 | 298.8281 |     - |     - |  7058304 B |
|          Decode_Base58BtcV2 | 5VLCA(...)AL3rP [2154] |  3,049.567 us |  62.0859 us |   161.3696 us |  2,994.773 us |   13 |        - |     - |     - |     1608 B |
| Decode_Base58Btc_SimpleBase | 5VLCA(...)AL3rP [2154] |  5,310.203 us | 101.9358 us |    95.3508 us |  5,314.767 us |   14 |        - |     - |     - |     3216 B |
|            **Encode_Base58Btc** |   **UT5UZ(...)UEhxL [78]** |     **48.852 us** |   **0.7457 us** |     **0.6227 us** |     **48.903 us** |   **11** |   **1.5259** |     **-** |     **-** |    **36920 B** |
|          Encode_Base58BtcV2 |   UT5UZ(...)UEhxL [78] |     24.617 us |   0.5840 us |     0.5177 us |     24.517 us |   10 |        - |     - |     - |      632 B |
| Encode_Base58Btc_SimpleBase |   UT5UZ(...)UEhxL [78] |     11.954 us |   0.1083 us |     0.0960 us |     11.962 us |    8 |   0.0153 |     - |     - |      480 B |
|            Decode_Base58Btc |   UT5UZ(...)UEhxL [78] |     15.619 us |   0.4775 us |     0.6694 us |     15.390 us |    9 |   0.7629 |     - |     - |    18928 B |
|          Decode_Base58BtcV2 |   UT5UZ(...)UEhxL [78] |      4.823 us |   0.0751 us |     0.0665 us |      4.804 us |    4 |        - |     - |     - |       88 B |
| Decode_Base58Btc_SimpleBase |   UT5UZ(...)UEhxL [78] |      6.878 us |   0.1288 us |     0.1265 us |      6.864 us |    6 |        - |     - |     - |      176 B |
|            **Encode_Base58Btc** |   **zUXE7(...)SEBPe [35]** |     **15.660 us** |   **0.2369 us** |     **0.1850 us** |     **15.641 us** |    **9** |   **0.4883** |     **-** |     **-** |    **11536 B** |
|          Encode_Base58BtcV2 |   zUXE7(...)SEBPe [35] |      5.299 us |   0.1131 us |     0.1548 us |      5.278 us |    5 |   0.0153 |     - |     - |      368 B |
| Encode_Base58Btc_SimpleBase |   zUXE7(...)SEBPe [35] |      2.527 us |   0.0340 us |     0.0266 us |      2.529 us |    3 |   0.0076 |     - |     - |      272 B |
|            Decode_Base58Btc |   zUXE7(...)SEBPe [35] |      7.072 us |   0.1407 us |     0.1316 us |      7.024 us |    7 |   0.2747 |     - |     - |     6520 B |
|          Decode_Base58BtcV2 |   zUXE7(...)SEBPe [35] |      1.065 us |   0.0225 us |     0.0536 us |      1.055 us |    1 |   0.0019 |     - |     - |       56 B |
| Decode_Base58Btc_SimpleBase |   zUXE7(...)SEBPe [35] |      1.370 us |   0.0205 us |     0.0181 us |      1.378 us |    2 |   0.0038 |     - |     - |      112 B |
