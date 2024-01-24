using System;
using DiscriminatedUnions.Common;

namespace DiscriminatedUnions.Classic
{
    public enum EnrollState
    {
        Active,
        Disabled,
        Broken,
    }

    public class Enroll
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public EnrollState State { get; set; }
        public EnrollData? Data { get; set; }
    }

    public static class EnrollProcessor
    {
        public static EnrollData GetData(Enroll enroll)
        {
            if (enroll == null)
            {
                throw new ArgumentNullException(nameof(enroll));
            }

            switch (enroll.State)
            {
                case EnrollState.Active:
                case EnrollState.Disabled:
                    {
                        if (enroll.Data == null)
                        {
                            throw new NullReferenceException(nameof(enroll.Data));
                        }
                        return enroll.Data;
                    }
                default:
                    {
                        throw new ArgumentException($"Enroll [{enroll.Name ?? ""}] have no data.");
                    }
            }
        }
    };
}