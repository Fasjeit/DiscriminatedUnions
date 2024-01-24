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
        public Exception? Error { get; set; }
    }

    public static class EnrollProcessor
    {
        public static EnrollData GetData(Enroll enroll)
        {
            ArgumentNullException.ThrowIfNull(enroll);

            switch (enroll.State)
            {
                case EnrollState.Active:
                case EnrollState.Disabled:
                {
                    if (enroll.Data == null)
                    {
                        throw new ArgumentException($"Enroll [{enroll.Id}:{enroll.Name ?? ""}] have no data.");
                    }
                    return enroll.Data;
                }
                case EnrollState.Broken:
                {
                    if (enroll.Error != null)
                    {
                        throw new ArgumentException($"Enroll [{enroll.Id}] is broken, see inner Exception.", enroll.Error);
                    }
                    throw new ArgumentException($"Enroll [{enroll.Id}] is broken with no exception.");
                }
                default:
                {
                    throw new ArgumentException($"Unexpected enroll state [{enroll.State}:{enroll.Name ?? ""}] for [{enroll.Id}].");
                }
            }
        }
    };
}