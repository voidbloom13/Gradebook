export interface Alert {
    id: string;
    message: string;
    type: 'error' | 'warning' | 'success';
    duration: number;
}