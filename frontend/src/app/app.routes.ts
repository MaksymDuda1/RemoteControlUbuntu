import { Routes } from '@angular/router';
import { authGuard } from '../guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { UserPageComponent } from './user-page/user-page.component';
import { CommandComponent } from './commands/command/command.component';
import { ConnectionComponent } from './user-connections/connection/connection.component';
import { ConnectionsComponent } from './connections/connections.component';
import {CommandsComponent} from './commands/commands.component';
import {CommandConnectionComponent} from './command-connection/command-connection.component';
import {CommandAproveComponent} from './command-approve/command-aprove.component';
import {CommandSetBuilderComponent} from './commands-set-builder/commands-set-builder.component';
import {WhiteListComponent} from './admin/white-list/white-list.component';
import { BlackListComponent } from './admin/black-list/black-list.component';
import {StatisticsComponent} from './home/statistics/statistics.component';
import { AdminComponent } from './admin/admin.component';
import {InstructionComponent} from './instruction/instruction.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    {
        path: '',
        component: TopMenuComponent,
        children: [
            { path: 'user-page', component: UserPageComponent },
            { path: 'home', component: HomeComponent },
            { path: "execute", component: CommandComponent },
            { path: "connections", component: ConnectionsComponent},
            { path: "commands", component: CommandsComponent},
            { path: "command", component: CommandComponent },
            { path: "command-connection", component: CommandConnectionComponent},
            {path: "command-approve", component: CommandAproveComponent},
            {path: "set-builder", component: CommandSetBuilderComponent},
            {path:"admin", component: AdminComponent},
            {path:'admin-white-list', component: WhiteListComponent},
            {path:"admin-black-list", component: BlackListComponent},
            {path: "admin-statistic", component: StatisticsComponent},
          {path: "admin-admins", component: AdminComponent},
          {path: "instruction", component: InstructionComponent},
        ]
    },
];
