export interface ForgotPasswordRequest {
    firstName: string;
    lastName: string;
    email: string;
    code: string;
    newPassword: string;
}