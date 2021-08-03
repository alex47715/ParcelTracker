namespace ParcelStatusService.Data.SqlRepository.SQLQueries
{
    internal static class ParcelStatusQueries
    {
        internal const string SetStatus = @"
        INSERT INTO [dbo].[ParcelStatus] 
        VALUES (@idParcel, @Status)";

        internal const string UpdateStatus = @"UPDATE [dbo].[ParcelStatus]
        SET Status = @Status
        WHERE ParcelId = @idParcel";

        internal const string RemoveStatus = @"DELETE [dbo].[ParcelStatus]
        WHERE ParcelId = @idParcel";
        internal const string GetStatus = @"SELECT Status FROM [dbo].[ParcelStatus]
        WHERE ParcelId = @idParcel";
    }
}
