namespace UserService.Repository.Queries
{
    public static class UserQueries
    {
        public static class Commands
        {
            public const string CreateUserCommand =
                 @"INSERT INTO Users (
                                Id, 
                                Name,
                                Birthdate,
                                ProfilePhoto
                                )
                         VALUES (
                                @Id, 
                                @Name,
                                @Birthdate,
                                @ProfilePhoto
                                );";

            public const string FollowCommand =
                 @"INSERT INTO Follows (
                                UserId, 
                                FollowedUserId,
                                createdDate
                                )
                         VALUES (
                                @UserId, 
                                @FollowedUserId,
                                @CreatedDate
                                );";

            public const string UnfollowCommand =
                    @"DELETE FROM Follows WHERE 
                                UserId =  @UserId and
                                FollowedUserId = @FollowedUserId
                                ;";
        }

        public static class Consults
        {
            public const string FindUserById =
                 @"SELECT DISTINCT au.Id, u.Name , au.Email , u.Birthdate , u.ProfilePhoto , u.CreatedDate
                    FROM Users u
                    LEFT JOIN AspNetUsers au ON au.Id = u.Id 
                    WHERE au.Id = @Id
                    ;";

            public const string FindUsers =
                 @"SELECT DISTINCT au.Id, u.Name , au.Email , u.Birthdate , u.ProfilePhoto , u.CreatedDate
                    FROM Users u
                    LEFT JOIN AspNetUsers au ON au.Id = u.Id
                    WHERE u.Id IS NOT NULL
                    ORDER BY u.CreatedDate DESC
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY
                    ;";

            public const string FindUsersBatch =
                 @"SELECT au.Id, u.Name , au.Email , u.Birthdate , u.ProfilePhoto , u.CreatedDate
                    FROM Users u
                    LEFT JOIN AspNetUsers au ON au.Id = u.Id
                    WHERE u.Id in @Ids
                    ORDER BY u.CreatedDate DESC
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY
                    ;";

            public const string FindFollowedUsers =
                 @"SELECT DISTINCT u.Id, u.Name , au.Email , u.Birthdate , u.ProfilePhoto , u.CreatedDate
                    FROM Follows f
                    RIGHT JOIN Users u ON u.Id = f.FollowedUserId  
                    LEFT JOIN AspNetUsers au ON au.Id = f.FollowedUserId 
                    WHERE f.UserId = @Id
                    ORDER BY u.Name
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY
                    ;";

            public const string CountUsers =
                 @"SELECT COUNT(Id) 
                    FROM Users u
                    ;";

            public const string CountFollowedUsers =
                 @"SELECT DISTINCT COUNT(u.Id)
                    FROM Follows f
                    RIGHT JOIN Users u ON u.Id = f.FollowedUserId  
                    WHERE f.UserId = @Id
                    ;";
        }


    }
}
