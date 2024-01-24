using System;
using System.Diagnostics.CodeAnalysis;
using DiscriminatedUnions.Common;
using OneOf;

namespace DiscriminatedUnions
{
    public enum EnrollRecordState
    {
        Active,
        Disabled,
    }
    public record ActiveEnroll(
        int Id,
        [NotNull] string Name,
        [NotNull] EnrollData Data,
        EnrollRecordState State);

    public record BrokenEnroll(
        int Id,
        Exception Ex);

    [GenerateOneOf]
    public partial class DiscriminatedEnroll : OneOfBase<ActiveEnroll, BrokenEnroll>
    {
        ///////////////
        // Generates //
        ///////////////

        //public DiscEnroll(OneOf<ActiveEnroll, BrokenEnroll> input) : base(input)
        //{
        //}
    }

    public static class EnrollrecordProcessor
    {
        public static OneOf<EnrollData, Exception> GetGetData(DiscriminatedEnroll enroll)
        {
            return enroll.Match<OneOf<EnrollData, Exception>>(
                active => active.Data,
                broken => broken.Ex);
        }
    }
}