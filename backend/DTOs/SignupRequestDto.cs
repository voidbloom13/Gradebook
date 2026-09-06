namespace Backend.Dtos;

public record SignupRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);