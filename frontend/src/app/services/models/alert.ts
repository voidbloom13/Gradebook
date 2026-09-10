export interface Alert {
    message: string;
    type: 'error' | 'warning' | 'success';
    duration: number;
}