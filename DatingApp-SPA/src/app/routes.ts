import { Routes } from '@angular/router';
import { HomeComponent } from './Home/Home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResovler } from './_resolvers/member-detail.resolver';
import { MemberListResovler } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResovler } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ListsResovler } from './_resolvers/lists.resolver';
import { MessagesResovler } from './_resolvers/message.resolver';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members', component: MemberListComponent, resolve: {users: MemberListResovler}},
            {path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResovler}},
            {path: 'member/edit', component: MemberEditComponent,
             resolve: {user: MemberEditResovler}, canDeactivate: [PreventUnsavedChanges]},
            {path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResovler}},
            {path: 'lists', component: ListsComponent, resolve: {users: ListsResovler}},
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
