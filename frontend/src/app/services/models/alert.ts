export interface Alert {
    id: string;
    header: string;
    message: string;
    type: 'error' | 'warning' | 'success' | 'info';
    duration: number;
}