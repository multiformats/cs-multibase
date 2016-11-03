using BenchmarkDotNet.Attributes.Jobs;

namespace Multiformats.Base.Benchmarks
{
    [LegacyJitX86Job, RyuJitX64Job]
    public class BenchmarkBase2 : BenchmarkBase<Base2Encoding> { }

    [LegacyJitX86Job, RyuJitX64Job]
    public class BenchmarkBase8 : BenchmarkBase<Base8Encoding> { }

    [LegacyJitX86Job, RyuJitX64Job]
    public class BenchmarkBase10 : BenchmarkBase<Base10Encoding> { }

    [LegacyJitX86Job, RyuJitX64Job]
    public class BenchmarkBase16 : BenchmarkBase<Base16Encoding> { }

    [LegacyJitX86Job, RyuJitX64Job]
    public class BenchmarkBase32 : BenchmarkBase<Base32Encoding> { }

    [LegacyJitX86Job, RyuJitX64Job]
    public class BenchmarkBase58 : BenchmarkBase<Base58Encoding> { }

    [LegacyJitX86Job, RyuJitX64Job]
    public class BenchmarkBase64 : BenchmarkBase<Base64Encoding> { }
}
