using System;
using DiscriminatedUnions.Classic;
using DiscriminatedUnions.Common;
using OneOf;
using OneOf.Types;

namespace DiscriminatedUnions;

class Program
{
    static void Main(string[] args)
    {
        ////////////////////////
        // Object description //
        ////////////////////////
        //
        //
        // Enrolls always have Id: int. 
        //
        // Non-broken Enrolls should have non-null Name, Data and State,
        // and should not have Error.
        //
        // Broken Enrolls only have Id and Error fields.
        //
        //
        ////////////////////////


        //////////////////
        // Classic demo //
        //////////////////

        var activeEnroll = new Enroll()
        {
            Id = 1,
            Name = "A",
            State = EnrollState.Active,
            Data = new EnrollData()
            { { "Provider", "Default" } }
        };
        var activeNullDataEnroll = new Enroll()
        {
            Id = 2,
            Name = "A_null",
            State = EnrollState.Active,
            Data = null
        };
        var bBrokenEnroll = new Enroll()
        {
            Id = 3,
            Name = "A_fail",
            State = EnrollState.Broken,
            Data = null, 
            Error = new Exception("Cannot read from closed connection")
        };

        // change here
        var enroll = activeEnroll;

        try
        {
            var data = EnrollProcessor.GetData(enroll);
        }
        catch (Exception)
        {
            throw;
        }

        ////////////////////////
        // Discriminated demo //
        ////////////////////////        

        var activeEnrollRecord = new ActiveEnroll(
            Id: 1,
            Name: "B",
            Data: new EnrollData()
                { { "Provider", "Default" } }, State: EnrollRecordState.Active);

        //var activeNullEnrollRecord = new ActiveEnroll(
        //    Id: 2,
        //    Name: "B_null",
        //    Data: null,
        //    State: EnrollRecordState.Active);

        var brokenEnrollRecord = new BrokenEnroll(
            Id: 3,
            Ex: new Exception("Cannot read from closed connection"));

        // change here
        var someEnroll = new DiscriminatedEnroll(activeEnrollRecord);

        var dataRecord = EnrollrecordProcessor.GetGetData(someEnroll)
            .Match(
                data => data,
                error => throw error);

        var dataRecordOptional = EnrollrecordProcessor.GetGetData(someEnroll)
            .Match<OneOf<EnrollData, None>>(
                data => data,
                _ => new None());
    }
}
