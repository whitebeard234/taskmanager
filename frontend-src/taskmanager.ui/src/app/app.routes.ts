import { Routes } from '@angular/router';
import { TaskmanagerComponent } from './components/taskmanager/taskmanager.component';

export const routes: Routes = [
    {
        path: '',
        component: TaskmanagerComponent
    },
    {
        path: 'mytasks',
        component: TaskmanagerComponent
    }
];
