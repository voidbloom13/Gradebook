import { Routes } from '@angular/router';

import { SessionCheck } from './pages/session-check/session-check';
import { Login } from './pages/login/login';
import { Dashboard } from './pages/dashboard/dashboard';
import { VerifyEmail } from './pages/user/verify-email/verify-email';
import { ResetPassword } from './pages/user/reset-password/reset-password';
import { ForgotPassword } from './pages/user/forgot-password/forgot-password';
import { UpdateEmail } from './pages/user/update-email/update-email';

export const routes: Routes = [
    {
        path: '',
        component: SessionCheck,
    },
    {
        path: 'login',
        component: Login,
    },
    {
        path: 'dashboard',
        component: Dashboard,
    },
    {
        path: 'user',
        children: [
            { path: 'verify-email', component: VerifyEmail },
            { path: 'reset-password', component: ResetPassword },
            { path: 'forgot-password', component: ForgotPassword },
            { path: 'update-email', component: UpdateEmail }
        ]
    }
];
