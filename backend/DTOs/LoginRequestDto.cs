namespace Backend.Dtos;

public record LoginRequestDto(
    string Email,
    string Password
);