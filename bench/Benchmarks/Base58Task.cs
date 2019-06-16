using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Multiformats.Base;

namespace Benchmarks
{
    [CoreJob]
    //[ClrJob]
    //[Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser, GcServer(true)]
    [RPlotExporter, RankColumn]
    public class Base58Task
    {
        [GlobalSetup]
        public void GlobalSetup()
        {
            const string text = "zUXE7GvtEk8XTXs1GF8HSGbVA9FCX9SEBPe";
            Multibase.Base58.Decode(text);
            Multibase.Base58_V2.Decode(text);
        }

        [Params(
            "zUXE7GvtEk8XTXs1GF8HSGbVA9FCX9SEBPe",
            "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")]
        public string text;

        [Benchmark]
        public string Encode_Base58Btc() => Multibase.Base58.Encode(Encoding.ASCII.GetBytes(text));

        [Benchmark]
        public string Encode_Base58BtcV2() => Multibase.Base58_V2.Encode(Encoding.ASCII.GetBytes(text));

        [Benchmark]
        public string Encode_Base58Btc_SimpleBase() => SimpleBase.Base58.Bitcoin.Encode(Encoding.ASCII.GetBytes(text));

        [Benchmark]
        public byte[] Decode_Base58Btc() => Multibase.Base58.Decode(text);

        [Benchmark]
        public byte[] Decode_Base58BtcV2() => Multibase.Base58_V2.Decode(text);

        [Benchmark]
        public Span<byte> Decode_Base58Btc_SimpleBase() => SimpleBase.Base58.Bitcoin.Decode(text);
    }
}
